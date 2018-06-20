namespace OnlineBusReservationV6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testProfilePictureForDriver : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Drivers", "PhoneNumber", c => c.String(nullable: false, maxLength: 11));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Drivers", "PhoneNumber", c => c.String(nullable: false));
        }
    }
}
