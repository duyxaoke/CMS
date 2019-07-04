namespace CMS.Infra.Data.Context
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "CMS.Category",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(maxLength: 250),
                        ParentId = c.Int(),
                        ModuleId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "CMS.CategoryMapping",
                c => new
                    {
                        CategoryMappingId = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        LanguageId = c.Int(nullable: false),
                        Title = c.String(maxLength: 250),
                        Description = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.CategoryMappingId)
                .ForeignKey("CMS.Category", t => t.CategoryId)
                .ForeignKey("CMS.Language", t => t.LanguageId)
                .Index(t => t.CategoryId)
                .Index(t => t.LanguageId);
            
            CreateTable(
                "CMS.Language",
                c => new
                    {
                        LanguageId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        LanguageCulture = c.String(maxLength: 100, unicode: false),
                        UniqueSeoCode = c.String(maxLength: 100, unicode: false),
                        IsActive = c.Boolean(nullable: false),
                        DisplayOrder = c.Int(nullable: false),
                        CreatedBy = c.String(maxLength: 100, unicode: false),
                        CreatedTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.LanguageId);
            
            CreateTable(
                "CMS.ContentMapping",
                c => new
                    {
                        ContentMappingId = c.Int(nullable: false, identity: true),
                        ContentId = c.Int(nullable: false),
                        LanguageId = c.Int(nullable: false),
                        Title = c.String(maxLength: 100, unicode: false),
                        SubTitle = c.String(maxLength: 100, unicode: false),
                        Description = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.ContentMappingId)
                .ForeignKey("CMS.Content", t => t.ContentId)
                .ForeignKey("CMS.Language", t => t.LanguageId)
                .Index(t => t.ContentId)
                .Index(t => t.LanguageId);
            
            CreateTable(
                "CMS.Content",
                c => new
                    {
                        ContentId = c.Int(nullable: false, identity: true),
                        ContentName = c.String(maxLength: 100, unicode: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 100, unicode: false),
                        CreatedTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.ContentId);
            
            CreateTable(
                "CMS.Config",
                c => new
                    {
                        ConfigId = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 250),
                        Keyword = c.String(maxLength: 4000),
                        Description = c.String(maxLength: 4000),
                        Logo = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.ConfigId);
            
            CreateTable(
                "dbo.Menu",
                c => new
                    {
                        MenuId = c.Int(nullable: false, identity: true),
                        ParentId = c.Int(),
                        Name = c.String(maxLength: 100),
                        Url = c.String(maxLength: 100),
                        Icon = c.String(maxLength: 100, unicode: false),
                        IsActive = c.Boolean(nullable: false),
                        Order = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MenuId);
            
            CreateTable(
                "dbo.MenuInRole",
                c => new
                    {
                        MenuInRoleId = c.Int(nullable: false, identity: true),
                        RoleId = c.Guid(nullable: false),
                        MenuId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MenuInRoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("CMS.CategoryMapping", "LanguageId", "CMS.Language");
            DropForeignKey("CMS.ContentMapping", "LanguageId", "CMS.Language");
            DropForeignKey("CMS.ContentMapping", "ContentId", "CMS.Content");
            DropForeignKey("CMS.CategoryMapping", "CategoryId", "CMS.Category");
            DropIndex("CMS.ContentMapping", new[] { "LanguageId" });
            DropIndex("CMS.ContentMapping", new[] { "ContentId" });
            DropIndex("CMS.CategoryMapping", new[] { "LanguageId" });
            DropIndex("CMS.CategoryMapping", new[] { "CategoryId" });
            DropTable("dbo.MenuInRole");
            DropTable("dbo.Menu");
            DropTable("CMS.Config");
            DropTable("CMS.Content");
            DropTable("CMS.ContentMapping");
            DropTable("CMS.Language");
            DropTable("CMS.CategoryMapping");
            DropTable("CMS.Category");
        }
    }
}
