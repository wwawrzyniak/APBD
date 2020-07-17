using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WebApplication1.DTO;
using WebApplication1.Models;
using WebApplication1.Service;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/animals")]
    public class AnimalController : ControllerBase
    {
        private IDbAnimals _service;


         public AnimalController(IDbAnimals service)
        {
            
            _service = service;
        }

        /*[HttpGet]
        public IActionResult getAnimals(string orderBy)
        {
            return _service.getAnimals(orderBy);
        }*/

        [HttpPost("add")]
        public IActionResult addAnimal(AnimlaRequest request) {
            return _service.AddAnimal(request);
         }

    }
}
    

