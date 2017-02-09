namespace PlacementCell.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditEmailColumnInCandiadteTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CandidateProfileDBModels", "EmailAddress", c => c.String());
            DropColumn("dbo.CandidateProfileDBModels", "Email");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CandidateProfileDBModels", "Email", c => c.String());
            DropColumn("dbo.CandidateProfileDBModels", "EmailAddress");
        }
    }
}
