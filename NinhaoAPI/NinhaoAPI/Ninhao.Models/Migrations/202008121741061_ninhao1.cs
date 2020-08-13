namespace Ninhao.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ninhao1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Make = c.String(maxLength: 100, unicode: false),
                        Color = c.String(maxLength: 40, unicode: false),
                        PlateNumber = c.String(maxLength: 10, unicode: false),
                        Type = c.String(maxLength: 40, unicode: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Cars");
        }
    }
}
