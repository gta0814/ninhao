namespace Ninhao.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createNinhao : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Trips",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StartFrom = c.String(nullable: false, maxLength: 8000, unicode: false),
                        Destination = c.String(nullable: false, maxLength: 8000, unicode: false),
                        TimeLeave = c.DateTime(nullable: false),
                        AvailiableSeat = c.Int(nullable: false),
                        PricePerSeat = c.Decimal(precision: 18, scale: 2),
                        DriverId = c.Guid(nullable: false),
                        PassengerId = c.Guid(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.DriverId)
                .ForeignKey("dbo.Users", t => t.PassengerId)
                .Index(t => t.DriverId)
                .Index(t => t.PassengerId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Email = c.String(nullable: false, maxLength: 8000, unicode: false),
                        Password = c.String(nullable: false, maxLength: 30, unicode: false),
                        ImagePath = c.String(maxLength: 300, unicode: false),
                        Contact = c.String(),
                        Phone = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trips", "PassengerId", "dbo.Users");
            DropForeignKey("dbo.Trips", "DriverId", "dbo.Users");
            DropIndex("dbo.Trips", new[] { "PassengerId" });
            DropIndex("dbo.Trips", new[] { "DriverId" });
            DropTable("dbo.Users");
            DropTable("dbo.Trips");
        }
    }
}
