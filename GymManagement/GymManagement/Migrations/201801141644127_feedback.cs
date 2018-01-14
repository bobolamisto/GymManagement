namespace GymManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class feedback : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Feedbacks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        UserFullName = c.String(),
                        Date = c.DateTime(nullable: false),
                        UserId = c.String(maxLength: 128),
                        CourseId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.CourseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Feedbacks", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Feedbacks", "CourseId", "dbo.Courses");
            DropIndex("dbo.Feedbacks", new[] { "CourseId" });
            DropIndex("dbo.Feedbacks", new[] { "UserId" });
            DropTable("dbo.Feedbacks");
        }
    }
}
