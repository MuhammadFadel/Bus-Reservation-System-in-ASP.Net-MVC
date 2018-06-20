namespace OnlineBusReservationV6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addSSNandProfilePicToASPUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "SSN", c => c.String(nullable: false, maxLength: 14));
            AddColumn("dbo.AspNetUsers", "ProfilePicture", c => c.Byte(nullable: false));
            AddColumn("dbo.Drivers", "DriverLicence", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Drivers", "DriverLicence");
            DropColumn("dbo.AspNetUsers", "ProfilePicture");
            DropColumn("dbo.AspNetUsers", "SSN");
        }
    }
}
