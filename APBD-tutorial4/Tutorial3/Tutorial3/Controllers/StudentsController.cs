using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Tutorial3.Models;

namespace Tutorial3.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetStudents()
        {
            var students = new List<Student>();
            using (var sqlConnection = new SqlConnection(@"Data Source=db-mssql;Initial Catalog=s19515;Integrated Security=True"))
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = sqlConnection;
                    command.CommandText = "select s.FirstName, s.LastName, s.BirthDate, st.Name as Studies, e.Semester " +
                                            "from Student s " +
                                            "join Enrollment e on e.IdEnrollment = s.IdEnrollment " +
                                            "join Studies st on st.IdStudy = e.IdStudy; ";
                    sqlConnection.Open();
                    var response = command.ExecuteReader();
                    while (response.Read())
                    {
                        //var st = new Student();
                        //st.FirstName = response["FirstName"].ToString();
                        //st.LastName = response["LastName"].ToString();
                        //st.Studies = response["Studies"].ToString();
                        //st.BirthDate = DateTime.Parse(response["BirthDate"].ToString());
                        //st.Semester = int.Parse(response["Semester"].ToString());

                        var st = new Student
                        {
                            FirstName = response["FirstName"].ToString(),
                            LastName = response["LastName"].ToString(),
                            Studies = response["Studies"].ToString(),
                            BirthDate = DateTime.Parse(response["BirthDate"].ToString()),
                            Semester = int.Parse(response["Semester"].ToString())
                        };

                        students.Add(st);
                    }
                }
            }
            return Ok(students);
        }
        //entries for the semester - enrollment id, date, studies name and semester 
        //string index = "s19515";
        [HttpGet("{index}")]
        public IActionResult GetEntrollment(string index)
        {
        //a list, because a student with given index can possibly study more than one thing 
            var enrollments = new List<Enrollment>();
            using (var sqlConnection = new SqlConnection(@"Data Source=db-mssql;Initial Catalog=s19515;Integrated Security=True"))
            {
                sqlConnection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = sqlConnection;
                    command.CommandText = "SELECT enrollment.IdEnrollment, enrollment.Semester, studies.Name, enrollment.StartDate"
                    +"from Enrollment enrollment"
                    +"join Studies studies on enrollment.IdStudy = studies.IdStudy"
                    +"join Student student on enrollment.IdEnrollment = student.IdEnrollment"
                    +"WHERE student.IndexNumber = @index";
                    command.Parameters.AddWithValue("index", index);
                    var response = command.ExecuteReader();
                    if (response.Read())
                    {
                        var enrollment = new Enrollment()
                        {
                            studiesName = response["Name"].ToString(),
                            Semester = Int32.Parse(response["Semester"].ToString()),
                            StartDate = DateTime.Parse(response["StartDate"].ToString()),
                            IdEnrollment = Int32.Parse(response["IdEnrollment"].ToString()),
                           
                        };
                        if (enrollment != null)
                            enrollments.Add(enrollment);

                    }
                    if (enrollments.Count == 0) return NotFound("");
                    else return Ok(enrollments);

                }
            }



            /*  [HttpGet("{id}")]
              public IActionResult GetStudent(int id)
              {
                  if (id == 1)
                  {
                      return Ok("Kowalski");
                  }
                  else if (id == 2)
                  {
                      return Ok("Malewski");
                  }
                  return NotFound("Cannot find the student");
              }
              */


            //[HttpPost]
            //public IActionResult CreateStudent(Student student)
            //{
            //    student.IndexNumber = $"s{new Random().Next(1, 20000)}";
            //    // _dbService.GetStudents().ToList().Add(student);
            //    return Ok(student);
            //}

            //[HttpPut("{id}")]
            //public IActionResult UpdateStudent(Student student, int id)
            //{
            //    if(student.idStudent != id)
            //    {
            //         return NotFound("Student Not Found");
            //    }
            //    // updating object 
            //    // student.FirstName = "James";
            //    // _dbService.GetStudents().ToList().Insert(id, student);
            //    return Ok("Update completed");
            //}

            //[HttpDelete("{id}")]
            //public IActionResult DeleteStudent(int id)
            //{
            //    if (id == null)
            //    {
            //        return NotFound("Student Not Found");
            //    }
            //    // deleting object 
            //    //_dbService.GetStudents().ToList().RemoveAt(id);
            //    return Ok("Delete completed");
            //}
        }
    }
}
