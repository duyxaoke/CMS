namespace CMS.Infra.Data.Context
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatecategorymm : DbMigration
    {
        public override void Up()
        {
            AddColumn("CMS.Category", "ModuleID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("CMS.Category", "ModuleID");
        }
    }
}
