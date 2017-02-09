namespace PlacementCell.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLoginToEmployee : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.UserImageDBModels", "EmployeeID_EmployeeID", "dbo.EmployeeDBModels");
            //DropIndex("dbo.UserImageDBModels", new[] { "EmployeeID_EmployeeID" });
            AddColumn("dbo.EmployeeDBModels", "UserName_ID", c => c.Int());
            CreateIndex("dbo.EmployeeDBModels", "UserName_ID");
            AddForeignKey("dbo.EmployeeDBModels", "UserName_ID", "dbo.LoginDBModels", "ID");
            //DropTable("dbo.UserImageDBModels");
        }
        
        public override void Down()
        {
            //CreateTable(
            //    "dbo.UserImageDBModels",
            //    c => new
            //        {
            //            ImageID = c.Int(nullable: false, identity: true),
            //            ProfileImage = c.Binary(storeType: "image"),
            //            EmployeeID_EmployeeID = c.Int(),
            //        })
            //    .PrimaryKey(t => t.ImageID);
            
            DropForeignKey("dbo.EmployeeDBModels", "UserName_ID", "dbo.LoginDBModels");
            DropIndex("dbo.EmployeeDBModels", new[] { "UserName_ID" });
            DropColumn("dbo.EmployeeDBModels", "UserName_ID");
            //CreateIndex("dbo.UserImageDBModels", "EmployeeID_EmployeeID");
            //AddForeignKey("dbo.UserImageDBModels", "EmployeeID_EmployeeID", "dbo.EmployeeDBModels", "EmployeeID");
        }
    }
}
