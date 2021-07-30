namespace Job_Offers_Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditJobModel2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Jobs", new[] { "User_Id" });
            DropColumn("dbo.Jobs", "UserId");
            RenameColumn(table: "dbo.Jobs", name: "User_Id", newName: "UserID");
            AlterColumn("dbo.Jobs", "UserID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Jobs", "UserID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Jobs", new[] { "UserID" });
            AlterColumn("dbo.Jobs", "UserID", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Jobs", name: "UserID", newName: "User_Id");
            AddColumn("dbo.Jobs", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Jobs", "User_Id");
        }
    }
}
