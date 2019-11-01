namespace beeftechee.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addImageDrink : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Drinks", "ImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Drinks", "ImageUrl");
        }
    }
}
