using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace Wyklad5.DTOs.Requests
{
	public class PromoteStudentRequest
	{
		[Required]
		public string StudyName { get; set; }
		[Required]
		public int Semester { get; set; }

	}

}
