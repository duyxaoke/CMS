namespace CMS.Infra.Data.Context
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatecategory23 : DbMigration
    {
        public override void Up()
        {
            AddColumn("CMS.Category", "CategoryName", c => c.String(maxLength: 250));
            AddColumn("CMS.CategoryMapping", "Title", c => c.String(maxLength: 250));
            DropColumn("CMS.CategoryMapping", "CategoryName");
        }
        
        public override void Down()
        {
            AddColumn("CMS.CategoryMapping", "CategoryName", c => c.String(maxLength: 250));
            DropColumn("CMS.CategoryMapping", "Title");
            DropColumn("CMS.Category", "CategoryName");
        }
    }
}
