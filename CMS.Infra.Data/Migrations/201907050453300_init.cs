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
                        Id = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(maxLength: 250),
                        ParentId = c.Int(),
                        ModuleId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                .ForeignKey("CMS.Category", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("CMS.Language", t => t.LanguageId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.LanguageId);
            
            CreateTable(
                "CMS.Language",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        LanguageCulture = c.String(maxLength: 200),
                        UniqueSeoCode = c.String(maxLength: 200),
                        IsActive = c.Boolean(nullable: false),
                        DisplayOrder = c.Int(nullable: false),
                        CreatedBy = c.String(maxLength: 200),
                        CreatedTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "CMS.ContentMapping",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContentId = c.Int(nullable: false),
                        LanguageId = c.Int(nullable: false),
                        Title = c.String(maxLength: 200),
                        SubTitle = c.String(maxLength: 200),
                        Description = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("CMS.Content", t => t.ContentId, cascadeDelete: true)
                .ForeignKey("CMS.Language", t => t.LanguageId, cascadeDelete: true)
                .Index(t => t.ContentId)
                .Index(t => t.LanguageId);
            
            CreateTable(
                "CMS.Content",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContentName = c.String(maxLength: 200),
                        IsActive = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 200),
                        CreatedTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "CMS.Config",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 250),
                        Keyword = c.String(maxLength: 4000),
                        Description = c.String(maxLength: 4000),
                        Logo = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Menus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentId = c.Int(),
                        Name = c.String(maxLength: 200),
                        Url = c.String(maxLength: 200),
                        Icon = c.String(maxLength: 200),
                        IsActive = c.Boolean(nullable: false),
                        Order = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MenuInRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleId = c.Guid(nullable: false),
                        MenuId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
            DropTable("dbo.MenuInRoles");
            DropTable("dbo.Menus");
            DropTable("CMS.Config");
            DropTable("CMS.Content");
            DropTable("CMS.ContentMapping");
            DropTable("CMS.Language");
            DropTable("CMS.CategoryMapping");
            DropTable("CMS.Category");
        }
    }
}
