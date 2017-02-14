using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PlacementCell.PlacementCellDBModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace PlacementCell.PlacementCellDBModel
{
    public class PlacementCellDBContext : DbContext
    {
        public PlacementCellDBContext()
            : base("name = DefaultConnection")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<PlacementCellDBContext>(null);
            base.OnModelCreating(modelBuilder);
        }

        public class ApplicationUser : IdentityUser
        {
            public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
            {
                // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
                var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
                // Add custom user claims here
                return userIdentity;
            }
        }

        public static PlacementCellDBContext Create()
        {
            return new PlacementCellDBContext();
        }
        public DbSet<LoginDBModel> LoginTable { get; set; }

        public DbSet<VacancyModelDB> VacancyTable { get; set; }

        public DbSet<EmployeeDBModel> EmployeeTable { get; set; }

        public DbSet<CandidateProfileDBModel> CandidateProfileTable { get; set; }

        public DbSet<PlacementDBTest> TestTable { get; set; }

        
        public DbSet<CandidateEvaluationDBModel> EvaluationTable { get; set; }

        public DbSet<ProfileImageDBModel> ProfileImageTable { get; set; }
    }
}