namespace LookinForBooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BookLoanCheckedOutBy : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.BookLoans", name: "User_Id", newName: "CheckedOutBy_Id");
            RenameIndex(table: "dbo.BookLoans", name: "IX_User_Id", newName: "IX_CheckedOutBy_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.BookLoans", name: "IX_CheckedOutBy_Id", newName: "IX_User_Id");
            RenameColumn(table: "dbo.BookLoans", name: "CheckedOutBy_Id", newName: "User_Id");
        }
    }
}
