namespace CMS.Infra.Data.Context
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatecategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("CMS.Category", "ParentID", c => c.Int(nullable: false));
            AddColumn("CMS.Category", "Description", c => c.String(maxLength: 4000));
            DropColumn("CMS.Category", "PartnerID");
            DropColumn("CMS.Category", "CategoryCode");
            DropColumn("CMS.Category", "PercentCommission");
            DropColumn("CMS.Category", "IsActive");
        }
        
        public override void Down()
        {
            AddColumn("CMS.Category", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("CMS.Category", "PercentCommission", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("CMS.Category", "CategoryCode", c => c.String(maxLength: 50, unicode: false));
            AddColumn("CMS.Category", "PartnerID", c => c.Int(nullable: false));
            DropColumn("CMS.Category", "Description");
            DropColumn("CMS.Category", "ParentID");
        }
    }
}
