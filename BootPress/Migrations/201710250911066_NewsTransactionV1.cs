namespace BootPress.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewsTransactionV1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NewsTransactions",
                c => new
                    {
                        IdTransaction = c.String(nullable: false, maxLength: 128),
                        Approved = c.DateTimeOffset(nullable: false, precision: 7),
                        Price = c.Double(nullable: false),
                        Notes = c.String(),
                        News_Permalink = c.String(maxLength: 128),
                        Staff_Id = c.String(maxLength: 128),
                        Writer_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.IdTransaction)
                .ForeignKey("dbo.News", t => t.News_Permalink)
                .ForeignKey("dbo.Staffs", t => t.Staff_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Writer_Id)
                .Index(t => t.News_Permalink)
                .Index(t => t.Staff_Id)
                .Index(t => t.Writer_Id);
            
            AddColumn("dbo.News", "Price", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NewsTransactions", "Writer_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.NewsTransactions", "Staff_Id", "dbo.Staffs");
            DropForeignKey("dbo.NewsTransactions", "News_Permalink", "dbo.News");
            DropIndex("dbo.NewsTransactions", new[] { "Writer_Id" });
            DropIndex("dbo.NewsTransactions", new[] { "Staff_Id" });
            DropIndex("dbo.NewsTransactions", new[] { "News_Permalink" });
            DropColumn("dbo.News", "Price");
            DropTable("dbo.NewsTransactions");
        }
    }
}
