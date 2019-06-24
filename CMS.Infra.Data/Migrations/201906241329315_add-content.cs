namespace CMS.Infra.Data.Context
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcontent : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "CMS.Content",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContentName = c.String(maxLength: 100, unicode: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 100, unicode: false),
                        CreatedTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("CMS.Content");
        }
    }
}
