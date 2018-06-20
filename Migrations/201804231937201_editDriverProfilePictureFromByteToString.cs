namespace OnlineBusReservationV6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editDriverProfilePictureFromByteToString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Drivers", "ProfilePicture", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Drivers", "ProfilePicture", c => c.Byte(nullable: false));
        }
    }
}
