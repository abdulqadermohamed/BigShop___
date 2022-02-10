namespace BigShop___.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hamed1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "isdeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "isdeleted");
        }
    }
}
