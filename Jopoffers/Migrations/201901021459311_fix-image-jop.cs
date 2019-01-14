namespace Jopoffers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fiximagejop : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Jops", "image", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Jops", "image", c => c.String(nullable: false));
        }
    }
}
