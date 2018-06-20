namespace OnlineBusReservationV6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addBusPictureAttributeToBusModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Buses", "BusPicture", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Buses", "BusPicture");
        }
    }
}
