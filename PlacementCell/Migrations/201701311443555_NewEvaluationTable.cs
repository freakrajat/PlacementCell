namespace PlacementCell.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewEvaluationTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CandidateEvaluationDBModels",
                c => new
                    {
                        EvaluationID = c.Int(nullable: false, identity: true),
                        SalaryAgreed = c.Long(nullable: false),
                        LocationOffered = c.String(),
                        InterviewResult = c.String(),
                        Comments = c.String(),
                        CandidateID_CanProfileID = c.Int(),
                    })
                .PrimaryKey(t => t.EvaluationID)
                .ForeignKey("dbo.CandidateProfileDBModels", t => t.CandidateID_CanProfileID)
                .Index(t => t.CandidateID_CanProfileID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CandidateEvaluationDBModels", "CandidateID_CanProfileID", "dbo.CandidateProfileDBModels");
            DropIndex("dbo.CandidateEvaluationDBModels", new[] { "CandidateID_CanProfileID" });
            DropTable("dbo.CandidateEvaluationDBModels");
        }
    }
}
