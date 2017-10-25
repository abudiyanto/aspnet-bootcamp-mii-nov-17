namespace BootPress.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewsV2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.News", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.News", "Updated", c => c.DateTimeOffset(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            DropColumn("dbo.News", "Updated");
            DropColumn("dbo.News", "IsDeleted");
        }
    }
}
