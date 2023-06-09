using BussinessObject.Models;

namespace DTO
{
	public class AuthorizeUser : Employee
	{
		public AuthorizeUser(Employee employee) {
			EmailAddress = employee.EmailAddress;
			EmployeeID = employee.EmployeeID;
			FullName = employee.FullName;
		}
		public AuthorizeUser()
		{

		}
		public string AuthorizeRole { get; set; }

	}
}
