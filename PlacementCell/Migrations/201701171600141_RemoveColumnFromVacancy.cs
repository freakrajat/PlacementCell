namespace PlacementCell.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveColumnFromVacancy : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.VacancyModelDBs", "DatePosted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.VacancyModelDBs", "DatePosted", c => c.String());
        }
    }
}
