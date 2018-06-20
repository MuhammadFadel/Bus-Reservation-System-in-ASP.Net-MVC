namespace OnlineBusReservationV6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTripForignKeyAndPictureAttributes : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Trips", name: "Bus_Id", newName: "BusId");
            RenameColumn(table: "dbo.Trips", name: "Line_Id", newName: "LineId");
            RenameIndex(table: "dbo.Trips", name: "IX_Line_Id", newName: "IX_LineId");
            RenameIndex(table: "dbo.Trips", name: "IX_Bus_Id", newName: "IX_BusId");
            AddColumn("dbo.Trips", "TripPicture", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Trips", "TripPicture");
            RenameIndex(table: "dbo.Trips", name: "IX_BusId", newName: "IX_Bus_Id");
            RenameIndex(table: "dbo.Trips", name: "IX_LineId", newName: "IX_Line_Id");
            RenameColumn(table: "dbo.Trips", name: "LineId", newName: "Line_Id");
            RenameColumn(table: "dbo.Trips", name: "BusId", newName: "Bus_Id");
        }
    }
}
