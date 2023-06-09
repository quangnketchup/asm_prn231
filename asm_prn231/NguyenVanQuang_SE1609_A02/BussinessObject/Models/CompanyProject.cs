using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.Models
{
	public class CompanyProject
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int CompanyProjectID { get; set; }

		[Required(ErrorMessage = "Project name is required!!")]
		[Display(Name = "Project Name")]
		[MinLength(2, ErrorMessage = "Project name must have at least 2 characters!!")]
		[MaxLength(255, ErrorMessage = "Project name is limited to 255 characters!!")]
		public string ProjectName { get; set; }

		[Required(ErrorMessage = "Project Description is required!!")]
		[MinLength(2, ErrorMessage = "Project Description must have at least 2 characters!!")]
		[MaxLength(1000, ErrorMessage = "Project Description is limited to 255 characters!!")]
		public string ProjectDescription { get; set; }
		 
		[Required(ErrorMessage = "Estimated Start Date is required!!")]
		[Display(Name = "Estimated Start Date Date")]
		[DataType(DataType.DateTime)]
		public DateTime EstimatedStartDate { get; set; }

		[Required(ErrorMessage = "Expected End Date is required!!")]
		[Display(Name = "Expected End Date")]
		[DataType(DataType.DateTime)]
		public DateTime ExpectedEndDate { get; set; }

	}
}
