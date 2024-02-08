using AutoMapper;
using AutoMapper.QueryableExtensions;
using MealOrdering.Server.Data.Context;
using MealOrdering.Server.Data.Models;
using MealOrdering.Server.Services.Infasracture;
using MealOrdering.Shared.CustomExceptions;
using MealOrdering.Shared.DTO;
using MealOrdering.Shared.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MealOrdering.Server.Services.Services
{
	public class UserService : IUserService
	{
		private readonly IMapper _mapper;
		private readonly DataContext _context;
		private readonly IConfiguration _configuration;

		public UserService(IMapper Mapper, DataContext context, IConfiguration configuration)
		{
			_mapper = Mapper;
			_context = context;
			_configuration = configuration;
		}

		public async Task<List<UserDto>> GetUsersAsync()
		{
			return await _context.Users
				.Where(i => i.IsActive)
				.ProjectTo<UserDto>(_mapper.ConfigurationProvider)
				.ToListAsync();
		}
		public async Task<UserDto?> GetUserById(Guid Id)
		{
			return await _context.Users
				.Where(i => i.Id == Id)
				.ProjectTo<UserDto>(_mapper.ConfigurationProvider)
				.FirstOrDefaultAsync();
		}
		public async Task<UserDto> CreateUser(UserDto User)
		{
			var dbUserCheck = await _context.Users.Where(i => i.Id == User.Id).FirstOrDefaultAsync();
			if (dbUserCheck != null)
				throw new Exception("İlgili kayıt zaten mevcut");

			var dbUser = _mapper.Map<Users>(User);

			await _context.Users.AddAsync(dbUser);
			await _context.SaveChangesAsync();
			return _mapper.Map<UserDto>(dbUser);
		}

		public async Task<bool> DeleteUserById(Guid Id)
		{
			var dbUser = await _context.Users.Where(i => i.Id == Id).FirstOrDefaultAsync();
			if (dbUser == null)
				throw new Exception("User not found!");
			_context.Remove(dbUser);
			var result = await _context.SaveChangesAsync();
			return result > 0 ? true : false;
		}

		public async Task<UserDto> UpdateUser(UserDto User)
		{
			var dbUser = await _context.Users.Where(i => i.Id == User.Id).FirstOrDefaultAsync();
			if (dbUser == null)
				throw new Exception("İlgili kayıt bulunamadı!");

			_mapper.Map(User, dbUser);

			await _context.SaveChangesAsync();
			return _mapper.Map<UserDto>(dbUser);
		}

		public async Task<UserLoginResponseDto> Login(string Email, string Password)
		{
			//Veritabanı kullanıcı doğrulama işlemleri yapıldı.
			var encryptedPassword = PasswordEncrypter.Encrypt(Password);
			var dbUser = await _context.Users.FirstOrDefaultAsync(i=>i.EmailAddress==Email && i.Password == encryptedPassword);

			if (dbUser == null)
				throw new Exception("User not found!");

			if(!dbUser.IsActive)
				throw new Exception("User state is Passive!");

			UserLoginResponseDto result = new UserLoginResponseDto();

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityKey"]));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var expiry = DateTime.Now.AddDays(int.Parse(_configuration["JwpExpiryDays"].ToString()));

			var claims = new[]
			{
				new Claim(ClaimTypes.Email,Email)
			};

			var token = new JwtSecurityToken(_configuration["JwtIssuer"], _configuration["JwtAudience"], claims, null, expiry, creds);

			result.ApiToken = new JwtSecurityTokenHandler().WriteToken(token);
			result.User = _mapper.Map<UserDto>(dbUser);

			return result;
		}
	}
}
