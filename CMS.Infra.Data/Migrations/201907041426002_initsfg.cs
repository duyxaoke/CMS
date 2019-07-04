namespace CMS.Infra.Data.Context
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initsfg : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Menu", newName: "Menus");
            RenameTable(name: "dbo.MenuInRole", newName: "MenuInRoles");
            DropForeignKey("CMS.CategoryMapping", "CategoryId", "CMS.Category");
            DropForeignKey("CMS.CategoryMapping", "LanguageId", "CMS.Language");
            DropForeignKey("CMS.ContentMapping", "LanguageId", "CMS.Language");
            DropForeignKey("CMS.ContentMapping", "ContentId", "CMS.Content");
            AddForeignKey("CMS.CategoryMapping", "CategoryId", "CMS.Category", "CategoryId", cascadeDelete: true);
            AddForeignKey("CMS.CategoryMapping", "LanguageId", "CMS.Language", "LanguageId", cascadeDelete: true);
            AddForeignKey("CMS.ContentMapping", "LanguageId", "CMS.Language", "LanguageId", cascadeDelete: true);
            AddForeignKey("CMS.ContentMapping", "ContentId", "CMS.Content", "ContentId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("CMS.ContentMapping", "ContentId", "CMS.Content");
            DropForeignKey("CMS.ContentMapping", "LanguageId", "CMS.Language");
            DropForeignKey("CMS.CategoryMapping", "LanguageId", "CMS.Language");
            DropForeignKey("CMS.CategoryMapping", "CategoryId", "CMS.Category");
            AddForeignKey("CMS.ContentMapping", "ContentId", "CMS.Content", "ContentId");
            AddForeignKey("CMS.ContentMapping", "LanguageId", "CMS.Language", "LanguageId");
            AddForeignKey("CMS.CategoryMapping", "LanguageId", "CMS.Language", "LanguageId");
            AddForeignKey("CMS.CategoryMapping", "CategoryId", "CMS.Category", "CategoryId");
            RenameTable(name: "dbo.MenuInRoles", newName: "MenuInRole");
            RenameTable(name: "dbo.Menus", newName: "Menu");
        }
    }
}
