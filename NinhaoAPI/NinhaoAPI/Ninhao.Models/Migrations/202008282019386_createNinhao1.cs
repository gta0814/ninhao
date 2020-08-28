namespace Ninhao.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createNinhao1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Users", new[] { "CarId" });
            AlterColumn("dbo.Users", "CarId", c => c.Guid());
            CreateIndex("dbo.Users", "CarId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "CarId" });
            AlterColumn("dbo.Users", "CarId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Users", "CarId");
        }
    }
}
