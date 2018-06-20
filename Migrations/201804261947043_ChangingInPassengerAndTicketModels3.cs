namespace OnlineBusReservationV6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingInPassengerAndTicketModels3 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Seats", name: "Passenger_Id", newName: "PassengerId");
            RenameColumn(table: "dbo.Tickets", name: "Payment_Id", newName: "PaymentId");
            RenameIndex(table: "dbo.Seats", name: "IX_Passenger_Id", newName: "IX_PassengerId");
            RenameIndex(table: "dbo.Tickets", name: "IX_Payment_Id", newName: "IX_PaymentId");
            AddColumn("dbo.Passengers", "ApplicationUserId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Passengers", "ApplicationUserId");
            RenameIndex(table: "dbo.Tickets", name: "IX_PaymentId", newName: "IX_Payment_Id");
            RenameIndex(table: "dbo.Seats", name: "IX_PassengerId", newName: "IX_Passenger_Id");
            RenameColumn(table: "dbo.Tickets", name: "PaymentId", newName: "Payment_Id");
            RenameColumn(table: "dbo.Seats", name: "PassengerId", newName: "Passenger_Id");
        }
    }
}
