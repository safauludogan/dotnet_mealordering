using Blazored.LocalStorage;
using Blazored.Modal;
using MealOrdering.Server.Data.Context;
using MealOrdering.Server.Services.Extensions;
using MealOrdering.Server.Services.Infasracture;
using MealOrdering.Server.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddBlazoredModal();

builder.Services.ConfigureMapping();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<HttpContextAccessor>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<IValidationService, ValidationService>();

builder.Services.AddDbContext<DataContext>(opt =>
{
	opt.UseNpgsql("User ID=postgres;password=sas;Host=localhost;Port=5432;Database=mealordering");
	opt.EnableSensitiveDataLogging();
});

builder.Services.AddAuthentication(opt =>
{
	opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(opt =>
{
	opt.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		ValidIssuer = builder.Configuration["JwtIssuer"],
		ValidAudience = builder.Configuration["JwtAudience"],
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSecurityKey"])),
	};
});

builder.Services.AddBlazoredLocalStorage();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseWebAssemblyDebugging();
}
else
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");
app.Run();
