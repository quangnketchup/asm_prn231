using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Reflection.Emit;

namespace BussinessObject.Models
{
	public class MyDBContext : DbContext
	{
		public MyDBContext()
		{

		}

		public MyDBContext(DbContextOptions options) : base(options)
		{
		}

		
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			var config = new ConfigurationBuilder()
			   .SetBasePath(Directory.GetCurrentDirectory())
			   .AddJsonFile("appsettings.json", true, true)
			   .Build();
			optionsBuilder.UseSqlServer(config.GetConnectionString("NguyenVanQuang_SE150550_A02_DB"));
		}
		public DbSet<CompanyProject> CompanyProjects { get; set; }
		public DbSet<Employee> Employees { get; set; }
		public DbSet<Department> Department { get; set; }
		public DbSet<ParticipatingProject> ParticipatingProject { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<CompanyProject>().ToTable("CompanyProject");
			modelBuilder.Entity<Employee>().ToTable("Employee");
			modelBuilder.Entity<Department>().ToTable("Department");
			modelBuilder.Entity<ParticipatingProject>().ToTable("ParticipatingProject");
			modelBuilder.Entity<Employee>()
				.HasIndex(e => e.EmailAddress)
				.IsUnique();
		}
	}
}
