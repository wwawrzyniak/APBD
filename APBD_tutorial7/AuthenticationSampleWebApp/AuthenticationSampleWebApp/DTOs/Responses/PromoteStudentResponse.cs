using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationSampleWebApp.DTOs.Responses
{
    public class PromoteStudentResponse
    {
        public int IdEnrollment { get; set; }
        public string StudyName { get; set; }
        public int Semester { get; set; }
    }
}
