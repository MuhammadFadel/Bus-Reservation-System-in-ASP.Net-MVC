namespace OnlineBusReservationV6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTestMigration1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Passengers", "Blocked", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Passengers", "Blocked");
        }
    }
}
