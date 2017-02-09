using PlacementCell.PlacementCellDBModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
        public DbSet<LoginDBModel> LoginTable { get; set; }

        public DbSet<VacancyModelDB> VacancyTable { get; set; }

        public DbSet<EmployeeDBModel> EmployeeTable { get; set; }

        public DbSet<CandidateProfileDBModel> CandidateProfileTable { get; set; }

        public DbSet<PlacementDBTest> TestTable { get; set; }

        
        public DbSet<CandidateEvaluationDBModel> EvaluationTable { get; set; }

        public DbSet<ProfileImageDBModel> ProfileImageTable { get; set; }
    }
}