using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Temp.Models;

namespace Temp.DataBase
{
    public class RenderPagesDataBase
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["VinWebFormsApp"].ConnectionString;

        public static List<PageLink> GetAllPageLinks()
        {
            string sqlCommand = "Select PageName, RoleName From PageLinks";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                List<PageLink> pageLinks = new List<PageLink>();
                using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                pageLinks.Add(new PageLink
                                {
                                    PageName = reader["PageName"].ToString(),
                                    RoleName = reader["RoleName"].ToString()
                                });
                            }
                        }
                        connection.Close();
                        return pageLinks;
                    }
                }
            }
        }
    }
}