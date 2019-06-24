namespace CMS.Infra.Data.Context
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addlanguage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "CMS.Language",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100, unicode: false),
                        LanguageCulture = c.String(maxLength: 100, unicode: false),
                        UniqueSeoCode = c.String(maxLength: 100, unicode: false),
                        IsActive = c.Boolean(nullable: false),
                        DisplayOrder = c.Int(nullable: false),
                        CreatedBy = c.String(maxLength: 100, unicode: false),
                        CreatedTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("CMS.Partner");
        }
        
        public override void Down()
        {
            CreateTable(
                "CMS.Partner",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 250),
                        Logo = c.String(maxLength: 100, unicode: false),
                        Description = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("CMS.Language");
        }
    }
}
