namespace CMS.Infra.Data.Context
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class akdf : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Menus", newName: "Menu");
            RenameTable(name: "dbo.MenuInRoles", newName: "MenuInRole");
            DropForeignKey("CMS.CategoryMapping", "CategoryId", "CMS.Category");
            DropForeignKey("CMS.CategoryMapping", "LanguageId", "CMS.Language");
            DropForeignKey("CMS.ContentMapping", "LanguageId", "CMS.Language");
            DropForeignKey("CMS.ContentMapping", "ContentId", "CMS.Content");
            AddColumn("CMS.Language", "IsDefault", c => c.Boolean(nullable: false));
            AddForeignKey("CMS.CategoryMapping", "CategoryId", "CMS.Category", "Id");
            AddForeignKey("CMS.CategoryMapping", "LanguageId", "CMS.Language", "Id");
            AddForeignKey("CMS.ContentMapping", "LanguageId", "CMS.Language", "Id");
            AddForeignKey("CMS.ContentMapping", "ContentId", "CMS.Content", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("CMS.ContentMapping", "ContentId", "CMS.Content");
            DropForeignKey("CMS.ContentMapping", "LanguageId", "CMS.Language");
            DropForeignKey("CMS.CategoryMapping", "LanguageId", "CMS.Language");
            DropForeignKey("CMS.CategoryMapping", "CategoryId", "CMS.Category");
            DropColumn("CMS.Language", "IsDefault");
            AddForeignKey("CMS.ContentMapping", "ContentId", "CMS.Content", "Id", cascadeDelete: true);
            AddForeignKey("CMS.ContentMapping", "LanguageId", "CMS.Language", "Id", cascadeDelete: true);
            AddForeignKey("CMS.CategoryMapping", "LanguageId", "CMS.Language", "Id", cascadeDelete: true);
            AddForeignKey("CMS.CategoryMapping", "CategoryId", "CMS.Category", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.MenuInRole", newName: "MenuInRoles");
            RenameTable(name: "dbo.Menu", newName: "Menus");
        }
    }
}
