using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuthenticationSampleWebApp.DTOs.Requests;
using AuthenticationSampleWebApp.Services;

namespace AuthenticationSampleWebApp
{

    [Route("api/enrollments")]
    [ApiController] //-> implicit model validation
    public class EnrollmentsController : ControllerBase
    {
        private IStudentDbService _service;

        public EnrollmentsController(IStudentDbService service)
        {
            _service = service;
        }

        [Authorize(Roles = "employee")]
        [HttpPost]
        public IActionResult EnrollStudent(EnrollStudentRequest request)
        {

            return _service.EnrollStudent(request);
        }

        [Authorize(Roles = "employee")]
        [HttpPost("promotions")]
        public IActionResult PromoteStudents(PromoteStudentRequest request)
        {

            return _service.PromoteStudents(request);
        }

        //..

        //..


    }
}
