namespace Ninhao.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createNinhao : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Make = c.String(maxLength: 100, unicode: false),
                        CarModel = c.String(maxLength: 100, unicode: false),
                        Color = c.String(maxLength: 40, unicode: false),
                        Type = c.String(maxLength: 40, unicode: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                        Note = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Email = c.String(nullable: false, maxLength: 40, unicode: false),
                        Password = c.String(nullable: false, maxLength: 30, unicode: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        NickName = c.String(),
                        age = c.Int(),
                        Gender = c.String(nullable: false, maxLength: 10, unicode: false),
                        ImagePath = c.String(maxLength: 300, unicode: false),
                        SocialMediaAccount = c.String(maxLength: 30, unicode: false),
                        Phone = c.Long(nullable: false),
                        Address = c.String(),
                        CarPlate = c.String(maxLength: 10, unicode: false),
                        CarId = c.Guid(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cars", t => t.CarId)
                .Index(t => t.CarId);
            
            CreateTable(
                "dbo.UsersTrips",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        TripId = c.Guid(nullable: false),
                        IsDriver = c.Boolean(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Trips", t => t.TripId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.TripId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UsersTrips", "UserId", "dbo.Users");
            DropForeignKey("dbo.UsersTrips", "TripId", "dbo.Trips");
            DropForeignKey("dbo.Users", "CarId", "dbo.Cars");
            DropIndex("dbo.UsersTrips", new[] { "TripId" });
            DropIndex("dbo.UsersTrips", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "CarId" });
            DropTable("dbo.UsersTrips");
            DropTable("dbo.Users");
            DropTable("dbo.Trips");
            DropTable("dbo.Cars");
        }
    }
}
