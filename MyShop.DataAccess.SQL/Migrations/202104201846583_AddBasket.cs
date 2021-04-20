namespace MyShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBasket : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Baskets",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BasketItems",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        productId = c.String(),
                        BasketId = c.String(maxLength: 128),
                        Quantity = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Baskets", t => t.BasketId)
                .Index(t => t.BasketId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BasketItems", "BasketId", "dbo.Baskets");
            DropIndex("dbo.BasketItems", new[] { "BasketId" });
            DropTable("dbo.BasketItems");
            DropTable("dbo.Baskets");
        }
    }
}
