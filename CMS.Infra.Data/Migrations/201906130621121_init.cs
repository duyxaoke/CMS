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
                        PartnerID = c.Int(nullable: false),
                        CategoryCode = c.String(maxLength: 50, unicode: false),
                        CategoryName = c.String(maxLength: 250),
                        PercentCommission = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Menu",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentId = c.Int(),
                        Name = c.String(maxLength: 100),
                        Url = c.String(maxLength: 100),
                        Icon = c.String(maxLength: 100, unicode: false),
                        IsActive = c.Boolean(nullable: false),
                        Order = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MenuInRole",
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
            DropTable("dbo.MenuInRole");
            DropTable("dbo.Menu");
            DropTable("CMS.Category");
        }
    }
}
