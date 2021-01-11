using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using VinsUncoderLibrary.Models;
using VinsUncoderLibrary.ModelsView;

namespace VinsUncoderLibrary.DataBase
{
    public class VinDescriptionsDataBase
    {
        private readonly static string ConnectionString = StaticConfigValues.CONNECTION_STRING;

        public static List<VinDescription> GetAllVinsDescriptions()
        {
            string sqlCommand = "Select VinDescriptionId, MarkId, MaskId, VinPart, VinPartDescription, EnumMeaningOfVinParts  from VinsDescriptions";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                List<VinDescription> descriptions = new List<VinDescription>();

                using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                VinDescription description = new VinDescription
                                {
                                    IdOfDescription = (int)reader["VinDescriptionId"],
                                    IdOfMark = (string)reader["MarkId"],
                                    IdOfMask = (int)reader["MaskId"],
                                    VinPart = (string)reader["VinPart"],
                                    VinPartDecription = (string)reader["VinPartDescription"],
                                    EnumMeaningOfVinParts = (TypeOfVinPartMeaning)reader["EnumMeaningOfVinParts"]

                                };
                                descriptions.Add(description);
                            }
                        }

                        connection.Close();

                        return descriptions;

                    }
                }
            }
        }
        public static List<VinDescriptionView> GetAllVinsDescriptionsView()
        {
            string sqlCommand = "Select VinPart, VinPartDescription, EnumMeaningOfVinParts from VinsDescriptions";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                List<VinDescriptionView> descriptions = new List<VinDescriptionView>();

                using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                VinDescriptionView description = new VinDescriptionView
                                {
                                    VinPart = (string)reader["VinPart"],
                                    VinPartDecription = (string)reader["VinPartDescription"],
                                    EnumMeaningOfVinParts = (TypeOfVinPartMeaning)reader["EnumMeaningOfVinParts"]

                                };
                                descriptions.Add(description);
                            }
                        }

                        connection.Close();

                        return descriptions;

                    }
                }
            }
        }

        public static string GetMarkOfMask(string MarkId)
        {
            string sqlCommand = "SELECT TOP(1) VinPartDescription From VinsDescriptions" +
                " Where MaskId = (SELECT MaskId FROM VinMasks Where Mask = N'XXX00000000000000')" +
                " AND MarkId = @MarkId";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                {
                    string Mark = "None";

                    command.Parameters.Add("@MarkId", SqlDbType.NVarChar).Value = MarkId;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Mark = reader["VinPartDescription"].ToString();
                            }
                        }

                        connection.Close();

                        return Mark;
                    }
                }
            }
        }

        public static List<VinDescription> GetVinsDescriptionsByMaskId(int MaskId)
        {
            string sqlCommand = "Select VinDescriptionId, MarkId, MaskId, VinPart, VinPartDescription, EnumMeaningOfVinParts from VinsDescriptions WHERE MaskId = 12 ORDER BY VinPartDescription";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                List<VinDescription> descriptions = new List<VinDescription>();

                using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                {

                    command.Parameters.Add("@MaskId", System.Data.SqlDbType.Int).Value = MaskId;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                VinDescription description = new VinDescription
                                {
                                    IdOfDescription = (int)reader["VinDescriptionId"],
                                    IdOfMark = (string)reader["MarkId"],
                                    IdOfMask = (int)reader["MaskId"],
                                    VinPart = (string)reader["VinPart"],
                                    VinPartDecription = (string)reader["VinPartDescription"],
                                    EnumMeaningOfVinParts = (TypeOfVinPartMeaning)reader["EnumMeaningOfVinParts"]
                                };
                                descriptions.Add(description);
                            }
                        }

                        connection.Close();

                        return descriptions;

                    }
                }
            }
        }
        

        public static void AddVinDescription(VinDescription description)
        {
            string sqlCommand = "INSERT Into VinsDescriptions (MarkId,MaskId,VinPart,VinPartDescription,EnumMeaningOfVinParts) VALUES " +
                "(@MarkId,@MaskId,@VinPart,@VinPartDescription,@EnumMeaningOfVinParts) ";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                {
                    command.Parameters.Add("@MarkId", System.Data.SqlDbType.NVarChar).Value = description.IdOfMark;
                    command.Parameters.Add("@MaskId", System.Data.SqlDbType.Int).Value = description.IdOfMask;
                    command.Parameters.Add("@VinPart", System.Data.SqlDbType.NVarChar).Value = description.VinPart;
                    command.Parameters.Add("@VinPartDescription", System.Data.SqlDbType.NVarChar ).Value = description.VinPartDecription;
                    command.Parameters.Add("@EnumMeaningOfVinParts", System.Data.SqlDbType.Int).Value = description.EnumMeaningOfVinParts;

                    command.ExecuteNonQuery();

                    connection.Close();
                }
            }
        }
        public static int AddVinDescriptionWhithOutput(VinDescription description)
        {
            string sqlCommand = "INSERT Into VinsDescriptions (MarkId,MaskId,VinPart,VinPartDescription,EnumMeaningOfVinParts) " +
                "  OUTPUT inserted.VinDescriptionId" +
                " VALUES " +
                "(@MarkId,@MaskId,@VinPart,@VinPartDescription,@EnumMeaningOfVinParts) ";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                int descId = 0;
                using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                {

                    command.Parameters.Add("@MarkId", System.Data.SqlDbType.NVarChar).Value = description.IdOfMark;
                    command.Parameters.Add("@MaskId", System.Data.SqlDbType.Int).Value = description.IdOfMask;
                    command.Parameters.Add("@VinPart", System.Data.SqlDbType.NVarChar).Value = description.VinPart;
                    command.Parameters.Add("@VinPartDescription", System.Data.SqlDbType.NVarChar).Value = description.VinPartDecription;
                    command.Parameters.Add("@EnumMeaningOfVinParts", System.Data.SqlDbType.Int).Value = description.EnumMeaningOfVinParts;

                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            descId = (int)reader["VinDescriptionId"];
                        }
                    }

                    connection.Close();

                    return descId;
                    
                }
            }
        }
       
        public static List<VinDescriptionView> GetVinDescroptionViewByMark(string Mark)
        {
            string sqlCommand = @"
            SELECT 
            	vd.VinPart,
            	vd.VinPartDescription,
            	vd.EnumMeaningOfVinParts
             FROM VinsDescriptions vd
             WHERE	(MarkId = (SELECT TOP(1)
            				      MarkId
            				    FROM VinsDescriptions vd
            				    WHERE	vd.VinPartDescription = @Mark))
            ";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                List<VinDescriptionView> descriptions = new List<VinDescriptionView>();

                using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                {
                    command.Parameters.Add("@Mark", SqlDbType.NVarChar).Value = Mark;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                VinDescriptionView description = new VinDescriptionView
                                {
                                    VinPart = (string)reader["VinPart"],
                                    VinPartDecription = (string)reader["VinPartDescription"],
                                    EnumMeaningOfVinParts = (TypeOfVinPartMeaning)reader["EnumMeaningOfVinParts"]

                                };
                                descriptions.Add(description);
                            }
                        }
                        connection.Close();

                        return descriptions;
                    }
                }
            }
        }

        public static List<VinDescription> GetVinDescroptionByMark(string Mark)
        {
            string sqlCommand = @"
            SELECT
	            vd.VinPart,
	            vd.VinPartDescription,
	            vd.EnumMeaningOfVinParts,
	            vd.VinDescriptionId,
	            vd.MarkId,
	            vd.MaskId,
	            vm.Mask
             FROM VinsDescriptions vd
             INNER JOIN Marks m ON (vd.MarkId = m.MarkId)
             INNER JOIN VinMasks vm ON (vd.MaskId = vm.MaskId)
             WHERE m.Mark = @Mark
            ";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                List<VinDescription> descriptions = new List<VinDescription>();

                using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                {
                    command.Parameters.Add("@Mark", SqlDbType.NVarChar).Value = Mark;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                VinDescription description = new VinDescription
                                {
                                    IdOfDescription = (int)reader["VinDescriptionId"],
                                    IdOfMark = (string)reader["MarkId"],
                                    IdOfMask = (int)reader["MaskId"],
                                    VinPart = (string)reader["VinPart"],
                                    VinPartDecription = (string)reader["VinPartDescription"],
                                    EnumMeaningOfVinParts = (TypeOfVinPartMeaning)reader["EnumMeaningOfVinParts"],
                                    Mask = reader["Mask"].ToString()

                                };
                                descriptions.Add(description);
                            }
                        }
                        connection.Close();

                        return descriptions;
                    }
                }
            }
        }

        public static List<VinDescription> GetVinsDescriptionsByMarkIdAndMaskId(string MarkId, int MaskId)
        {
            string sqlCommand = "Select VinDescriptionId, MarkId, MaskId, VinPart, VinPartDescription, EnumMeaningOfVinParts from VinsDescriptions WHERE MarkId = @MarkId and MaskId = @MaskId";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                List<VinDescription> descriptions = new List<VinDescription>();

                using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                {
                    command.Parameters.Add("@MarkId", System.Data.SqlDbType.NVarChar).Value = MarkId;
                    command.Parameters.Add("@MaskId", System.Data.SqlDbType.Int).Value = MaskId;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                VinDescription description = new VinDescription
                                {
                                    IdOfDescription = (int)reader["VinDescriptionId"],
                                    IdOfMark = (string)reader["MarkId"],
                                    IdOfMask = (int)reader["MaskId"],
                                    VinPart = (string)reader["VinPart"],
                                    VinPartDecription = (string)reader["VinPartDescription"],
                                    EnumMeaningOfVinParts = (TypeOfVinPartMeaning)reader["EnumMeaningOfVinParts"]

                                };
                                descriptions.Add(description);
                            }
                        }
                        connection.Close();

                        return descriptions;
                    }
                }
            }
        }

        public static List<VinDescription> GetVinsDescriptionsByMarkId(string markId)
        {
            string sqlCommand = @"
            SELECT
	            vd.VinPart
             FROM VinsDescriptions vd
             INNER JOIN Marks m ON (vd.MarkId = m.MarkId)
             INNER JOIN VinMasks vm ON (vd.MaskId = vm.MaskId)
             WHERE vd.MarkId = @MarkId
            ";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                List<VinDescription> descriptions = new List<VinDescription>();

                using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                {
                    command.Parameters.Add("@MarkId", SqlDbType.NVarChar).Value = markId;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                VinDescription description = new VinDescription
                                {
                                    VinPart = (string)reader["VinPart"],
                                };
                                descriptions.Add(description);
                            }
                        }
                        connection.Close();

                        return descriptions;
                    }
                }
            }
        }

        internal static VinDescription GetVinsDescriptionsByDescriptionId(int idOfDescriptionSecond)
        {
            string sqlCommand = "SELECT VinDescriptionId, MarkId, MaskId, VinPart, VinPartDescription, EnumMeaningOfVinParts FROM VinsDescriptions WHERE VinDescriptionId = @VinDescriptionId";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                VinDescription vinsDescription = new VinDescription();

                using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                {
                    command.Parameters.Add("@VinDescriptionId", System.Data.SqlDbType.Int).Value = idOfDescriptionSecond;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                vinsDescription.IdOfDescription = (int)reader["VinDescriptionId"];
                                vinsDescription.IdOfMark = (string)reader["MarkId"];
                                vinsDescription.IdOfMask = (int)reader["MaskId"];
                                vinsDescription.VinPart = (string)reader["VinPart"];
                                vinsDescription.VinPartDecription = (string)reader["VinPartDescription"];
                                vinsDescription.EnumMeaningOfVinParts = (TypeOfVinPartMeaning)reader["EnumMeaningOfVinParts"];
                            }
                        }
                        connection.Close();

                        return vinsDescription;
                    }
                }
            }
        }

        public static List<VinDescription> GetVinDescriptionLinksByDescId(int descId)
        {
            string commandString = @"
            SELECT 
	            vd.EnumMeaningOfVinParts,
	            vd.MarkId,
	            vd.MaskId,
	            vd.VinDescriptionId,
	            vd.VinPart,
	            vd.VinPartDescription
             FROM VinDescriptionsLinks vdl
             INNER JOIN VinsDescriptions vd ON (vdl.IdOfDescriptionSecond = vd.VinDescriptionId)
             WHERE  (vdl.IdOfDescriptionFirst = @descId)
            ";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                List<VinDescription> descriptions = new List<VinDescription>();

                using (SqlCommand command = new SqlCommand(commandString, connection))
                {
                    command.Parameters.Add("@descId", SqlDbType.Int).Value = descId;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                VinDescription description = new VinDescription
                                {
                                    IdOfDescription = (int)reader["VinDescriptionId"],
                                    IdOfMark = (string)reader["MarkId"],
                                    IdOfMask = (int)reader["MaskId"],
                                    VinPart = (string)reader["VinPart"],
                                    VinPartDecription = (string)reader["VinPartDescription"],
                                    EnumMeaningOfVinParts = (TypeOfVinPartMeaning)reader["EnumMeaningOfVinParts"]

                                };
                                descriptions.Add(description);
                            }
                        }
                        connection.Close();

                        return descriptions;
                    }
                }
            }

        }

        public static VinDescription GetVinsDescriptionsById(int VinDescId)
        {
            string commandString = @"
            SELECT
	            vd.VinDescriptionId,
	            vd.VinPart,
	            vd.EnumMeaningOfVinParts,
	            vd.MarkId,
	            vd.MaskId,
	            vd.VinPartDescription
             FROM VinsDescriptions vd
             WHERE vd.VinDescriptionId = @VinDescId
                ";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(commandString, connection))
                {
                    VinDescription vinsDescription = new VinDescription();

                    command.Parameters.Add("@VinDescId", SqlDbType.Int).Value = VinDescId;

                    using (SqlDataReader reader = command.ExecuteReader()){
                        if (reader.HasRows)
                        {
                            reader.Read();
                            vinsDescription.IdOfDescription = (int)reader["VinDescriptionId"];
                            vinsDescription.IdOfMark = (string)reader["MarkId"];
                            vinsDescription.IdOfMask = (int)reader["MaskId"];
                            vinsDescription.VinPart = (string)reader["VinPart"];
                            vinsDescription.VinPartDecription = (string)reader["VinPartDescription"];
                            vinsDescription.EnumMeaningOfVinParts = (TypeOfVinPartMeaning)reader["EnumMeaningOfVinParts"];
                        }

                        connection.Close();

                        return vinsDescription;
                    }
                }
            }

        }

        public static void RemoveVinDescById(int VinDescritptionId)
        {
            string commandString = @"
            DELETE vd
            FROM VinsDescriptions vd
            WHERE vd.VinDescriptionId = @VinDescriptionId
            ";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(commandString, connection))
                {
                    command.Parameters.Add("@VinDescriptionId", SqlDbType.Int).Value = VinDescritptionId;

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
