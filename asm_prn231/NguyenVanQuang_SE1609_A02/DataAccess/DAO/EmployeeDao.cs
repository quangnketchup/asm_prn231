using BussinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ParticipantManagementLibrary;

namespace DataAccess.DAO
{
	public class EmployeeDao
	{
		private static EmployeeDao instance = null;
		private static readonly object instanceLock = new object();

		private EmployeeDao()
		{

		}

		public static EmployeeDao Instance
		{
			get
			{
				lock (instanceLock)
				{
					if (instance == null)
					{
						instance = new EmployeeDao();
					}
					return instance;
				}
			}
		}
		public Employee GetDefaultUser()
		{
			return JsonConvert.DeserializeObject<Employee>(ParticipantManagementApiConfiguration.DefaultAccount);
		}
		public async Task<Employee> LoginAsync(string email, string password)
		{
			var db = new MyDBContext();
			IEnumerable<Employee> users = await db.Employees.ToListAsync();
			users = users.Prepend(GetDefaultUser());
			if (email == null)
			{
				email = string.Empty;
			}
			if (password == null)
			{
				password = string.Empty;
			}
			return users?.SingleOrDefault(user =>
	user?.EmailAddress?.ToLower() == email?.ToLower() &&
	user?.Password == password);

		}

		public async Task<IEnumerable<Employee>> GetEmployeesAsync()
		{
			var db = new MyDBContext();
			return await db.Employees.ToListAsync();
		}

		public async Task<Employee> GetEmployeeAsync(int EmployeeID)
		{
			var db = new MyDBContext();
			return await db.Employees.FindAsync(EmployeeID);
		}

		public async Task<Employee> AddEmployeeAsync(Employee newEmployee)
		{
			CheckEmployee(newEmployee);
			var db = new MyDBContext();
			await db.Employees.AddAsync(newEmployee);
			await db.SaveChangesAsync();

			return newEmployee;
		}

		public async Task<Employee> UpdateEmployeeAsync(Employee updatedEmployee)
		{
			if (await GetEmployeeAsync(updatedEmployee.EmployeeID) == null)
			{
				throw new ApplicationException("Employee does not exist!!");
			}
			CheckEmployee(updatedEmployee);
			var db = new MyDBContext();
			db.Employees.Update(updatedEmployee);
			await db.SaveChangesAsync();
			return updatedEmployee;
		}

		public async Task DeleteEmployeeAsync(int EmployeeID)
		{
			Employee deletedEmployee = await GetEmployeeAsync(EmployeeID);
			if (deletedEmployee == null)
			{
				throw new ApplicationException("Employee does not exist!!");
			}
			var db = new MyDBContext();
			db.Employees.Remove(deletedEmployee);
			await db.SaveChangesAsync();
		}

		private void CheckEmployee(Employee employee)
		{
			employee.FullName.StringValidate(
				allowEmpty: false,
				emptyErrorMessage: "Employee Last name is required!!",
				minLength: 2,
				minLengthErrorMessage: "Employee Last name must have at least 2 characters!!",
				maxLength: 255,
				maxLengthErrorMessage: "Employee Last name is limited to 255 characters!!"
				);
			if (!string.IsNullOrEmpty(employee.Telephone))
			{
				employee.Telephone.IsPhoneNumber("Phone number is invalid!!");
			}

			employee.Address.StringValidate(
				allowEmpty: true,
				emptyErrorMessage: "",
				minLength: 2,
				minLengthErrorMessage: "Employee Address must have at least 2 characters!!",
				maxLength: 500,
				maxLengthErrorMessage: "Employee Address is limited to 500 characters!!"
				);

			employee.EmailAddress.IsEmail("Email is invalid!!");
		}
	}
}
