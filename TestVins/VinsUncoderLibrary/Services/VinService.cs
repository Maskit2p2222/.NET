using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using VinsUncoderLibrary.DataBase;
using VinsUncoderLibrary.Models;

namespace VinsUncoderLibrary.Services
{
    public class VinService
    {
        private readonly static string connectionString = @"
        Data Source=DESKTOP-FLV2ICD\SQLEXPRESS;Initial Catalog=AllVins;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False
        ";

        public static void MultiThreadedVinDecoding()
        {
            string sqlCommand = @"
                UPDATE TOP(5) Vins
                 SET (VinProgressStatus = 10)
                 OUTPUT inserted.VinTextValue
                 WHERE (VinProgressStatus = 0)
            ";
            while (true)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlTransaction transaction = connection.BeginTransaction();
                    
                    using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                    {
                        command.Transaction = transaction;
                        try
                        {
                            List<Vin> vins = new List<Vin>();
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        vins.Add(new Vin
                                        {
                                            VinTextValue = reader["VinTextValue"].ToString(),
                                        });
                                    }
                                }
                            }
                            List<Task> tasks = new List<Task>();
                            List<List<VinPartDecodingResult>> resultTables = new List<List<VinPartDecodingResult>>();
                            foreach (Vin vin in vins)
                            {
                                Uncoder uncoder = new Uncoder();
                                Task task = new Task(() =>
                                {
                                    resultTables.Add(uncoder.UncodeVinWhithReturn(vin));
                                });
                                
                                task.Start();
                                tasks.Add(task);
                            }
                            Task.WaitAll(tasks.ToArray());
                            foreach(List<VinPartDecodingResult> results in resultTables)
                            {
                                VinDecodingResultDataBase.AddRangeOfResults(results, transaction, connection);
                            }
                            string sqlUpdateCommand = @"
                                UPDATE Vins
                                 SET (VinProgressStatus = 20)
                                 WHERE (VinTextValue = @VinTextValue)
                            ";
                            
                            using (SqlCommand updateCommand = new SqlCommand(sqlUpdateCommand, connection))
                            {
                                updateCommand.Transaction = transaction;
                                foreach (Vin vin in vins)
                                {
                                    updateCommand.Parameters.Clear();
                                    updateCommand.Parameters.Add("@VinTextValue", System.Data.SqlDbType.NVarChar).Value = vin.VinTextValue;
                                    updateCommand.ExecuteNonQuery();
                                }
                            }
                            if (!SelectCheck(transaction, connection))
                            {
                                transaction.Commit();
                                connection.Close();
                                break;
                            }
                            else
                            {
                                transaction.Commit();
                                connection.Close();
                            }


                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            transaction.Rollback();
                            break;
                        }
                    }
                }
            }
        }

        public static bool SelectCheck(SqlTransaction transaction, SqlConnection connection)
        {
            string sqlCommand = @"
            SELECT
            	v.VinTextValue
             FROM Vins v
             WHERE (v.VinProgressStatus = 0)
            ";
            using (SqlCommand command = new SqlCommand(sqlCommand, connection))
            {
                command.Transaction = transaction;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    bool flag = reader.HasRows;
                    return flag;
                }
            }
        }
    }
}
