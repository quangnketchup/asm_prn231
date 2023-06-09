using BussinessObject.Models;
using DTO;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.OData;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using WebAPI.Extension;

namespace WebAPI.Extensions
{
	public static class ServiceExtension
	{
		public static void AddService(this IServiceCollection services)
		{
			//services.AddDbContext<eStoreContext>(options =>
			//{
			//    options.UseSqlServer(Configuration.GetConnectionString("eStoreDb"));
			//});
			//services.AddDatabaseDeveloperPageExceptionFilter();
			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie(options =>
				{
					options.Cookie.Name = "ParticipantLoginCookie";
					options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None;
					options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
					options.Events = new CookieAuthenticationEvents
					{
						OnRedirectToLogin = redirectContext =>
						{
							redirectContext.HttpContext.Response.StatusCode = 401;
							return Task.CompletedTask;
						},
						OnRedirectToAccessDenied = redirectContext =>
						{
							redirectContext.HttpContext.Response.StatusCode = 401;
							return Task.CompletedTask;
						},
					};
				});

			services.AddCors(options =>
			{
				options.AddPolicy("CorsPolicy",
					builder => builder.SetIsOriginAllowed(host => true)
								.AllowAnyMethod()
								.AllowAnyHeader()
								.AllowCredentials());
			});
			services.AddHttpContextAccessor();
			services.AddRepository();
			services.AddControllers();

			services.AddControllers()
				.AddNewtonsoftJson(options =>
				{
					options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
					options.SerializerSettings.DateFormatString = "yyyy'-'MM'-'dd'T'HH':'mm':'ssZ";
					options.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Local;
				}
			)
				.AddOData(option => option.Select().Filter()
					.Count().OrderBy().Expand().SetMaxTop(100)
					.AddRouteComponents("odata", GetEdmModel()));
			//services.addodataswaggersupport();
		}

		private static IEdmModel GetEdmModel()
{
	ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
	builder.EntitySet<Employee>("Employees");

	return builder.GetEdmModel();
}
	}
}
