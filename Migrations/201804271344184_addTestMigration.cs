namespace OnlineBusReservationV6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTestMigration : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Passengers", "Blocked");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Passengers", "Blocked", c => c.Boolean(nullable: false));
        }
    }
}
