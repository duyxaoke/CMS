namespace CMS.Infra.Data.Context
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatecategoryád : DbMigration
    {
        public override void Up()
        {
            AddColumn("CMS.Category", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("CMS.Category", "IsActive");
        }
    }
}
