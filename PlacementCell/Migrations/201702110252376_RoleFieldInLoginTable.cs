namespace PlacementCell.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RoleFieldInLoginTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LoginDBModels", "Role", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LoginDBModels", "Role");
        }
    }
}
