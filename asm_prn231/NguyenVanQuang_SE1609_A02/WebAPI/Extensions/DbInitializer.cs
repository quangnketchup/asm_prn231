using BussinessObject.Models;

namespace WebAPI.Extension
{
    public class DbInitializer
    {
        public static void Initialize(MyDBContext context)
        {
            context.Database.EnsureCreated();

            if (context.Employees.Any())
            {
                return;
            }

            IEnumerable<Employee> users = new List<Employee>()
            {
                new Employee()
                {
                    EmailAddress = "test@gmail.com",
                    Password = "123",
                    FullName = "Test",
                }
            };
            context.Employees.AddRange(users);
            context.SaveChanges();
        }
    }
}
