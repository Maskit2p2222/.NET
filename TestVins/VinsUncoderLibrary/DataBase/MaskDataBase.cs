//using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using VinsUncoderLibrary.Models;
using VinsUncoderLibrary.ModelsView;

namespace VinsUncoderLibrary.DataBase
{
    public class MaskDataBase
    {
        private readonly static string connectionString = StaticConfigValues.CONNECTION_STRING;
        //public MaskDataBase(string connectionString)
        //{
        //    this.connectionString = connectionString;
        //}

        public static List<Mask> GetAllMasks()
        {
            string sqlCommand = "SELECT * FROM VinMasks";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                List<Mask> masks = new List<Mask>();
                using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                {
                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                masks.Add(new Mask
                                {
                                    IdOfMark = (string)dr["MarkId"],
                                    MaskView = (string)dr["Mask"],
                                    IdOfMask = (int)dr["MaskId"]
                                });

                            }
                        }
                        connection.Close();
                        return masks;
                    }
                }
                    /*connection.Query<Mask>(sqlCommand).ToList();*/
            }
        }


        public static List<MaskView> GetAllMasksView()
        {
            string sqlCommand = "SELECT Mask FROM VinMasks";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                {
                    List<MaskView> maskViews = new List<MaskView>();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                maskViews.Add(new MaskView
                                {
                                    Mask = reader["Mask"].ToString()
                                });

                            }
                        }
                        connection.Close();
                        return maskViews;
                    }

                }
            }
        }

        public static List<Mask> GetMasksByIdOfMark(string MarkId)
        {
            string sqlCommand = "SELECT MarkId, Mask, MaskId FROM VinMasks WHERE MarkId = @MarkId;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                List<Mask> masks = new List<Mask>();
                using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                {
                    //command.Parameters.AddWithValue("@MarkId", MarkId);
                    command.Parameters.Add("@MarkId", System.Data.SqlDbType.NVarChar).Value = MarkId;
                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                masks.Add(new Mask
                                {
                                    IdOfMark = (string)dr["MarkId"],
                                    MaskView = (string)dr["Mask"],
                                    IdOfMask = (int)dr["MaskId"]
                                });

                            }
                        }
                        connection.Close();
                        return masks;
                    }
                }
            }
        }

        public static MaskModel GetMaskModelByIdOfMask(int MaskId)
        {
            string sqlCommand = @" 
            SELECT
                v.*,
                COUNT(vd.VinPartDescription) as CountOfDescriptions
            FROM VinMasks v
            LEFT JOIN VinsDescriptions vd ON(vd.MaskId = v.MaskId)
            WHERE(vd.MaskId = @MaskId)
            GROUP BY v.MarkId, v.MaskId, v.Mask
            ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                MaskModel mask = new MaskModel();
                using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                {
                    //command.Parameters.AddWithValue("@MaskId", MaskId);
                    command.Parameters.Add("@MaskId", System.Data.SqlDbType.Int).Value = MaskId;
                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            dr.Read();
                            mask.IdOfMark = (string)dr["MarkId"];
                            mask.IdOfMask = (int)dr["MaskId"];
                            mask.MaskView = (string)dr["Mask"];
                            mask.CountOfDescriptions = (int)dr["CountOfDescriptions"];
                        }
                        connection.Close();
                        return mask;
                    }
                }
            }
        }

        public static List<MaskModel> GetMaskModelsByMark(string Mark)
        {
            string sqlCommand = @"
                SELECT
                    v.*,
                    COUNT(vd.VinPartDescription) as CountOfDescriptions
                FROM VinMasks v
                INNER JOIN Marks m ON (m.MarkId = v.MarkId)
                LEFT JOIN VinsDescriptions vd ON(vd.MaskId = v.MaskId)
                WHERE(m.Mark = @Mark)
                GROUP BY v.MarkId, v.MaskId, v.Mask
            ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                List<MaskModel> masks = new List<MaskModel>();
                using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                {
                    //command.Parameters.AddWithValue("@MarkId", MarkId);
                    command.Parameters.Add("@Mark", System.Data.SqlDbType.NVarChar).Value = Mark;
                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                masks.Add(new MaskModel
                                {
                                    IdOfMark = (string)dr["MarkId"],
                                    MaskView = (string)dr["Mask"],
                                    IdOfMask = (int)dr["MaskId"],
                                    CountOfDescriptions = (int)dr["CountOfDescriptions"]
                                });

                            }
                        }
                        connection.Close();
                        return masks;
                    }
                }
            }
        }

        
        public static List<Mask> GetMasksByMark(string Mark)
        {
            string sqlCommand = @"
            SELECT
                v.*
             FROM VinMasks v
             INNER JOIN Marks m ON (m.MarkId = v.MarkId)
             WHERE(m.Mark = @Mark)
            ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                List<Mask> masks = new List<Mask>();
                using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                {
                    command.Parameters.Add("@Mark", System.Data.SqlDbType.NVarChar).Value = Mark;
                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                masks.Add(new Mask
                                {
                                    IdOfMark = (string)dr["MarkId"],
                                    MaskView = (string)dr["Mask"],
                                    IdOfMask = (int)dr["MaskId"]
                                });

                            }
                        }
                        connection.Close();
                        return masks;
                    }
                }
            }
        }



        public static string GenerateNewMarkId()
        {
            string sqlCommand = "SELECT DISTINCT MarkId From VinMasks";
            List<int> MarksId = new List<int>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                
                using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                MarksId.Add(Convert.ToInt32(reader["MarkId"].ToString()));
                            }
                        }
                    }
                }
                connection.Close();
            }
            MarksId.Sort();
            return (MarksId.LastOrDefault() + 1).ToString();

        }

        public static string GetMarkIdByMark(string mark)
        {
            string sqlCommand = "SELECT TOP (1) MarkId From VinsDescriptions Where VinPartDescription = @VinPartDescription";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string MarkId = null;
                using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                {
                    command.Parameters.Add("@VinPartDescription", System.Data.SqlDbType.NVarChar).Value = mark;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                MarkId = reader["MarkId"].ToString();
                            }
                        }
                    }
                    connection.Close();
                    return MarkId;
                }
            }
        }

        public static void AddMask(Mask mask)
        {
            string sqlCommand = "INSERT INTO VinMasks (MarkId, Mask) Values (@MarkId,@Mask);";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                {
                    //command.Parameters.AddWithValue("@MarkId", mask.IdOfMark);
                    //command.Parameters.AddWithValue("@Mask", mask.MaskView);
                    command.Parameters.Add("@MarkId", System.Data.SqlDbType.NVarChar).Value = mask.IdOfMark;
                    command.Parameters.Add("@Mask", System.Data.SqlDbType.NVarChar).Value = mask.MaskView;
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }


        public static Mask GetMasksByIdOfMask(int MaskId)
        {
            string sqlCommand = "SELECT TOP(1) MarkId, MaskId, Mask FROM VinMasks WHERE MaskId = @MaskId;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                Mask mask = new Mask();
                using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                {
                    //command.Parameters.AddWithValue("@MaskId", MaskId);
                    command.Parameters.Add("@MaskId", System.Data.SqlDbType.Int).Value = MaskId;
                    using (SqlDataReader dr = command.ExecuteReader()) 
                    {
                        if (dr.HasRows)
                        {
                            dr.Read();
                            mask.IdOfMark = (string)dr["MarkId"];
                            mask.IdOfMask = (int)dr["MaskId"];
                            mask.MaskView = (string)dr["Mask"];
                        }
                        connection.Close();
                        return mask;
                    }
                }
            }
        }


    }
}
