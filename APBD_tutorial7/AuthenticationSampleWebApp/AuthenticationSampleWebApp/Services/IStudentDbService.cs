using AuthenticationSampleWebApp.DTOs;
using AuthenticationSampleWebApp.DTOs.Requests;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace AuthenticationSampleWebApp.Services
{

    public interface IStudentDbService
    {
        IActionResult EnrollStudent(EnrollStudentRequest request);
        IActionResult PromoteStudents(PromoteStudentRequest request);
        IActionResult LoginCheck(LoginRequestDto request);
        IActionResult SecureMyPassword(passwordRequest request);

    }
}


