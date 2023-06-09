using BussinessObject.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using Repository;
using System.Data;

namespace WebAPI.Extension
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddRepository(this IServiceCollection services)
		{
			services.AddScoped<IEmployeeRepository, EmployeeRepository>();
			return services;
		}
	}
}
