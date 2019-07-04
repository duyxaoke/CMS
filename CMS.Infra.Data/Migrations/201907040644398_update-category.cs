namespace CMS.Infra.Data.Context
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatecategory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "CMS.CategoryMapping",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        LanguageId = c.Int(nullable: false),
                        CategoryName = c.String(maxLength: 250),
                        Description = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("CMS.Category", t => t.CategoryId)
                .ForeignKey("CMS.Language", t => t.LanguageId)
                .Index(t => t.CategoryId)
                .Index(t => t.LanguageId);
            
            DropColumn("CMS.Category", "CategoryName");
            DropColumn("CMS.Category", "Description");
        }
        
        public override void Down()
        {
            AddColumn("CMS.Category", "Description", c => c.String(maxLength: 4000));
            AddColumn("CMS.Category", "CategoryName", c => c.String(maxLength: 250));
            DropForeignKey("CMS.CategoryMapping", "LanguageId", "CMS.Language");
            DropForeignKey("CMS.CategoryMapping", "CategoryId", "CMS.Category");
            DropIndex("CMS.CategoryMapping", new[] { "LanguageId" });
            DropIndex("CMS.CategoryMapping", new[] { "CategoryId" });
            DropTable("CMS.CategoryMapping");
        }
    }
}
