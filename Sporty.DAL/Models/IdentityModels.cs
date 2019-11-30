using System;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace Sporty.DAL.Models
{
    public class ApplicationUser : IdentityUser<string, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public ApplicationUser()
        {
            Id = Guid.NewGuid().ToString();
        }

        // Custom properties
        public virtual List<Place> Places { get; set; }
        public virtual List<ScheduleOrder> Schedules { get; set; }
        public virtual List<Rating> Ratings { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, string> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here

            return userIdentity;
        }
    }
    public class ApplicationRole : IdentityRole<string, ApplicationUserRole>
    {

        // Custom properties
        //
    }
    public class ApplicationUserLogin : IdentityUserLogin<string>
    {

    }
    public class ApplicationUserRole : IdentityUserRole<string>
    {

    }
    public class ApplicationUserClaim : IdentityUserClaim<string>
    {

    }
}
