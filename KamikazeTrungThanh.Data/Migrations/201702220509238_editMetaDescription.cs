namespace KamikazeTrungThanh.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editMetaDescription : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProductCategories", "MetaDescription", c => c.String());
            AlterColumn("dbo.Products", "MetaDescription", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "MetaDescription", c => c.String(maxLength: 256));
            AlterColumn("dbo.ProductCategories", "MetaDescription", c => c.String(maxLength: 256));
        }
    }
}
