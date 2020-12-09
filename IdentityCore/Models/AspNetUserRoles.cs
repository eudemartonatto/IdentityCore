using Microsoft.AspNetCore.Identity;
using System;
using Microsoft.Data.SqlClient;
using System.ComponentModel;

namespace IdentityCore.Models
{
    public class ApplicationAspNetUserRoles :  IdentityUserRole<string>        
    {
        
        public string RoleName;
        public string UserName;
        
        [DefaultValue("ApplicationAspNetUserRoles")]
        public string Discriminator;

        public ApplicationAspNetUserRoles(string userId, string roleId) : base()
        {        
            string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=aspnet-IdentityCore-3BD2EB4F-0BBD-49A4-8186-8F023F37A994;Integrated Security=true";                           
            string queryString =
                "SELECT r.Name roleName, u.UserName userName" +
                  " FROM AspNetUserRoles ur, AspNetUsers u, AspNetRoles r " +
                  " WHERE ur.UserId = u.Id " +
                  "   AND ur.RoleId = r.Id" +
                  "   AND ur.RoleId = @roleId " +
                  "   AND ur.UserId = @userId";

            using SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(queryString, connection);
            command.Parameters.AddWithValue("@userId", userId);
            command.Parameters.AddWithValue("@roleId", roleId);


            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                this.RoleId = roleId;
                this.UserId = userId;
                this.RoleName = reader[0].ToString();
                this.UserName = reader[1].ToString();
                reader.Close();
            }
            catch (Exception ex)
            {
                this.UserName = ex.Message;
            }

        }
    
    }
    }
