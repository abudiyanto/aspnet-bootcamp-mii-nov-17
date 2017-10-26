namespace BootPress.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserBalance : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Balance", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Balance");
        }
    }
}
