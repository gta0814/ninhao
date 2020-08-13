namespace Ninhao.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ninhao : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Email", c => c.String(nullable: false, maxLength: 40, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Email", c => c.String(nullable: false, maxLength: 8000, unicode: false));
        }
    }
}
