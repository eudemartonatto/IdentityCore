using Microsoft.AspNetCore.Identity;
using System.ComponentModel;

namespace IdentityCore.Models
{
    public class ApplicationAspNetUserRoles2 :  IdentityUserRole<string>        
    {
        [DefaultValue("ApplicationAspNetUserRoles")]
        public string Discriminator;
    }
 }
