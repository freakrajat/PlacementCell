namespace PlacementCell.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDOJcolumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CandidateEvaluationDBModels", "DOJ", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CandidateEvaluationDBModels", "DOJ");
        }
    }
}
