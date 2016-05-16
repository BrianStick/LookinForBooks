namespace LookinForBooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BookOwner : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Books", "Owner_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Books", new[] { "Owner_Id" });
            AlterColumn("dbo.Books", "Owner_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Books", "Owner_Id");
            AddForeignKey("dbo.Books", "Owner_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "Owner_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Books", new[] { "Owner_Id" });
            AlterColumn("dbo.Books", "Owner_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Books", "Owner_Id");
            AddForeignKey("dbo.Books", "Owner_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
