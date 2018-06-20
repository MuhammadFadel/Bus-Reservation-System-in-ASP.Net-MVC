namespace OnlineBusReservationV6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProfilePicture : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Passengers", "IsBlocked", c => c.Boolean(nullable: false));
            AlterColumn("dbo.AspNetUsers", "ProfilePicture", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "ProfilePicture", c => c.Byte(nullable: false));
            DropColumn("dbo.Passengers", "IsBlocked");
        }
    }
}
