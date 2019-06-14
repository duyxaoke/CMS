namespace CMS.Infra.Data.Context
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addpartner : DbMigration
    {
        public override void Up()
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
            
        }
        
        public override void Down()
        {
            DropTable("CMS.Partner");
        }
    }
}
