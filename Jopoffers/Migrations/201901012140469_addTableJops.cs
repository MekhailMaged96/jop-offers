namespace Jopoffers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTableJops : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Jops",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        title = c.String(nullable: false),
                        content = c.String(nullable: false),
                        image = c.String(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jops", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Jops", new[] { "CategoryId" });
            DropTable("dbo.Jops");
        }
    }
}
