namespace PlacementCell.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatingDatabaseWithTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CandidateProfileDBModels",
                c => new
                    {
                        CanProfileID = c.Int(nullable: false, identity: true),
                        CandidateName = c.String(nullable: false),
                        DOB = c.DateTime(),
                        Location = c.String(),
                        Gender = c.String(),
                        Percentage10 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Percentage12 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalExp = c.Int(nullable: false),
                        VacancyID_VacancyID = c.Int(),
                    })
                .PrimaryKey(t => t.CanProfileID)
                .ForeignKey("dbo.VacancyModelDBs", t => t.VacancyID_VacancyID)
                .Index(t => t.VacancyID_VacancyID);
            
            CreateTable(
                "dbo.VacancyModelDBs",
                c => new
                    {
                        VacancyID = c.Int(nullable: false, identity: true),
                        VacancyTitle = c.String(nullable: false),
                        NumPosition = c.Int(nullable: false),
                        Skills = c.String(maxLength: 500),
                        ExpRequired = c.Int(nullable: false),
                        Location = c.String(),
                        Domain = c.String(),
                        PostingDate = c.DateTime(),
                        DatePosted = c.String(),
                    })
                .PrimaryKey(t => t.VacancyID);
            
            CreateTable(
                "dbo.EmployeeDBModels",
                c => new
                    {
                        EmployeeID = c.Int(nullable: false, identity: true),
                        EmployeeName = c.String(nullable: false),
                        Location = c.String(),
                        DOB = c.DateTime(nullable: false),
                        DOJ = c.DateTime(nullable: false),
                        CTC = c.Int(nullable: false),
                        Designation = c.String(),
                        Email = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeID);
            
            CreateTable(
                "dbo.LoginDBModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(nullable: false),
                        RememberMe = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PlacementDBTests",
                c => new
                    {
                        TestID = c.Int(nullable: false, identity: true),
                        TestDate = c.DateTime(),
                        TechnicalInterviewDate = c.DateTime(),
                        HrInterviewDate = c.DateTime(),
                        SelectCandidate_CanProfileID = c.Int(nullable: false),
                        TestAdministrator_EmployeeID = c.Int(nullable: false),
                        Vacancy_VacancyID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TestID)
                .ForeignKey("dbo.CandidateProfileDBModels", t => t.SelectCandidate_CanProfileID, cascadeDelete: true)
                .ForeignKey("dbo.EmployeeDBModels", t => t.TestAdministrator_EmployeeID, cascadeDelete: true)
                .ForeignKey("dbo.VacancyModelDBs", t => t.Vacancy_VacancyID, cascadeDelete: true)
                .Index(t => t.SelectCandidate_CanProfileID)
                .Index(t => t.TestAdministrator_EmployeeID)
                .Index(t => t.Vacancy_VacancyID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlacementDBTests", "Vacancy_VacancyID", "dbo.VacancyModelDBs");
            DropForeignKey("dbo.PlacementDBTests", "TestAdministrator_EmployeeID", "dbo.EmployeeDBModels");
            DropForeignKey("dbo.PlacementDBTests", "SelectCandidate_CanProfileID", "dbo.CandidateProfileDBModels");
            DropForeignKey("dbo.CandidateProfileDBModels", "VacancyID_VacancyID", "dbo.VacancyModelDBs");
            DropIndex("dbo.PlacementDBTests", new[] { "Vacancy_VacancyID" });
            DropIndex("dbo.PlacementDBTests", new[] { "TestAdministrator_EmployeeID" });
            DropIndex("dbo.PlacementDBTests", new[] { "SelectCandidate_CanProfileID" });
            DropIndex("dbo.CandidateProfileDBModels", new[] { "VacancyID_VacancyID" });
            DropTable("dbo.PlacementDBTests");
            DropTable("dbo.LoginDBModels");
            DropTable("dbo.EmployeeDBModels");
            DropTable("dbo.VacancyModelDBs");
            DropTable("dbo.CandidateProfileDBModels");
        }
    }
}
