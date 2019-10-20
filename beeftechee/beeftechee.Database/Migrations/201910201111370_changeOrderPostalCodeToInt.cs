namespace beeftechee.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeOrderPostalCodeToInt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "PostalCode", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "PostalCode", c => c.String());
        }
    }
}
