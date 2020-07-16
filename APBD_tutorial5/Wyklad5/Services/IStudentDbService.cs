using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wyklad5.DTOs.Requests;

namespace Wyklad5.Services
{
    public interface IStudentDbService
    {
        IActionResult EnrollStudent(EnrollStudentRequest request);
        IActionResult PromoteStudents(PromoteStudentRequest request);
    }
}
