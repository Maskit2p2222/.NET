using System.Collections.Generic;
using System.Data.SqlClient;
using VinsUncoderLibrary.Models;

namespace VinsUncoderLibrary.DataBase
{
    public class MarksDataBase
    {
        private readonly static string connectionString = StaticConfigValues.CONNECTION_STRING;


        public static List<MarksView> GetMarksViews()
        {
            string commandString = @"
            SELECT 
	            m.Mark AS Mark,
	            m.MeaningfulMask AS MeaningfulMask,
	            COUNT(vd.VinPartDescription) AS CountOfVinDescriptions,
	            m.MarkId
            FROM Marks m
            LEFT JOIN VinsDescriptions vd ON (vd.MarkId = m.MarkId)
            GROUP BY m.Mark, m.MeaningfulMask, m.MarkId
            ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(commandString, connection))
                {
                    List<MarksView> MarksView = new List<MarksView>();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                MarksView.Add(new Models.MarksView
                                {
                                    Mark = reader["Mark"].ToString(),
                                    CountOfVinDescriptions = (int)reader["CountOfVinDescriptions"],
                                    MarkId = (int)reader["MarkId"],
                                    MeaningfulMask = reader["MeaningfulMask"].ToString()
                                });
                            }
                        }
                    }
                    connection.Close();
                    return MarksView;
                }
            }
        }
        
       
        public static List<string> GetAllMarks()
        {
            string commandString = @"
            SELECT
	            m.Mark
             FROM Marks m
            ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(commandString, connection))
                {
                    List<string> Marks = new List<string>();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Marks.Add(reader["Mark"].ToString());
                            }
                        }
                    }
                    connection.Close();
                    return Marks;
                }
            }
        }

        public static List<string> GetAllMarksByLike(string PartOfMark)
        {
            string commandString = @"
            SELECT
                m.Mark
             FROM Marks m
             WHERE m.Mark LIKE @PartOfMark
            ";
            PartOfMark = "%" + PartOfMark + "%";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(commandString, connection))
                {
                    List<string> Marks = new List<string>();
                    command.Parameters.Add("@PartOfMark", System.Data.SqlDbType.NVarChar).Value = PartOfMark;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Marks.Add(reader["Mark"].ToString());
                            }
                        }
                    }
                    connection.Close();
                    return Marks;
                }
            }
        }


        public static string GetMarkIdByMark(string Mark)
        {
            string commandString = @"
            SELECT
	            m.MarkId
             FROM Marks m
             WHERE (m.Mark = @Mark)
            ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(commandString, connection))
                {
                    command.Parameters.Add("@Mark", System.Data.SqlDbType.NVarChar).Value = Mark;
                    int MarkId = 0;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                MarkId = (int)reader["MarkId"];
                            }
                        }
                        
                    }
                    connection.Close();
                    return MarkId.ToString();
                }
            }
        }

        public static string GetMeaningfulMaskbyMark(string Mark)
        {
            string commandString = @"
            SELECT
	            m.MeaningfulMask
             FROM Marks m
             WHERE (m.Mark = @Mark)
            ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(commandString, connection))
                {
                    command.Parameters.Add("@Mark", System.Data.SqlDbType.NVarChar).Value = Mark;
                    string MeaningfulMask = null;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                MeaningfulMask = reader["MeaningfulMask"].ToString();
                            }
                        }
                    }
                    connection.Close();
                    return MeaningfulMask;


                }
            }
        }

        public static void AddNewMark(string Mark)
        {
            string commandString = @"
            INSERT INTO Marks (Mark) VALUES (@Mark)
            ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(commandString, connection))
                {
                    command.Parameters.Add("@Mark", System.Data.SqlDbType.NVarChar).Value = Mark;
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public static bool CheckMarkByMarkName(string Mark)
        {
            string commandString = @"
            SELECT
	            m.Mark
             FROM Marks m
             WHERE (m.Mark = @Mark)
            ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(commandString, connection))
                {
                    command.Parameters.Add("@Mark", System.Data.SqlDbType.NVarChar).Value = Mark;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            connection.Close();
                            return true;
                        }
                        else
                        {
                            connection.Close();
                            return false;
                        }
                    }
                }
            }
        }

        public static void AddMeaningfulMask(string Mask, string Mark)
        {
            string commandString = @"
              UPDATE m
              SET m.MeaningfulMask = @MeaningfulMask
              FROM Marks m
              WHERE (m.Mark = @Mark)
            ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(commandString, connection))
                {
                    command.Parameters.Add("@MeaningfulMask", System.Data.SqlDbType.NVarChar).Value = Mask;
                    command.Parameters.Add("@Mark", System.Data.SqlDbType.NVarChar).Value = Mark;
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
    }
}
