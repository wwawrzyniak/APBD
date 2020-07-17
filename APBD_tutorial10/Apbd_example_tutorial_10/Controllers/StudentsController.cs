using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apbd_example_tutorial_10.Entities;
using Apbd_example_tutorial_10.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Apbd_example_tutorial_10.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly StudentContext _studentContext;

        public StudentsController(StudentContext studentContext)
        {
            _studentContext = studentContext;
        }
        //2.1
        [HttpGet]
        public IActionResult GetStudents()
        {
            var students = _studentContext.Student
                                          .Include(s => s.IdEnrollmentNavigation).ThenInclude(e => e.IdStudyNavigation)
                                          .Select(s => new GetStudentsResponse
                                          {
                                              IndexNumber = s.IndexNumber,
                                              FirstName = s.FirstName,
                                              LastName = s.LastName,
                                              BirthDate = s.BirthDate.ToShortDateString(),
                                              Semester = s.IdEnrollmentNavigation.Semester,
                                              Studies = s.IdEnrollmentNavigation.IdStudyNavigation.Name
                                          })
                                          .ToList();

           
            return Ok(students);
        }
        //2.2
        [HttpPost("Add")]
        public IActionResult AddStudent(Student student)
        {
            try
            {
                var added = _studentContext.Student.Add(student);

                _studentContext.SaveChanges();
            }
            catch (Exception e) { return BadRequest(e); }
            return Ok(student.FirstName + " successfully added");
        }
        //2.3
        [HttpPost("Modify")]
        public IActionResult ModifyStudent(Student student)
        {
            try
            {
                var res = _studentContext.Student
                    .Where(student2 => student2.IndexNumber == student.IndexNumber)
                    .ToList();

                Student oldStudent = res.First();

                oldStudent.FirstName = student.FirstName;
                oldStudent.LastName = student.LastName;
                oldStudent.BirthDate = student.BirthDate;
                oldStudent.IdEnrollment = student.IdEnrollment;

                _studentContext.Update(oldStudent);

                _studentContext.SaveChanges();
                return Ok(student.IndexNumber + " successfully modified");
            }
            catch (Exception e) { return BadRequest(e);
            }
        }

        //2.4
        [HttpDelete("{Index}")]
        public IActionResult DeleteStudent(string Index)
        {
            try
            {
                var res = _studentContext.Student
                    .Where(student2 => student2.IndexNumber == Index).ToList();

                _studentContext.Student.Remove(res.First());

                _studentContext.SaveChanges();
                return Ok(Index + " successfully deleted");
            }
            catch (Exception e) { return BadRequest(e); }
        }

        //3.1
        [HttpPost("Promote")]
        public IActionResult PromoteStudents(PromoteStudentRequest request)
        {
            try
            {
                //Check if all required data has been delivered. 
                if (string.IsNullOrEmpty(request.Semester.ToString()) || string.IsNullOrWhiteSpace(request.StudyName))
                {
                    return BadRequest("Not enough data");
                }
                //Check if Enrollment table contains provided Studies and Semester. Otherwise return 404 (Not Found).
                var res =( from enr in _studentContext.Enrollment
                          join stud in _studentContext.Studies
                          on enr.IdStudy equals stud.IdStudy
                          where stud.Name.Equals(request.StudyName) select enr).ToList();

                if (res.Count() == 0)
                {
                    return NotFound(404);
                }

                var res2 = (from enr in _studentContext.Enrollment
                           where enr.Semester == request.Semester
                           select enr).ToList();

                if (res2.Count() == 0)
                {
                    return NotFound(404);
                }

                //Now that everything was checked we can promote students to next semester

                var lastEnrollment = _studentContext.Enrollment
                                     .OrderByDescending(en => en.IdEnrollment)
                                     .ToList()
                                     .First();
                var theEnrollment = lastEnrollment.IdEnrollment + 1;
                var theSemester = lastEnrollment.Semester + 1;
                var idStud = _studentContext.Studies
                             .Where(x => x.Name.Equals(request.StudyName))
                             .Select(y => y.IdStudy)
                             .ToList()
                             .First();

                Enrollment newEnrollment = new Enrollment {
                    IdEnrollment = theEnrollment,
                    Semester = theSemester,
                    IdStudy = idStud,
                    StartDate = DateTime.Now

                };
                var res4 = _studentContext.Enrollment.Add(newEnrollment);
                var res5 = (from en in _studentContext.Enrollment where (en.Semester.Equals(request.Semester)) && (en.IdStudy.Equals(idStud)) select en.IdEnrollment).ToList();
                if (res5.Count==0) { return BadRequest("No students to promote"); }
                var query =
               (from student in _studentContext.Student
                where student.IdEnrollment == res5.First() select student);

                foreach (Student s in query)
                {
                    s.IdEnrollment = theEnrollment;
                    _studentContext.Update(s);
                }
                _studentContext.SaveChanges();

            }
            catch (Exception e)
            {
                return BadRequest(e);

            }
            return Ok("Students successfully promoted");
        }
        //3.2
        [HttpPost("Enroll")]
        public IActionResult EnrollStudent(EnrollStudentRequest request)
        {
            try
            {
                //Check if all required data has been delivered. 
                if (string.IsNullOrWhiteSpace(request.IndexNumber) || string.IsNullOrWhiteSpace(request.FirstName) || string.IsNullOrWhiteSpace(request.LastName) || string.IsNullOrWhiteSpace(request.Studies))
                {
                    return NotFound("Not enough data");
                }
                //check if studies from the request exists in the Studies table.
                var res =( from stu in _studentContext.Studies where stu.Name.Equals(request.Studies) select stu).ToList();
                if (res.Count() == 0) return NotFound("Studies doesnt exist");
                var idStudies = res.ToList().First().IdStudy;

                //For existing studies, find the latest entry in the Enrollments table with value Semester = 1
                var lastEnrollment = (from e in _studentContext.Enrollment where (e.IdStudy.Equals(idStudies)) && (e.Semester == 1) orderby e.IdEnrollment descending select e.IdEnrollment).ToList();
               
                //If such record doesn’t exist, we must insert it with StartDate set as a current date
                if (lastEnrollment.Count==0)
                {
                    var max = _studentContext.Enrollment.Max(c => c.IdEnrollment);
                    Enrollment en = new Enrollment
                    {
                        IdEnrollment = max+1,
                        IdStudy = idStudies,
                        Semester = 1,
                        StartDate = DateTime.Now
                    };

                    var res2 = _studentContext.Enrollment.Add(en);

                }
                var maxEnrollment = _studentContext.Enrollment.Max(c => c.IdEnrollment);
                //In the next step a new Student must be created.
                //Remember about checking if index number provided in the request is not assigned to other student(otherwise return an error).

                var res3 = _studentContext.Student.Where(s => s.IndexNumber == request.IndexNumber).Select(s => s.IndexNumber).ToList();
                if (res3.Count!=0)
                {
                    return BadRequest("Student with given index already exists");

                }

                Student s = new Student
                {
                    IndexNumber = request.IndexNumber,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    BirthDate = request.Birthdate,
                    IdEnrollment = maxEnrollment
                };
                _studentContext.Add(s);
                _studentContext.SaveChanges();
            }
            catch (Exception exc)
            {
                return BadRequest(exc);
            }
            return Ok("Enrollment successfull");
        }
                
    }

}

