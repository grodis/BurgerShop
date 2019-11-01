namespace beeftechee.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedImageUrlToBurger : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Burgers", "ImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Burgers", "ImageUrl");
        }
    }
}
