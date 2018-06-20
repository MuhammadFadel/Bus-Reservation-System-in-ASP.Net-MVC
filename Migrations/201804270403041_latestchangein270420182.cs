namespace OnlineBusReservationV6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class latestchangein270420182 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Passengers", "IsBlocked", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Passengers", "IsBlocked", c => c.Boolean(nullable: false));
        }
    }
}
