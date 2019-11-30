using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Sporty.DAL.Models;
using System.Data.Entity;

namespace Sporty.DAL
{
    public class SportyDbContext : IdentityDbContext<
        ApplicationUser, ApplicationRole, string, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public SportyDbContext()
            : base("DefaultConnection")
        {
        }

        public static SportyDbContext Create()
        {
            return new SportyDbContext();
        }

        // Custom
        public DbSet<Field> Fields { get; set; }
        public DbSet<FieldType> FieldTypes { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<ScheduleOrder> SchedulesOrder { get; set; }
        public DbSet<SpecialDayPrice> SpecialDayPrices { get; set; }
        public DbSet<WeekPrice> WeekPrices { get; set; }
    }
}
