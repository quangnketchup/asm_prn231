using BussinessObject.Models;
using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository;
	public class EmployeeRepository : IEmployeeRepository
	{
		public async Task<Employee> AddEmployeeAsync(Employee newEmployee)
			=> await EmployeeDao.Instance.AddEmployeeAsync(newEmployee);

		public async Task DeleteEmployeeAsync(int employeeId)
			=> await EmployeeDao.Instance.DeleteEmployeeAsync(employeeId);

		public async Task<Employee> GetEmployeeAsync(int authorId)
			=> await EmployeeDao.Instance.GetEmployeeAsync(authorId);

		public async Task<IEnumerable<Employee>> GetEmployeesAsync() => await EmployeeDao.Instance.GetEmployeesAsync();

		public async Task<Employee> UpdateEmployeeAsync(Employee updatedEmployee) => await EmployeeDao.Instance.UpdateEmployeeAsync(updatedEmployee);
		public async Task<Employee> LoginAsync(string email, string password) => await EmployeeDao.Instance.LoginAsync(email, password);
		public Employee GetDefaultEmployee()
			=> EmployeeDao.Instance.GetDefaultUser();
	}

	public interface IEmployeeRepository
	{
		Task<IEnumerable<Employee>> GetEmployeesAsync();
		Task<Employee> GetEmployeeAsync(int employeeId);
		Task<Employee> AddEmployeeAsync(Employee newEmployee);
		Task<Employee> UpdateEmployeeAsync(Employee updatedEmployee);
		Task DeleteEmployeeAsync(int employeeId);
		Task<Employee> LoginAsync(string email, string password);
		Employee GetDefaultEmployee();

	}
