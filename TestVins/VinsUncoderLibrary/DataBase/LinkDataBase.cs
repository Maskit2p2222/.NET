using System.Collections.Generic;
using System.Data.SqlClient;
using VinsUncoderLibrary.Models;

namespace VinsUncoderLibrary.DataBase
{
    public class LinkDataBase
    {
        private readonly static string connectionString = StaticConfigValues.CONNECTION_STRING;
        //public LinkDataBase(string connectionString)
        //{            
        //    this.connectionString = connectionString;
        //}

        public static List<VinDescriptionsLink> GetLinkTablesByIdOfDescription(int IdOfDescription)
        {
            string sqlCommand = "SELECT IdOfDescriptionFirst, IdOfDescriptionSecond FROM VinDescriptionsLinks " +
                "WHERE IdOfDescriptionFirst = @IdOfDescription";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                List<VinDescriptionsLink> links = new List<VinDescriptionsLink>();
                using (SqlCommand command = new SqlCommand(sqlCommand, connection)) {
                command.Parameters.AddWithValue("@IdOfDescription", IdOfDescription);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                links.Add(new VinDescriptionsLink
                                {
                                    IdOfDescriptionFirst = (int)reader["IdOfDescriptionFirst"],
                                    IdOfDescriptionSecond = (int)reader["IdOfDescriptionSecond"]
                                });
                            }
                        }
                        connection.Close();
                        return links;
                    }
                }
                
            }
        }

        public static void AddNewLink(VinDescriptionsLink vinDescriptionsLink)
        {
            string commandString = @"
            INSERT INTO VinDescriptionsLinks VALUES (@IdOfDescriptionFirst, @IdOfDescriptionSecond);
            ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(commandString, connection))
                {
                    command.Parameters.Add("@IdOfDescriptionFirst", System.Data.SqlDbType.Int).Value = vinDescriptionsLink.IdOfDescriptionFirst;
                    command.Parameters.Add("@IdOfDescriptionSecond", System.Data.SqlDbType.Int).Value = vinDescriptionsLink.IdOfDescriptionSecond;
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
    }
}
