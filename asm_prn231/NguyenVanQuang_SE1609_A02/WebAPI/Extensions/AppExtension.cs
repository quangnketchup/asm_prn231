namespace WebAPI.Extensions
{
	public static class AppExtension
	{
		public static void RegisterFeatures(this IApplicationBuilder app)
		{
			app.UseSwagger();
			app.UseSwaggerUI(c => { c.SwaggerEndpoint($"/swagger/v1/swagger.json", "My Api v1"); });
			app.UseAuthentication();
			app.UseRouting();
			app.UseCors("CorsPolicy");
			app.UseAuthorization();
			app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
		}
	}
}
