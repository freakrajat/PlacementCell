namespace PlacementCell.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDesignationColumnInTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CandidateEvaluationDBModels", "DesignationOffered", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CandidateEvaluationDBModels", "DesignationOffered");
        }
    }
}
