using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Entities;

namespace WebApplication1.Controllers
{
    [Route("api/doctors")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {

        private readonly DoctorDbContext _context;
        public DoctorsController(DoctorDbContext context)
        { 
            _context = context;
        }

        //like data of one doctor or data of all doctors?

            /*
        //one doctor with given id
        [HttpGet]
        public IActionResult GetDoctorData(int id)
        {
            try
            {
                var res = _context.Doctors
                         .Where(d2 => d2.IdDoctor == id).ToList();
                return Ok(res.First());
            } catch (Exception e) { return BadRequest(e); }
        }
        */
        //all doctors
        [HttpGet]
        public IActionResult GetAllDoctorsData()
        {
            try
            {
                 return Ok(_context.Doctors.ToList());
            }
            catch (Exception e) { return BadRequest(e); }
            
        }

        [HttpPost("add")]
        public IActionResult AddDoctor(Doctor doctor)
        {
            try
            {
                _context.Doctors.Add(doctor);
                _context.SaveChanges();
                return Ok("Created a new doctor");
            }
            catch (Exception e) { return BadRequest(e);
            }
        }

        [HttpPost("modify")]
        public IActionResult ModifyDoctor(Doctor doctor)
        {
            try
            {
                var res = _context.Doctors
                    .Where(d2 => d2.IdDoctor == doctor.IdDoctor)
                    .ToList();

                Doctor oldDoctor = res.First();

                oldDoctor.FirstName = doctor.FirstName;
                oldDoctor.LastName = doctor.LastName;
                oldDoctor.Email = doctor.Email;

                _context.Update(oldDoctor);

                _context.SaveChanges();
                return Ok(doctor.IdDoctor + " successfully modified");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete("delete")]
        public IActionResult DeleteDoctor(int id)
        {
            try
            {
                var res = _context.Doctors
                    .Where(d2 => d2.IdDoctor == id).ToList();

                _context.Doctors.Remove(res.First());

                _context.SaveChanges();
                return Ok(id + " successfully deleted");
            }
            catch (Exception e) { return BadRequest(e); }
        }

    }
}


