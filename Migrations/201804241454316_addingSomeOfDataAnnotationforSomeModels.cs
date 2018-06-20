namespace OnlineBusReservationV6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingSomeOfDataAnnotationforSomeModels : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Seats", "Passenger_Id", "dbo.Passengers");
            DropForeignKey("dbo.Passengers", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Seats", new[] { "Passenger_Id" });
            DropIndex("dbo.Passengers", new[] { "User_Id" });
            AddColumn("dbo.Seats", "Time", c => c.String(nullable: false));
            AlterColumn("dbo.Seats", "Passenger_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Passengers", "User_Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Lines", "From", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Lines", "To", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Drivers", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.ContactUsForms", "Name", c => c.String(nullable: false, maxLength: 100));
            CreateIndex("dbo.Seats", "Passenger_Id");
            CreateIndex("dbo.Passengers", "User_Id");
            AddForeignKey("dbo.Seats", "Passenger_Id", "dbo.Passengers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Passengers", "User_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Passengers", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Seats", "Passenger_Id", "dbo.Passengers");
            DropIndex("dbo.Passengers", new[] { "User_Id" });
            DropIndex("dbo.Seats", new[] { "Passenger_Id" });
            AlterColumn("dbo.ContactUsForms", "Name", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("dbo.Drivers", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Lines", "To", c => c.String(nullable: false));
            AlterColumn("dbo.Lines", "From", c => c.String(nullable: false));
            AlterColumn("dbo.Passengers", "User_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Seats", "Passenger_Id", c => c.Int());
            DropColumn("dbo.Seats", "Time");
            CreateIndex("dbo.Passengers", "User_Id");
            CreateIndex("dbo.Seats", "Passenger_Id");
            AddForeignKey("dbo.Passengers", "User_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Seats", "Passenger_Id", "dbo.Passengers", "Id");
        }
    }
}
