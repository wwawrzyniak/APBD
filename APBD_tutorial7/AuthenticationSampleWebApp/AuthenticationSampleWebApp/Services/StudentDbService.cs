﻿using AuthenticationSampleWebApp.DTOs;
using AuthenticationSampleWebApp.DTOs.Requests;
using AuthenticationSampleWebApp.DTOs.Responses;
using AuthenticationSampleWebApp.Models;
using AuthenticationSampleWebApp.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

public class SqlServerStudentDbService : ControllerBase, IStudentDbService
{

    public SqlServerStudentDbService(/*.. */ )
    {

    }

    public IActionResult EnrollStudent(EnrollStudentRequest request)
    {
        // var st = new Student();
        //st.FirstName = request.FirstName;

        using (var sqlConnection = new SqlConnection(@"Data Source=db-mssql;Initial Catalog=s19515;Integrated Security=True"))
        {
            using (var com = new SqlCommand())
            {
                com.Connection = sqlConnection;

                sqlConnection.Open();
                var tran = sqlConnection.BeginTransaction();
                com.Transaction = tran;

                try
                {
                    //Check if all required data has been delivered. 
                    if (string.IsNullOrWhiteSpace(request.IndexNumber) || string.IsNullOrWhiteSpace(request.FirstName) || string.IsNullOrWhiteSpace(request.LastName) || string.IsNullOrWhiteSpace(request.Studies))
                    {
                        tran.Rollback();
                        return NotFound("Not enough data");
                    }
                    //check if studies from the request exists in the Studies table.
                    com.CommandText = "select IdStudy from Studies where name=@name";
                    com.Parameters.AddWithValue("name", request.Studies);

                    var dr = com.ExecuteReader();
                    if (!dr.Read())
                    {
                        dr.Close();
                        tran.Rollback();
                        return BadRequest("Studies donesnt exist");
                    }

                    int idstudy = (int)dr["IdStudy"];
                    dr.Close();
                    com.ExecuteNonQuery();

                    //For existing studies, find the latest entry in the Enrollments table with value Semester = 1
                    com.CommandText = "select max(IdEnrollment) from Enrollment where IdStudy=@idstudy and Semester=1";
                    com.Parameters.AddWithValue("idstudy", idstudy);
                    dr.Close();
                    //If such record doesn’t exist, we must insert it with StartDate set as a current date
                    dr = com.ExecuteReader();
                    DateTime startDate;
                    if (!dr.Read())
                    {
                        startDate = DateTime.Now;
                        com.CommandText = "insert into Enrollment values ((Select max(IdEnrollment)+1 from Enrollment), 1, @ids, @sd)";
                        com.Parameters.AddWithValue("ids", idstudy);
                        com.Parameters.AddWithValue("sd", startDate);

                    }
                    dr.Close();
                    com.ExecuteNonQuery();
                    dr.Close();
                    //In the next step a new Student must be created.

                    string IndexN = request.IndexNumber;
                    string Fname = request.FirstName;
                    string Lname = request.LastName;
                    DateTime Bdate = request.Birthdate;

                    //Remember about checking if index number provided in the request is not assigned to other student(otherwise return an error).
                    com.CommandText = "select * from student where IndexNumber=@id";
                    com.Parameters.AddWithValue("id", IndexN);
                    dr = com.ExecuteReader();
                    if (dr.Read())
                    {
                        dr.Close();
                        tran.Rollback();
                        return BadRequest("Student with this index already exists");
                    }
                    dr.Close();
                    com.ExecuteNonQuery();
                    com.CommandText = "INSERT INTO Student(IndexNumber, FirstName, LastName, BirthDate, IdEnrollment) VALUES(@Index, @Fname, @Lname, @Bday, (Select max(IdEnrollment) from Enrollment))";
                    com.Parameters.AddWithValue("Index", IndexN);
                    com.Parameters.AddWithValue("Fname", Fname);
                    com.Parameters.AddWithValue("Lname", Lname);
                    com.Parameters.AddWithValue("Bday", Bdate);

                    com.ExecuteNonQuery();
                    tran.Commit();
                    var res = new EnrollStudentResponse();
                    res.Semester = 1;
                    res.LastName = request.LastName;
                    return CreatedAtAction("EnrollStudent", res);
                }
                catch (SqlException exc)
                {
                    tran.Rollback();
                }

            }
            return NotFound("");
        }
    }

    public IActionResult LoginCheck(LoginRequestDto request)
    {
        using var sqlConnection = new SqlConnection(@"Data Source=db-mssql;Initial Catalog=s19515;Integrated Security=True");
        {
            using (var command = new SqlCommand())
            {
                try { 
                
                    //decode the string password given by user eg. alamakota to check against the secure password stored in database
                    var pass = request.Haslo;

                    //I couldnt understand how it should work with random salt in every connection so I set fixed salt(?????)
                     var salt = "b3w5ucH/iQuAmwYUBnmXeQ==";
                     byte[] randomBytes = new byte[128 / 8];

                   /*  using (var generator = RandomNumberGenerator.Create())
                     {
                         generator.GetBytes(randomBytes);
                         salt = Convert.ToBase64String(randomBytes);
                     }*/


                     var valueBytes = KeyDerivation.Pbkdf2(password: pass,
                        salt: Encoding.UTF8.GetBytes(salt),
                        prf: KeyDerivationPrf.HMACSHA512,
                        iterationCount: 10000,
                        numBytesRequested: 256 / 8);

                     //password is now as hashed in database - now we can verify login
                     var newPass = Convert.ToBase64String(valueBytes);

                     command.Connection = sqlConnection;
                     command.CommandText = "select s.FirstName, s.LastName from student s where s.IndexNumber=@index and s.password=@pass";
                     command.Parameters.AddWithValue("index", request.Login);
                     command.Parameters.AddWithValue("pass", newPass);
                     sqlConnection.Open();

                     var response = command.ExecuteReader();
                     if (!response.Read())
                     {

                         return StatusCode(401);
                     }
                     else
                     {
                         var student = new Student
                         {
                             FirstName = response["FirstName"].ToString(),
                             LastName = response["LastName"].ToString(),
                             IdStudent = request.Login,
                             Password = request.Haslo
                         };

                         return Ok(200);
                         // return CreatedAtAction("LoginCheck", student);
 

                     }
                }
                catch (SqlException ex)
                {

                }
                return StatusCode(401);
            }
        }
    }

    public IActionResult PromoteStudents(PromoteStudentRequest request)
    {

        using (var sqlConnection = new SqlConnection(@"Data Source=db-mssql;Initial Catalog=s19515;Integrated Security=True"))
        {
            using (var com = new SqlCommand("promote", sqlConnection))
            {
                using (var comm = new SqlCommand())
                {
                    comm.Connection = sqlConnection;
                    com.Connection = sqlConnection;

                    sqlConnection.Open();
                    var tran = sqlConnection.BeginTransaction();
                    com.Transaction = tran;
                    comm.Transaction = tran;

                    try
                    {
                        //Check if all required data has been delivered. 
                        if (string.IsNullOrEmpty(request.Semester.ToString()) || string.IsNullOrWhiteSpace(request.StudyName))
                        {
                            tran.Rollback();
                            return NotFound("Not enough data");
                        }
                        //Check if Enrollment table contains provided Studies and Semester. Otherwise return 404 (Not Found).
                        comm.CommandText = "select Name from Studies s join Enrollment e on e.IdStudy=s.IdStudy where s.Name=@name";
                        comm.Parameters.AddWithValue("name", request.StudyName);

                        var dr = comm.ExecuteReader();
                        if (!dr.Read())
                        {
                            dr.Close();
                            tran.Rollback();
                            return BadRequest("Studies donesnt exist");
                        }
                        dr.Close();
                        comm.ExecuteNonQuery();
                        comm.CommandText = "select Semester from Enrollment where Semester=@sem";
                        comm.Parameters.AddWithValue("sem", request.Semester);

                        dr = comm.ExecuteReader();
                        if (!dr.Read())
                        {
                            dr.Close();
                            tran.Rollback();
                            return BadRequest("Semester donesnt exist");
                        }
                        dr.Close();
                        comm.ExecuteNonQuery();

                        //var dr2 = com.ExecuteNonQuery();
                        com.CommandType = CommandType.StoredProcedure;
                        //com.CommandText = "promote";
                        com.Parameters.Add("@Semester", SqlDbType.Int);
                        com.Parameters.Add("@StudyName", SqlDbType.VarChar, 10);
                        com.Parameters.Add("@exit", SqlDbType.Int).Direction = ParameterDirection.Output;
                        com.Parameters["@Semester"].Value = request.Semester;
                        com.Parameters["@StudyName"].Value = request.StudyName;

                        dr.Close();
                        com.ExecuteNonQuery();

                        int newId = Convert.ToInt32(com.Parameters["@exit"].Value);


                        comm.CommandText = "SELECT IdEnrollment, s.Name, e.Semester FROM Enrollment e join Studies s on e.IdEnrollment= IdEnrollment WHERE IdEnrollment = @newId";
                        comm.Parameters.AddWithValue("newId", newId);
                        //nic nie zwraca/
                        dr = comm.ExecuteReader();
                        if (!dr.Read())
                            return NotFound("Puste");

                        int iden = int.Parse(dr["IdEnrollment"].ToString());
                        string sName = dr["Name"].ToString();
                        int sem = int.Parse(dr["Semester"].ToString());
                        var resp = new PromoteStudentResponse();
                        dr.Close();
                        resp.IdEnrollment = iden;
                        resp.StudyName = sName;
                        resp.Semester = sem;
                        return CreatedAtAction("PromoteStudents", resp);
                    }
                    catch (SqlException exc)
                    {
                        tran.Rollback();
                    }

                }
                return NotFound("");
            }
        }
    }
    
    //decode plain string password int hashed one (should be called for example after a new user is added to the db for the first time)
    public IActionResult SecureMyPassword(passwordRequest request)
    {

            using var sqlConnection = new SqlConnection(@"Data Source=db-mssql;Initial Catalog=s19515;Integrated Security=True");
            {
                using (var command = new SqlCommand())
                {
                    try
                    {
                        command.Connection = sqlConnection;
                        command.CommandText = "select s.Password from student s where s.IndexNumber=@index";
                        command.Parameters.AddWithValue("index", request.Login);
                        sqlConnection.Open();

                        var response = command.ExecuteReader();
                        if (!response.Read())
                        {

                            return StatusCode(400);
                        }
                        else
                        {

                            var pass = response["Password"].ToString();
                            //create salt
                            var salt = "b3w5ucH/iQuAmwYUBnmXeQ==";
                           /* byte[] randomBytes = new byte[128 / 8];
                            using (var generator = RandomNumberGenerator.Create())
                            {
                                generator.GetBytes(randomBytes);
                                salt = Convert.ToBase64String(randomBytes);
                            }
                            */
                            var valueBytes = KeyDerivation.Pbkdf2(password: pass,
                               salt: Encoding.UTF8.GetBytes(salt),
                               prf: KeyDerivationPrf.HMACSHA512,
                               iterationCount: 10000,
                               numBytesRequested: 256 / 8);

                            var newPass = Convert.ToBase64String(valueBytes);
                        var t = newPass.GetType();
                        Console.WriteLine(t);
                            response.Close();

                            command.CommandText = "update Student set password=@new where IndexNumber=@index2";
                            command.Parameters.AddWithValue("new", newPass);
                            command.Parameters.AddWithValue("index2", request.Login);
                            response = command.ExecuteReader();
      
                             return Ok(200);
                        }

                    }
                    catch (SqlException ex)
                    {

                    }
                    return StatusCode(401);
                }
            }

        
    }
}




