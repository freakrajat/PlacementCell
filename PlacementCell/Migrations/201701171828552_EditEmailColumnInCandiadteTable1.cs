namespace PlacementCell.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditEmailColumnInCandiadteTable1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CandidateProfileDBModels", "MailAddress", c => c.String());
            DropColumn("dbo.CandidateProfileDBModels", "EmailAddress");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CandidateProfileDBModels", "EmailAddress", c => c.String());
            DropColumn("dbo.CandidateProfileDBModels", "MailAddress");
        }
    }
}
