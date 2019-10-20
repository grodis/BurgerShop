namespace beeftechee.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedOrderDrinkAndOrderBurger : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Burgers", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.Drinks", "Order_Id", "dbo.Orders");
            DropIndex("dbo.Burgers", new[] { "Order_Id" });
            DropIndex("dbo.Drinks", new[] { "Order_Id" });
            CreateTable(
                "dbo.OrderBurgers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        Name = c.String(),
                        Price = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        MeatName = c.String(),
                        BreadName = c.String(),
                        CheeseName = c.String(),
                        SauceName = c.String(),
                        VeggieName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.OrderDrinks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        Name = c.String(),
                        Litre = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId);
            
            DropColumn("dbo.Burgers", "Order_Id");
            DropColumn("dbo.Drinks", "Order_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Drinks", "Order_Id", c => c.Int());
            AddColumn("dbo.Burgers", "Order_Id", c => c.Int());
            DropForeignKey("dbo.OrderDrinks", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.OrderBurgers", "OrderId", "dbo.Orders");
            DropIndex("dbo.OrderDrinks", new[] { "OrderId" });
            DropIndex("dbo.OrderBurgers", new[] { "OrderId" });
            DropTable("dbo.OrderDrinks");
            DropTable("dbo.OrderBurgers");
            CreateIndex("dbo.Drinks", "Order_Id");
            CreateIndex("dbo.Burgers", "Order_Id");
            AddForeignKey("dbo.Drinks", "Order_Id", "dbo.Orders", "Id");
            AddForeignKey("dbo.Burgers", "Order_Id", "dbo.Orders", "Id");
        }
    }
}
