using Microsoft.AspNetCore.Identity;
using System;
using Microsoft.Data.SqlClient;
using System.ComponentModel;
using IdentityCore.Areas;
using IdentityCore.Controllers;
using IdentityCore.Data;
using IdentityCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace IdentityCore.Models
{
    public class ApplicationAspNetUserRoles :  IdentityUserRole<string>        
    {
        
        public string RoleName;
        public string UserName;
     
        private readonly ApplicationDbContext _context;

        /* 
        public ApplicationAspNetUserRoles(ApplicationDbContext context, string userId, string roleId) : base()
        {
            _context = context;
            string usuario = userId;
            string regra = roleId;
        }
     */

        [DefaultValue("ApplicationAspNetUserRoles")]
        public string Discriminator;

        public ApplicationAspNetUserRoles(ApplicationDbContext context, string userId, string roleId) : base()
        {            
            /** se comentar este trecho o app funciona, context existe, mas as tabelas não funcionam*/
            var ur = (
               from geral in context.ApplicationAspNetUserRoles
               select new { value = geral.UserId, text = geral.RoleId }
             ).ToList();
            var userRole = new SelectList(ur, "value", "text");
            /**/



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
