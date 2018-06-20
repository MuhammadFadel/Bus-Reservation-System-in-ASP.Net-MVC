namespace OnlineBusReservationV6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDriverAvailableAttributeToDriverModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Drivers", "IsAvailable", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Drivers", "IsAvailable");
        }
    }
}
