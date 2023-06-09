using BussinessObject.Models;
using DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace WebAPI.Extensions
{
	public static class DatabaseExtension
	{
		public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
		{
			var connectionString = configuration.GetConnectionString("database=NguyenVanQuang_SE150550_A02_DB");
			services.Configure<UserLoginDTO>(configuration.GetSection("Account"));
			services.AddDbContext<MyDBContext>(options => options.UseSqlServer(connectionString));
		}
	}

}
