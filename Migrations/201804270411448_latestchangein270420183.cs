namespace OnlineBusReservationV6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class latestchangein270420183 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Passengers", "Blocked", c => c.Boolean(nullable: false));
            DropColumn("dbo.Passengers", "IsBlocked");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Passengers", "IsBlocked", c => c.Int(nullable: false));
            DropColumn("dbo.Passengers", "Blocked");
        }
    }
}
