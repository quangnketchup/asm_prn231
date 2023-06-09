using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.Models
{
	public  class ParticipatingProject
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int CompanyProjectID { get; set; }
		[Required]
		public int EmployeeID { get; set; }

		[Required]
		[StringLength(40)]
		public string ProjectName { get; set; }

		[Required]
		public ProjectRole ProjectRole { get; set; }

		[Required]
		public DateTime StartDate { get; set; }

		[Required]
		public DateTime EndDate { get; set; }

		public virtual Employee? Employee { get; set; }

	}
	public enum ProjectRole
	{
		ProjectManager = 1, ProjectMember = 2,
	}
}
