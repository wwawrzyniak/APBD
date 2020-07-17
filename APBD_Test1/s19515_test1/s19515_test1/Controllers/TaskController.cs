using Microsoft.AspNetCore.Mvc;
using s19515_test1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace s19515_test1.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TaskController : ControllerBase
    {
        private IDbService _service;


        public TaskController(IDbService service)
        {

            _service = service;
        }

        [HttpGet("{id}")]
        public IActionResult getTeamMemberAndHisTasks(int id)
        {
            return _service.getTeamMemberAndHisTasks(id);

        }
        [HttpDelete("{id}")]
        public IActionResult deleteDataAboutProject(int id)
        {
            return _service.deleteDataAboutProject(id);

        }



    }
}
