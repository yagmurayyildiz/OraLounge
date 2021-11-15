namespace Data.OraLounge.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.Categories",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false, maxLength: 50),
            //            ParentId = c.Int(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Categories", t => t.ParentId)
            //    .Index(t => t.ParentId);
            
            //CreateTable(
            //    "dbo.Products",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false, maxLength: 50),
            //            Description = c.String(),
            //            Price = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            CategoryId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
            //    .Index(t => t.CategoryId);
            
            //CreateTable(
            //    "dbo.ProductImages",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Source = c.String(nullable: false),
            //            Priority = c.Int(nullable: false),
            //            ProductId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
            //    .Index(t => t.ProductId);
            
            //CreateTable(
            //    "dbo.Users",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            FirstName = c.String(),
            //            LastName = c.String(),
            //            Username = c.String(),
            //            PasswordHash = c.Binary(),
            //            PasswordSalt = c.Binary(),
            //        })
            //    .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.ProductImages", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Categories", "ParentId", "dbo.Categories");
            DropIndex("dbo.ProductImages", new[] { "ProductId" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropIndex("dbo.Categories", new[] { "ParentId" });
            DropTable("dbo.Users");
            DropTable("dbo.ProductImages");
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
        }
    }
}
