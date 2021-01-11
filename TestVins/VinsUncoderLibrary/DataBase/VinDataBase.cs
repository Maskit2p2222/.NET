//using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using VinsUncoderLibrary.Models;

namespace VinsUncoderLibrary.DataBase
{
    public class VinDataBase
    {
        private readonly static string ConnectionString = StaticConfigValues.CONNECTION_STRING;

        public static Vin GetVinById(string id)
        {
            string sqlCommand = " SELECT VinTextValue FROM Vins WHERE VinTextValue = @VinTextValue;";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                //Vin vin = connection.QueryFirstOrDefault<Vin>(sqlCommand, new { VinTextValue = id});
                Vin vin = new Vin();
                using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                {
                    // command.Parameters.AddWithValue("@VinTextValue", id);
                    command.Parameters.Add("@VinTextValue", SqlDbType.NVarChar).Value = id;
                    using (SqlDataReader dataReader = command.ExecuteReader()) // Почитать про преобразование типов в датаРидерах
                    {
                        if (dataReader.HasRows)
                        {
                            dataReader.Read();
                            vin.VinTextValue = dataReader["VinTextValue"].ToString(); // as && ConvertTo
                        }
                        connection.Close();
                        return vin;
                    }
                }
            }
        }

        public static void AddVin(Vin vin)
        {
            string sqlCommand = "INSERT INTO Vins (VinTextValue, VinProgressStatus) VALUES (@VinTextValue, 20);";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                //_ = connection.Query<Vin>(sqlCommand, new { vin.VinTextValue });
                using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                {
                    //command.Parameters.AddWithValue("@VinTextValue", vin.VinTextValue);
                    command.Parameters.Add("@VinTextValue", SqlDbType.NVarChar).Value = vin.VinTextValue;
                    command.ExecuteNonQuery();
                    connection.Close();                   
                }
            }
        }

        public static int GetCountOfAllVins()
        {
            string sqlCommand = @"
            SELECT DISTINCT
				v.VinTextValue AS Vin,
	            vdr1.VinPartDescription as Mark,
	            vdr2.VinPartDescription as Model,
	            vdr4.VinPartDescription as SerialNumber
             FROM Vins v
             INNER JOIN VinDecodingResult vdr1 ON (v.VinTextValue = vdr1.Vin)
             INNER JOIN VinDecodingResult vdr2 ON (v.VinTextValue = vdr2.Vin)
             INNER JOIN VinDecodingResult vdr4 ON (v.VinTextValue = vdr4.Vin)
             WHERE  (vdr1.EnumMeaning = 0)
             AND	(vdr2.EnumMeaning = 2)
             AND	(vdr4.EnumMeaning = 6)
            ";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                {
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        int countOfRows = 0;
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                countOfRows++;
                            }
                        }
                        return countOfRows;
                    }
                }
            }
        }


        public static List<VinRowToView> GetDistinctVinRowsWhithPaging(int fromPage, int toPage)
        {
            string sqlCommand = @"
            SELECT DISTINCT
	            v.VinTextValue,
	            vdr1.VinPartDescription as Mark,
	            vdr2.VinPartDescription as Model,
	            vdr4.VinPartDescription as SerialNumber
             FROM Vins v
             INNER JOIN VinDecodingResult vdr1 ON (v.VinTextValue = vdr1.Vin)
             INNER JOIN VinDecodingResult vdr2 ON (v.VinTextValue = vdr2.Vin)
             INNER JOIN VinDecodingResult vdr4 ON (v.VinTextValue = vdr4.Vin)
             WHERE  (vdr1.EnumMeaning = 0)
             AND	(vdr2.EnumMeaning = 2)
             AND	(vdr4.EnumMeaning = 6)
			 ORDER BY vdr1.VinPartDescription
			 OFFSET @fromPage ROWS FETCH NEXT @toPage ROWS ONLY;
            ";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                List<VinRowToView> vinRowsToView = new List<VinRowToView>();
                using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                {
                    command.Parameters.Add("@fromPage", SqlDbType.Int).Value = fromPage;
                    command.Parameters.Add("@toPage", SqlDbType.Int).Value = toPage;
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                vinRowsToView.Add(new VinRowToView
                                {
                                    VinValue = dataReader["VinTextValue"].ToString(),
                                    Mark = dataReader["Mark"].ToString(),
                                    Model = dataReader["Model"].ToString(),
                                    SerialNumber = dataReader["SerialNumber"].ToString(),
                                });
                            }
                        }
                        return vinRowsToView;
                    }
                }
            }

        }

        public static List<string> GetAllModelsOfVins()
        {
            string sqlCommand = @"
	        SELECT DISTINCT
		        vdr2.VinPartDescription as Model
	         FROM  VinDecodingResult vdr1
	         INNER JOIN VinDecodingResult vdr2 ON (vdr1.Vin = vdr2.Vin AND vdr2.EnumMeaning = 2)";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                // List<Vin> vins = connection.Query<Vin>(sqlCommand).ToList();
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                {
                    List<string> Models = new List<string>();
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Models.Add(dataReader["Model"].ToString());
                                
                            }
                        }
                        connection.Close();
                        return Models;
                    }
                }
            }

        }

        public static List<string> GetModelsByMark(string Mark)
        {
            string sqlCommand = @"
            SELECT DISTINCT
	            vdr2.VinPartDescription as Model
             FROM  VinDecodingResult vdr1
             INNER JOIN VinDecodingResult vdr2 ON (vdr1.Vin = vdr2.Vin AND vdr2.EnumMeaning = 2)
             INNER JOIN VinDecodingResult vdr4 ON (vdr1.Vin = vdr4.Vin AND vdr4.EnumMeaning = 6)
             WHERE  (vdr1.EnumMeaning = 0)
             AND	(vdr1.VinPartDescription = @Mark)
            ";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                // List<Vin> vins = connection.Query<Vin>(sqlCommand).ToList();
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                {
                    command.Parameters.Add("@Mark", SqlDbType.NVarChar).Value = Mark;
                    List<string> models = new List<string>();
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        int countOfRows = 0;
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                models.Add(dataReader["Model"].ToString());
                            }
                        }
                        connection.Close();
                        return models;
                    }
                }
            }
        }

        public static int PrGetCountOfRows(string markFilter = null, string modelFilter = null)
        {
            string sqlCommand = "[dbo].[prGetCountOfRows]";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                // List<Vin> vins = connection.Query<Vin>(sqlCommand).ToList();
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@MARK", SqlDbType.NVarChar).Value = markFilter ?? (object)DBNull.Value;
                    if (string.IsNullOrEmpty(modelFilter) || modelFilter == "")
                    {
                        command.Parameters.Add("@MODEL", SqlDbType.NVarChar).Value = (object)DBNull.Value;
                    }
                    else
                    {
                        command.Parameters.Add("@MODEL", SqlDbType.NVarChar).Value = modelFilter;
                    }
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        int countOfRows = 0;
                        if (dataReader.HasRows)
                        {
                            if (dataReader.Read())
                            {
                                countOfRows = (int)dataReader["CountOfRows"];
                            }
                        }
                        connection.Close();
                        return countOfRows;
                    }
                }
            }
        }

        public static List<VinRowToView> PrGetVinsWhithSortingAndFilters (int pageIndex, int pageSize, string markFilter = null,
            string modelFilter = null, string sorting = null)
        {
            string sqlCommand = "[dbo].PrGetVinsWhithSortingAndFilters";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                // List<Vin> vins = connection.Query<Vin>(sqlCommand).ToList();
                List<VinRowToView> vins = new List<VinRowToView>();
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@PAGEINDEX", SqlDbType.Int).Value = pageIndex;
                    command.Parameters.Add("@PAGESIZE", SqlDbType.Int).Value = pageSize;
                    command.Parameters.Add("@MARK", SqlDbType.NVarChar).Value = markFilter ?? (object)DBNull.Value;
                    if (string.IsNullOrEmpty(modelFilter) || modelFilter == "")
                    {
                        command.Parameters.Add("@MODEL", SqlDbType.NVarChar).Value = (object)DBNull.Value;
                    }
                    else
                    {
                        command.Parameters.Add("@MODEL", SqlDbType.NVarChar).Value = modelFilter;
                    }
                    command.Parameters.Add("@SORTING", SqlDbType.NVarChar).Value = sorting ?? (object)DBNull.Value;
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                vins.Add(new VinRowToView
                                {
                                    VinValue = dataReader["Vin"].ToString(),
                                    Mark = dataReader["Mark"].ToString(),
                                    Model = dataReader["Model"].ToString(),
                                    SerialNumber = dataReader["SerialNumber"].ToString(),
                                });
                            }
                        }
                        connection.Close();
                        return vins;
                    }
                }
            }
        }

        public static List<VinRowToView> GetAllVinRowsToView()
        {
            string sqlCommand = @"
            SELECT TOP(1000)
	            v.VinTextValue,
	            vdr1.VinPartDescription as Mark,
	            vdr2.VinPartDescription as Model,
	            vdr3.VinPartDescription as Year,
	            vdr4.VinPartDescription as SerialNumber
             FROM Vins v
             INNER JOIN VinDecodingResult vdr1 ON (v.VinTextValue = vdr1.Vin)
             INNER JOIN VinDecodingResult vdr2 ON (v.VinTextValue = vdr2.Vin)
             INNER JOIN VinDecodingResult vdr3 ON (v.VinTextValue = vdr3.Vin)
             INNER JOIN VinDecodingResult vdr4 ON (v.VinTextValue = vdr4.Vin)
             WHERE  (vdr1.EnumMeaning = 0)
             AND	(vdr2.EnumMeaning = 2)
             AND	(vdr3.EnumMeaning = 3)
             AND	(vdr4.EnumMeaning = 6)
            ";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                List<VinRowToView> vinRowsToView = new List<VinRowToView>();
                using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                {
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                vinRowsToView.Add(new VinRowToView
                                {
                                    VinValue = dataReader["VinTextValue"].ToString(),
                                    Mark = dataReader["Mark"].ToString(),
                                    Model = dataReader["Model"].ToString(),
                                    SerialNumber = dataReader["SerialNumber"].ToString(),
                                    Year = dataReader["Year"].ToString()
                                });
                            }
                        }
                        return vinRowsToView;
                    }
                }
            }
        }
        public static void AddVinRange(List<Vin> vins)
        {
            string sqlCommand = "INSERT INTO Vins (VinTextValue) VALUES (@VinTextValue);";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                foreach (Vin vin in vins)
                {
                    //_ = connection.Query<Vin>(sqlCommand, new { vin.VinTextValue });
                    using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                    {
                        //command.Parameters.AddWithValue("@VinTextValue", vin.VinTextValue);
                        command.Parameters.Add("@VinTextValue", SqlDbType.NVarChar).Value = vin.VinTextValue;
                        command.ExecuteNonQuery();
                        connection.Close();
                    }

                }

            }

        }

        public static void RemoveVin(Vin vin)
        {
            string sqlCommand = "DELETE FROM Vins WHERE VinTextValue = @VinTextValue";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                {
                    //command.Parameters.AddWithValue("@VinTextValue", vin.VinTextValue);
                    command.Parameters.Add("@VinTextValue", SqlDbType.NVarChar).Value = vin.VinTextValue;
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                //_ = connection.Query<Vin>(sqlCommand, new { vin.VinTextValue });
            }
        }

        public static List<Vin> GetAllVins()
        {
            string sqlCommand = "[dbo].prGetAllVins"; // prGetAllVins
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                // List<Vin> vins = connection.Query<Vin>(sqlCommand).ToList();
                List<Vin> vins = new List<Vin>();
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                vins.Add(new Vin { VinTextValue = (string)dr["VinTextValue"] });
                            }
                        }
                        connection.Close();
                        return vins;
                    }
                }
            }
        }
    }
}
