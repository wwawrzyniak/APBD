using Microsoft.AspNetCore.Mvc;
using System;
using tutorial3.IDL;
using tutorial3.Models;

namespace tutorial3.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private readonly IdbService _dbService;

        public StudentsController(IdbService dbService)
        {
            _dbService = dbService;
        }
        [HttpGet]
        public IActionResult GetStudents(string orderBy)
        {
            return Ok(_dbService.GetStudents(orderBy));
        }

        [HttpGet]
         public IActionResult GetStudent() {
            return Ok(_dbService.GetStudents());
          }

        [HttpGet("{id}")]
            public IActionResult GetStudent([FromRoute]string id)
            {
                    Student st = _dbService.GetStudent (id);
                     if (st != null) return Ok (st);
                     else return NotFound ("Nie znalezniono studenta");
            }
        [HttpPost]
        public IActionResult CreateStudent([FromBody]Student student)
        {
            if (student != null)
                return Ok(_dbService.AddStudent(student));
            else return NotFound("Wrong input");
        }

        [HttpPut]
        public IActionResult UpdateStudent([FromBody]Student student)
        {
            if (student != null) { 
                _dbService.UpdateStudent(student);
                return Ok($"{student} update complete - surname changed to updated");
            }

            return NotFound("Student doesnt exist");
        }
        [HttpDelete]
        public IActionResult DeleteStudent([FromRoute] string id)
        {
                _dbService.DeleteStudent(id);
                return Ok("Delete complete");
        }

        [HttpDelete]
        public IActionResult DeleteStudent([FromBody] Student st)
        {
            if (st != null)
            {
                _dbService.DeleteStudent(st);
                return Ok("Delete complete");
            }
            else return NotFound("Student doesnt exist");
        }
        }
    }
}