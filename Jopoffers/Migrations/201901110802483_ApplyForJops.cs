namespace Jopoffers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplyForJops : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplyForJops",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        message = c.String(),
                        ApplyDate = c.DateTime(nullable: false),
                        JopId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Jops", t => t.JopId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.JopId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplyForJops", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplyForJops", "JopId", "dbo.Jops");
            DropIndex("dbo.ApplyForJops", new[] { "UserId" });
            DropIndex("dbo.ApplyForJops", new[] { "JopId" });
            DropTable("dbo.ApplyForJops");
        }
    }
}
