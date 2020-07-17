using Microsoft.AspNetCore.Mvc;
using System;
using WebApplication3.DTOs.Requests;
using WebApplication3.Service;

namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("api/pets")]
    public class PetsController : ControllerBase
    {
        private readonly IDbService _service;

        public PetsController(IDbService service)
        {
            _service = service;
        }

       [HttpGet("{year}")]
         public IActionResult list([FromQuery] int? year)
       {
            try
            {
                return _service.listPets(year);
            }
            catch(Exception e) { return BadRequest(e); }
            
        }

        [HttpPost]
        public IActionResult add([FromBody] AddPetRequest request)
        {
            try
            {
                return _service.addPet(request);
            }
            catch(Exception e) { return BadRequest(e); }
        }
    }
}
