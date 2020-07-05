using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Exceptions;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {

        
        [HttpGet]
        public IActionResult GetStudents()
        {
            var list = new List<Student>()
            {
                new Student{IdStudent=1, FirstName="Jan", LastName="Kowalski"},
                new Student{IdStudent=2, FirstName="Andrzej", LastName="Malewicz"},
                new Student{IdStudent=3, FirstName="Krzysztof", LastName="Andrzejewicz"}
            };

            //throw new StudentCannotDefendException("Student nie moze sie bronic, bo ...");

            return Ok(list);
        }



    }
}