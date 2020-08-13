namespace Ninhao.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ninhao2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "CarId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Users", "CarId");
            AddForeignKey("dbo.Users", "CarId", "dbo.Cars", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "CarId", "dbo.Cars");
            DropIndex("dbo.Users", new[] { "CarId" });
            DropColumn("dbo.Users", "CarId");
        }
    }
}
