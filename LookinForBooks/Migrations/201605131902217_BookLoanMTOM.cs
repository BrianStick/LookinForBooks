namespace LookinForBooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BookLoanMTOM : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookLoans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CheckedOut = c.DateTime(nullable: false),
                        CheckedIn = c.DateTime(),
                        Book_Id = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.Book_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Book_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookLoans", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.BookLoans", "Book_Id", "dbo.Books");
            DropIndex("dbo.BookLoans", new[] { "User_Id" });
            DropIndex("dbo.BookLoans", new[] { "Book_Id" });
            DropTable("dbo.BookLoans");
        }
    }
}
