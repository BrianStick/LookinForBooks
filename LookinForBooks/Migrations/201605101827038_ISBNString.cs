namespace LookinForBooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ISBNString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "Isbn", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "Isbn", c => c.Int(nullable: false));
        }
    }
}
