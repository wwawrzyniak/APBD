using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Services
{
    public class SqlServerDbService : IDbService
    {
        public bool CheckIndexNumber(string index)
        {
            using (var con = new SqlConnection(@"Data Source=db-mssql;Initial Catalog=s19515;Integrated Security=True"))
            {
                using (var com = new SqlCommand())
                {
                    com.Connection = con;

                    con.Open();
                    com.CommandText = "Select indexNumber from Student where IndexNumber = @index";
                    com.Parameters.AddWithValue("index", index);
                    
                    var dr = com.ExecuteReader();
                   // int count = (int)dr["ilosc"];
                    if (!dr.Read())
                    {
                        dr.Close();
                        return false;
                    }
                    else return true;
                }
            }
        

        }

        //...
    }
}
