namespace Jopoffers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditJop : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jops", "UserID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Jops", "UserID");
            AddForeignKey("dbo.Jops", "UserID", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jops", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.Jops", new[] { "UserID" });
            DropColumn("dbo.Jops", "UserID");
        }
    }
}
