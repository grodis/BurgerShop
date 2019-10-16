namespace beeftechee.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Breads",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Burgers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BreadId = c.Int(nullable: false),
                        MeatId = c.Int(nullable: false),
                        CheeseId = c.Int(),
                        SauceId = c.Int(),
                        VeggieId = c.Int(),
                        Order_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Breads", t => t.BreadId, cascadeDelete: true)
                .ForeignKey("dbo.Cheese", t => t.CheeseId)
                .ForeignKey("dbo.Meats", t => t.MeatId, cascadeDelete: true)
                .ForeignKey("dbo.Sauces", t => t.SauceId)
                .ForeignKey("dbo.Veggies", t => t.VeggieId)
                .ForeignKey("dbo.Orders", t => t.Order_Id)
                .Index(t => t.BreadId)
                .Index(t => t.MeatId)
                .Index(t => t.CheeseId)
                .Index(t => t.SauceId)
                .Index(t => t.VeggieId)
                .Index(t => t.Order_Id);
            
            CreateTable(
                "dbo.Cheese",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Meats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sauces",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Veggies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Drinks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Litre = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Order_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.Order_Id)
                .Index(t => t.Order_Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Drinks", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.Burgers", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.Burgers", "VeggieId", "dbo.Veggies");
            DropForeignKey("dbo.Burgers", "SauceId", "dbo.Sauces");
            DropForeignKey("dbo.Burgers", "MeatId", "dbo.Meats");
            DropForeignKey("dbo.Burgers", "CheeseId", "dbo.Cheese");
            DropForeignKey("dbo.Burgers", "BreadId", "dbo.Breads");
            DropIndex("dbo.Drinks", new[] { "Order_Id" });
            DropIndex("dbo.Burgers", new[] { "Order_Id" });
            DropIndex("dbo.Burgers", new[] { "VeggieId" });
            DropIndex("dbo.Burgers", new[] { "SauceId" });
            DropIndex("dbo.Burgers", new[] { "CheeseId" });
            DropIndex("dbo.Burgers", new[] { "MeatId" });
            DropIndex("dbo.Burgers", new[] { "BreadId" });
            DropTable("dbo.Orders");
            DropTable("dbo.Drinks");
            DropTable("dbo.Veggies");
            DropTable("dbo.Sauces");
            DropTable("dbo.Meats");
            DropTable("dbo.Cheese");
            DropTable("dbo.Burgers");
            DropTable("dbo.Breads");
        }
    }
}
