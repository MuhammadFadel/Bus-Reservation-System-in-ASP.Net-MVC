namespace OnlineBusReservationV6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingInPassengerAndTicketModels2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Passengers", "ApplicationUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Passengers", "ApplicationUserId", c => c.Int(nullable: false));
        }
    }
}
