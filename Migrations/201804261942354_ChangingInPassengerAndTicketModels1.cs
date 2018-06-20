namespace OnlineBusReservationV6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingInPassengerAndTicketModels1 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Passengers", name: "User_Id", newName: "ApplicationUser_Id");
            RenameIndex(table: "dbo.Passengers", name: "IX_User_Id", newName: "IX_ApplicationUser_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Passengers", name: "IX_ApplicationUser_Id", newName: "IX_User_Id");
            RenameColumn(table: "dbo.Passengers", name: "ApplicationUser_Id", newName: "User_Id");
        }
    }
}
