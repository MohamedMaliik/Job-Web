namespace Job_Offers_Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplayForJob : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplayForJobs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        ApplayDate = c.DateTime(nullable: false),
                        jobid = c.Int(nullable: false),
                        userid = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Jobs", t => t.jobid, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.userid)
                .Index(t => t.jobid)
                .Index(t => t.userid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplayForJobs", "userid", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplayForJobs", "jobid", "dbo.Jobs");
            DropIndex("dbo.ApplayForJobs", new[] { "userid" });
            DropIndex("dbo.ApplayForJobs", new[] { "jobid" });
            DropTable("dbo.ApplayForJobs");
        }
    }
}
