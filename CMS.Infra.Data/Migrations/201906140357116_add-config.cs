namespace CMS.Infra.Data.Context
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addconfig : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "CMS.Config",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 250),
                        Keyword = c.String(maxLength: 4000),
                        Description = c.String(maxLength: 4000),
                        Logo = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("CMS.Config");
        }
    }
}
