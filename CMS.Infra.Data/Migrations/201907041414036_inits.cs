namespace CMS.Infra.Data.Context
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inits : DbMigration
    {
        public override void Up()
        {
            AlterColumn("CMS.Language", "LanguageCulture", c => c.String(maxLength: 200));
            AlterColumn("CMS.Language", "UniqueSeoCode", c => c.String(maxLength: 200));
            AlterColumn("CMS.Language", "CreatedBy", c => c.String(maxLength: 200));
            AlterColumn("CMS.ContentMapping", "Title", c => c.String(maxLength: 200));
            AlterColumn("CMS.ContentMapping", "SubTitle", c => c.String(maxLength: 200));
            AlterColumn("CMS.ContentMapping", "Description", c => c.String(maxLength: 200));
            AlterColumn("CMS.Content", "ContentName", c => c.String(maxLength: 200));
            AlterColumn("CMS.Content", "CreatedBy", c => c.String(maxLength: 200));
            AlterColumn("CMS.Config", "Logo", c => c.String(maxLength: 200));
            AlterColumn("dbo.Menu", "Name", c => c.String(maxLength: 200));
            AlterColumn("dbo.Menu", "Url", c => c.String(maxLength: 200));
            AlterColumn("dbo.Menu", "Icon", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Menu", "Icon", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Menu", "Url", c => c.String(maxLength: 100));
            AlterColumn("dbo.Menu", "Name", c => c.String(maxLength: 100));
            AlterColumn("CMS.Config", "Logo", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("CMS.Content", "CreatedBy", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("CMS.Content", "ContentName", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("CMS.ContentMapping", "Description", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("CMS.ContentMapping", "SubTitle", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("CMS.ContentMapping", "Title", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("CMS.Language", "CreatedBy", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("CMS.Language", "UniqueSeoCode", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("CMS.Language", "LanguageCulture", c => c.String(maxLength: 100, unicode: false));
        }
    }
}
