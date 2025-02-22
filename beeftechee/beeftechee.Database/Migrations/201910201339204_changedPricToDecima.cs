namespace beeftechee.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedPricToDecima : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.OrderBurgers", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OrderBurgers", "Price", c => c.Int(nullable: false));
        }
    }
}
