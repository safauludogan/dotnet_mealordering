using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace MealOrdering.Server.Data.Context
{
	public class DesignTimeDataContextFactory : IDesignTimeDbContextFactory<DataContext>
	{
		public DataContext CreateDbContext(string[] args)
		{
			string connectionString = "User ID=postgres;password=sas;Host=localhost;Port=5432;Database=mealordering";
			var builder = new DbContextOptionsBuilder<DataContext>();

			builder.UseNpgsql(connectionString);

			return new DataContext(builder.Options);
		}
	}
}
