using Microsoft.AspNetCore.Mvc;
using s19515_test1.DTO;
using s19515_test1.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace s19515_test1.Services
{
    public class SqlDbService : ControllerBase, IDbService
    {
        public IActionResult deleteDataAboutProject(int id)
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
                        if (string.IsNullOrWhiteSpace(id.ToString()))
                        {
                            return BadRequest("You need to provide the id");
                        }

                        //Check if this project exists
                        com.CommandText = "Select IdProject from Project WHERE IdProject=@id1;";
                        com.Parameters.AddWithValue("id1", id);
                        var dr = com.ExecuteReader();
                        if (!dr.Read())
                        {
                            dr.Close();
                            return BadRequest("No project found");
                        }
                        dr.Close();

                        //Check if the project has some tasks
                        com.CommandText = "select p.IdProject, t.IdTask from Project p join Task t on p.IdProject=t.IdProject where p.IdProject=@id3";
                        com.Parameters.AddWithValue("id3", id);
                        dr = com.ExecuteReader();
                        if (!dr.Read())
                        {
                            Console.WriteLine("No tasks assined to this project");
                            dr.Close();
                        }
                        //In case the project has some tasks, delete data about those tasks
                        else
                        {
                            dr.Close();
                            com.CommandText = "delete from Task where IdProject =@id4;";
                            com.Parameters.AddWithValue("id4", id);
                            com.ExecuteNonQuery();
                        }
                        //Delete data about a given project
                        com.CommandText = "DELETE FROM Project WHERE IdProject=@id2;";
                        com.Parameters.AddWithValue("id2", id);
                        com.ExecuteNonQuery();

                        tran.Commit();
                        return Ok("Delete successfull");
                    }
                    catch (SqlException exc)
                    {
                        tran.Rollback();
                        return BadRequest(exc);
                    }
                }
            }
        }

        public IActionResult getTeamMemberAndHisTasks(int id)
        {
            using (var con = new SqlConnection(@"Data Source=db-mssql;Initial Catalog=s19515;Integrated Security=True"))
            {
                using (var com = new SqlCommand())
                {
                    com.Connection = con;
                    con.Open();

                    //Check if all required data has been delivered. 
                    if (string.IsNullOrWhiteSpace(id.ToString()))
                    {
                        return BadRequest("You need to provide the id");
                    }
                    //check if team member exists
                    com.CommandText = "Select IdTeamMember from TeamMember where IdTeamMember=@Index1";
                    com.Parameters.AddWithValue("index1", id);

                    var dr = com.ExecuteReader();
                    if (!dr.Read())
                    {
                        dr.Close();
                        return StatusCode(401);
                    }
                    //if he exists, return information about him
                    dr.Close();
                    com.CommandText = "Select * from TeamMember where IdTeamMember=@Index2";
                    com.Parameters.AddWithValue("index2", id);
                    dr = com.ExecuteReader();
                    var _teamMemberResponse = new TeamMemberResponse();
                    _teamMemberResponse.tasksListAssigned = new List<MojTask>();
                    _teamMemberResponse.tasksListCreated = new List<MojTask>();

                    while (dr.Read())
                        {
                            var t = new TeamMember
                            {
                                IdTeamMember = int.Parse(dr["IdTeamMember"].ToString()),
                                FirstName = dr["FirstName"].ToString(),
                                LastName = dr["LastName"].ToString(),
                                Email = dr["Email"].ToString()

                            };
                        _teamMemberResponse.teammember = t;
                     }
                    dr.Close();
           
                    //Tasks assigned to him
                    com.CommandText = "SELECT t.IdTask, t.Name, t.Description, t.Deadline, tt.Name ,p.Name from Task t join TaskType tt on t.IdTaskType = tt.IdTaskType join Project p on t.IdProject = p.IdProject where t.IdAssignedTo = @id3 order by t.Deadline DESC;";
                    com.Parameters.AddWithValue("id3", id);
                    dr = com.ExecuteReader();
                    //check if there are any tasks assgined
                    if (!dr.Read())
                    {
                        dr.Close();
                    }
                    else
                    {
                        while (dr.Read())
                        {
                            var mt = new MojTask
                            {
                                IdTask = (int)dr[0],
                                Name = dr[1].ToString(),
                                Description = dr[2] as string,
                                Deadline = (DateTime)dr[3],
                                TaskType = dr[4] as string,
                                ProjectName =  dr[5] as string
                            };
                            if (mt != null)
                                _teamMemberResponse.tasksListAssigned.Add(mt);
                            else Console.WriteLine("mt empty");
                        }
                    }
                    dr.Close();

                    //Tasks created by him
                    com.CommandText = "SELECT t.IdTask, t.Name, t.Description, t.Deadline, tt.Name ,p.Name from Task t join TaskType tt on t.IdTaskType = tt.IdTaskType join Project p on t.IdProject = p.IdProject where t.IdCreator = @id4 order by t.Deadline DESC;";
                    com.Parameters.AddWithValue("id4", id);
                    dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        var mt = new MojTask
                        {
                            IdTask = (int)dr[0],
                            Name = dr[1].ToString(),
                            Description = dr[2] as string,
                            Deadline = (DateTime)dr[3],
                            TaskType = dr[4] as string,
                            ProjectName = dr[5] as string
                        };
                        if (mt != null)
                            _teamMemberResponse.tasksListCreated.Add(mt);
                        else Console.WriteLine("mt empty");
                      
                    }

                    return Ok(_teamMemberResponse);
                    
                }
            }
        }
    }
}
