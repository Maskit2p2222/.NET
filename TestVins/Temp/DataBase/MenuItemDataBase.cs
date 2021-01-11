using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Temp.Models;

namespace Temp.DataBase
{
    public class MenuItemDataBase
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["VinWebFormsApp"].ConnectionString;

        public static List<MenuItem> GetMenuItemsByUsername(string UserName)
        {

            string SqlCommand = @"
            SELECT   
            	p.PageUrl,
            	m.MenuName,
            	m.MenuId,
            	m.ParentId
             FROM aspnet_UsersInRoles ur
             INNER JOIN [PageAvailableRoles] pl ON (pl.RoleId = ur.RoleId)
             INNER JOIN [Pages] p ON (p.PageId = pl.PageId)
             LEFT JOIN [Menus] m ON (p.PageId = m.PageId)
             INNER JOIN [aspnet_Users] u ON (u.UserName = @UserName)
             WHERE 
            		ur.UserId = u.UserId
            ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                List<MenuItem> menuItems = new List<MenuItem>();
                using (SqlCommand command = new SqlCommand(SqlCommand, connection))
                {
                    command.Parameters.Add("@UserName", System.Data.SqlDbType.NVarChar).Value = UserName;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                MenuItem menuItem = new MenuItem();
                                menuItem.MenuId = ReferenceEquals(reader["MenuId"], DBNull.Value) ? -1 : (int)reader["MenuId"];
                                menuItem.ParentId = ReferenceEquals(reader["ParentId"], DBNull.Value) ? -1 : (int)reader["ParentId"];
                                menuItem.PageUrl = reader["PageUrl"].ToString();
                                menuItem.MenuName =/* ReferenceEquals(reader["MenuName"], DBNull.Value) ? null :*/ reader["MenuName"].ToString();
                               
                                menuItems.Add(menuItem);
                            }
                        }
                        connection.Close();
                        return menuItems;
                    }
                }
            }
        }

        //public static List<string> GetAvailablePages(string UserName)
        //{
        //    string SqlCommand = @"
        //    SELECT
        //    	p.PageUrl
        //     FROM Pages p
        //     INNER JOIN PageAvailableRoles pr ON (pr.PageId = p.PageId)
        //     INNER JOIN aspnet_UsersInRoles ur ON (ur.RoleId = pr.RoleId)
        //     INNER JOIN aspnet_Users u ON (u.UserId = ur.UserId)
        //     WHERE
        //    		u.UserName = @UserName";
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();
        //        List<string> PagesUrls = new List<string>();
        //        using (SqlCommand command = new SqlCommand(SqlCommand, connection))
        //        {

        //            command.Parameters.Add("@UserName", System.Data.SqlDbType.NVarChar).Value = UserName;
        //            using (SqlDataReader reader = command.ExecuteReader())
        //            {
        //                if (reader.HasRows)
        //                {
        //                    while (reader.Read())
        //                    {
        //                        PagesUrls.Add(reader["PageUrl"].ToString());
        //                    }
        //                }
        //                connection.Close();
        //                return PagesUrls ;
        //            }
        //        }
        //    }

        //}
    }
}