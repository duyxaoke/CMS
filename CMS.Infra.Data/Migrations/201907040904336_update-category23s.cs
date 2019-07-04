namespace CMS.Infra.Data.Context
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatecategory23s : DbMigration
    {
        public override void Up()
        {
            AlterColumn("CMS.Category", "ParentId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("CMS.Category", "ParentId", c => c.Int(nullable: false));
        }
    }
}
