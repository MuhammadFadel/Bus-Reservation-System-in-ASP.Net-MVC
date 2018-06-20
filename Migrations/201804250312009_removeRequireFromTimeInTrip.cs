namespace OnlineBusReservationV6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeRequireFromTimeInTrip : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Trips", "Time", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Trips", "Time", c => c.String(nullable: false));
        }
    }
}
