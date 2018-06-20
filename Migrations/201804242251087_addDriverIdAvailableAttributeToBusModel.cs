namespace OnlineBusReservationV6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDriverIdAvailableAttributeToBusModel : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Buses", name: "Driver_Id", newName: "DriverId");
            RenameIndex(table: "dbo.Buses", name: "IX_Driver_Id", newName: "IX_DriverId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Buses", name: "IX_DriverId", newName: "IX_Driver_Id");
            RenameColumn(table: "dbo.Buses", name: "DriverId", newName: "Driver_Id");
        }
    }
}
