namespace PlacementCell.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImageTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProfileImageDBModels",
                c => new
                    {
                        ImageID = c.Int(nullable: false, identity: true),
                        EmployeeImage = c.Binary(),
                        Emloyee_EmployeeID = c.Int(),
                    })
                .PrimaryKey(t => t.ImageID)
                .ForeignKey("dbo.EmployeeDBModels", t => t.Emloyee_EmployeeID)
                .Index(t => t.Emloyee_EmployeeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProfileImageDBModels", "Emloyee_EmployeeID", "dbo.EmployeeDBModels");
            DropIndex("dbo.ProfileImageDBModels", new[] { "Emloyee_EmployeeID" });
            DropTable("dbo.ProfileImageDBModels");
        }
    }
}
