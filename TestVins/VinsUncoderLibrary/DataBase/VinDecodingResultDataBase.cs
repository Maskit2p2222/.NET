using System.Collections.Generic;
using System.Data.SqlClient;
using VinsUncoderLibrary.Models;

namespace VinsUncoderLibrary.DataBase
{
    public class VinDecodingResultDataBase
    {
        private readonly static string ConnectionString = StaticConfigValues.CONNECTION_STRING;
        public static void AddRangeOfResults(List<VinPartDecodingResult> resultTables)
        {
            string sqlCommand = "INSERT INTO VinDecodingResult (Vin, EnumMeaning, VinPartDescription)" +
                " VALUES (@Vin, @EnumMeaning, @VinPartDescription)";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                foreach (VinPartDecodingResult result in resultTables)
                {
                    using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                    {
                        //command.Parameters.AddWithValue("@Vin", result.Vin);
                        command.Parameters.Add("@Vin", System.Data.SqlDbType.NVarChar).Value = result.Vin;
                        //command.Parameters.AddWithValue("@EnumMeaning", result.EnumMeaning);
                        command.Parameters.Add("@EnumMeaning", System.Data.SqlDbType.Int).Value = result.EnumMeaning;
                        //command.Parameters.AddWithValue("@VinPartDescription", result.VinPartDescription);
                        command.Parameters.Add("@VinPartDescription", System.Data.SqlDbType.NVarChar).Value = result.VinPartDescription;
                        command.ExecuteNonQuery();
                    }
                }
                connection.Close();

            }
        }

        public static void AddRangeOfResults(List<VinPartDecodingResult> resultTables, SqlTransaction transaction, SqlConnection connection)
        {
            string sqlCommand = "INSERT INTO VinDecodingResult (Vin, EnumMeaning, VinPartDescription)" +
                " VALUES (@Vin, @EnumMeaning, @VinPartDescription)";
            foreach (VinPartDecodingResult result in resultTables)
            {

                using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                {
                    command.Transaction = transaction;
                    //command.Parameters.AddWithValue("@Vin", result.Vin);
                    command.Parameters.Add("@Vin", System.Data.SqlDbType.NVarChar).Value = result.Vin;
                    //command.Parameters.AddWithValue("@EnumMeaning", result.EnumMeaning);
                    command.Parameters.Add("@EnumMeaning", System.Data.SqlDbType.Int).Value = result.EnumMeaning;
                    //command.Parameters.AddWithValue("@VinPartDescription", result.VinPartDescription);
                    command.Parameters.Add("@VinPartDescription", System.Data.SqlDbType.NVarChar).Value = result.VinPartDescription;
                    command.ExecuteNonQueryAsync();
                }

            }
        }

        public static List<VinPartDecodingResult> GetResultsTableByVin(Vin vin)
        {
            string sqlCommand = "SELECT Vin, EnumMeaning, VinPartDescription  FROM " +
                "VinDecodingResult " +
                "WHERE Vin = @Vin";
            List<VinPartDecodingResult> results = new List<VinPartDecodingResult>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                {
                    //command.Parameters.AddWithValue("@Vin", vin.VinTextValue);
                    command.Parameters.Add("@Vin", System.Data.SqlDbType.NVarChar).Value = vin.VinTextValue;
                    using (
                        SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                results.Add(new VinPartDecodingResult
                                {
                                    Vin = (string)reader["Vin"],
                                    EnumMeaning = (TypeOfVinPartMeaning)reader["EnumMeaning"],
                                    VinPartDescription = (string)reader["VinPartDescription"]
                                });
                            }
                        }
                        connection.Close();
                        return results;
                    }

                }
            }

        }
    }

   
}
