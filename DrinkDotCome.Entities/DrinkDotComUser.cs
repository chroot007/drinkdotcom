using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DrinkDotCom.Entities
{
    public class DrinkDotComUser : IdentityUser
    {
        [Required]
        [StringLength(50, ErrorMessage = "Full Name length can't be more than 50")]
        public string FullName { get; set; }

        public string Country { get; set; }
        public string City { get; set; }

        public string Address { get; set; }

        public string ZipCode { get; set; }

        [Required]
        public DateTime? Dateofbirth { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(Microsoft.AspNet.Identity.UserManager<DrinkDotComUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            userIdentity.AddClaim(new Claim("Email", Email));
            userIdentity.AddClaim(new Claim("Picture", this.Picture != null ? this.Picture.URL : string.Empty));

            return userIdentity;
        }

        public int? PictureID { get; set; }
        public virtual Picture Picture { get; set; }
    }
}
