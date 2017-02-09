namespace PlacementCell.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedEmailInCandiadteTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CandidateProfileDBModels", "Email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CandidateProfileDBModels", "Email");
        }
    }
}
