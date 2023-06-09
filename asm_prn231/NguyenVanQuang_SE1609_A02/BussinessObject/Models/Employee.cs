using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.Models
{
	public class Employee
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int EmployeeID { get; set; }

		[Display(Name = "FullName")]
		[MinLength(2, ErrorMessage = "FullName must have at least 2 characters!!")]
		[MaxLength(255, ErrorMessage = "FullName is limited to 255 characters!!")]
		public string FullName { get; set; }

		[Display(Name = "FullName")]
		[MinLength(2, ErrorMessage = "FullName must have at least 2 characters!!")]
		[MaxLength(255, ErrorMessage = "FullName is limited to 255 characters!!")]
		public string Password { get; set; }

		[Display(Name = "Password")]
		[MinLength(2, ErrorMessage = "Skills must have at least 2 characters!!")]
		[MaxLength(255, ErrorMessage = "Skills is limited to 255 characters!!")]
		public string Skills { get; set; }

		[Display(Name = "Phone number")]
		[Phone(ErrorMessage = "Phone number is invalid!!")]
		public string Telephone { get; set; }
		[MinLength(2, ErrorMessage = "Employee Address must have at least 2 characters!!")]
		[MaxLength(500, ErrorMessage = "Employee Address is limited to 500 characters!!")]
		public string Address { get; set; }
		public Status Status { get; set; }
		public int DepartmentID { get; set; }
		[Display(Name = "Email Address")]
		[Required(ErrorMessage = "Email Address is required!!")]
		[EmailAddress(ErrorMessage = "Email is invalid!!")]
		public string EmailAddress { get; set; }
		public virtual Department Department { get; set; }
	}

	public enum Status
	{
		Active = 1, Inactive =2,
	}
}
