using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.Models
{
	public class Department
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int DepartmentID { get; set; }

		[Required(ErrorMessage = "DepartmentName is required!!")]
		[MinLength(2, ErrorMessage = "DepartmentName must have at least 2 characters!!")]
		[MaxLength(1000, ErrorMessage = "DepartmentName is limited to 255 characters!!")]
		public string DepartmentName { get; set; }

		[Required(ErrorMessage = "Department Description is required!!")]
		[MinLength(2, ErrorMessage = "Department Description must have at least 2 characters!!")]
		[MaxLength(1000, ErrorMessage = "Department Description is limited to 1000 characters!!")]
		public string DepartmentDescription { get; set; }
	}
}
