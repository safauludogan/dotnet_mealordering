using AutoMapper;
using AutoMapper.QueryableExtensions;
using MealOrdering.Server.Data.Context;
using MealOrdering.Server.Data.Models;
using MealOrdering.Server.Services.Infasracture;
using MealOrdering.Shared.DTO;
using Microsoft.EntityFrameworkCore;

namespace MealOrdering.Server.Services.Services
{
	public class UserService : IUserService
	{
		private readonly IMapper _mapper;
		private readonly DataContext _context;

		public UserService(IMapper Mapper, DataContext context)
		{
			_mapper = Mapper;
			_context = context;
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
			var dbUser = await _context.Users.Where(i => i.Id == User.Id).FirstOrDefaultAsync();
			if (dbUser != null)
				throw new Exception("İlgili kayıt zaten mevcut");

			dbUser = _mapper.Map<Users>(dbUser);

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

			_mapper.Map(User,dbUser);

			await _context.SaveChangesAsync();
			return _mapper.Map<UserDto>(dbUser);
		}
	}
}
