using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DTO;
using WebApplication1.Models;
using System.Net;


namespace WebApplication1.Service
{
    public class DbAnimals : ControllerBase,IDbAnimals
    {
        public IActionResult AddAnimal(AnimlaRequest request)
        {
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
                        if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Type) || string.IsNullOrWhiteSpace(request.IdOwner.ToString()))
                        {
                            tran.Rollback();
                            return StatusCode(401);
                        }




                        //create the animal
                        string aN = request.Name;
                        string aT = request.Type;
                        DateTime aAD = request.AdmissionDate;
                        int aOID = request.IdOwner;
         

                        //add the animal
                        com.CommandText = "INSERT INTO Animal VALUES(@name, @TYPE, @Aday, @Owner);";
                        com.Parameters.AddWithValue("name", aN);
                        com.Parameters.AddWithValue("TYPE", aT);
                        com.Parameters.AddWithValue("Aday", aAD);
                        com.Parameters.AddWithValue("Owner", aOID);

                        com.ExecuteNonQuery();

                        //if the list of procedures was given, add it to database
                        if (!string.IsNullOrWhiteSpace(request.procedures.ToString()))
                        {
                           
                            foreach (Procedure p in request.procedures)
                            {
                                com.CommandText = "INSERT INTO Procedure_Animal VALUES(@ProcId, (select IdAnimal from Animal where Name=@anName), @procD)";
                                com.Parameters.AddWithValue("ProcId", p.IdProcedure);
                                com.Parameters.AddWithValue("anName", request.Name);
                                com.Parameters.AddWithValue("procD", p.procedureDate);

                                com.ExecuteNonQuery();

                            }
                        }

                        tran.Commit();

                        return Ok(200);
                    }
                    catch (SqlException exc)
                    {
                        tran.Rollback();
                        return StatusCode(401);
                    }
                }
            }
        }

        public IActionResult getAnimals(string orderBy)
        {
            using (var con = new SqlConnection(@"Data Source=db-mssql;Initial Catalog=s19515;Integrated Security=True"))
            {
                using (var com = new SqlCommand())
                {
                    com.Connection = con;
                    if (String.IsNullOrWhiteSpace(orderBy)) { orderBy = "a.AdmissionDate_DESC;"; }
                    con.Open();
                    string[] ordrbyArray =orderBy.Split('_');
                    com.CommandText = $"Select a.Name, a.AdmissionDate, a.Type, o.LastName from Animal a join Owner o on a.IdOwner=o.IdOwner order by {ordrbyArray[0]} {ordrbyArray[1]}";
                    com.Parameters.AddWithValue("givenString", orderBy);

                    var dr = com.ExecuteReader();
                    if (!dr.Read())
                    {
                        dr.Close();
                        return StatusCode(400);
                    }
                    else
                    {
                        List<Animal> animals = new List<Animal>();
                        while (dr.Read())
                        {
                            var a = new Animal
                            {
                                Name = dr["Name"].ToString(),
                                AdmissionDate = DateTime.Parse(dr["AdmissionDate"].ToString()),
                                LastNameOfTheOwner = dr["LastName"].ToString(),
                                Type = dr["Type"].ToString()

                            };
                            animals.Add(a);
                        }
                        return Ok(animals);
                    }
                }
            }
        }
    }
}
