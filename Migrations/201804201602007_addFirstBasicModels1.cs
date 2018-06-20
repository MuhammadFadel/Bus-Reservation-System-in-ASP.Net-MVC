namespace OnlineBusReservationV6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFirstBasicModels1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Buses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Color = c.String(nullable: false, maxLength: 250),
                        BusNumber = c.String(nullable: false),
                        MaximumSeats = c.Int(nullable: false),
                        Driver_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Drivers", t => t.Driver_Id, cascadeDelete: true)
                .Index(t => t.Driver_Id);
            
            CreateTable(
                "dbo.Seats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsAvailable = c.Boolean(nullable: false),
                        SeatNumber = c.Int(nullable: false),
                        Passenger_Id = c.Int(),
                        Bus_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Passengers", t => t.Passenger_Id)
                .ForeignKey("dbo.Buses", t => t.Bus_Id)
                .Index(t => t.Passenger_Id)
                .Index(t => t.Bus_Id);
            
            CreateTable(
                "dbo.Passengers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BookingTime = c.String(nullable: false),
                        Payment_Id = c.Int(nullable: false),
                        Trip_Id = c.Int(nullable: false),
                        Passenger_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Payments", t => t.Payment_Id, cascadeDelete: true)
                .ForeignKey("dbo.Trips", t => t.Trip_Id, cascadeDelete: true)
                .ForeignKey("dbo.Passengers", t => t.Passenger_Id)
                .Index(t => t.Payment_Id)
                .Index(t => t.Trip_Id)
                .Index(t => t.Passenger_Id);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Method = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Trips",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Time = c.String(nullable: false),
                        Bus_Id = c.Int(nullable: false),
                        Line_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Buses", t => t.Bus_Id, cascadeDelete: true)
                .ForeignKey("dbo.Lines", t => t.Line_Id, cascadeDelete: true)
                .Index(t => t.Bus_Id)
                .Index(t => t.Line_Id);
            
            CreateTable(
                "dbo.Lines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        From = c.String(nullable: false),
                        To = c.String(nullable: false),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Drivers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        SSN = c.String(nullable: false, maxLength: 14),
                        PhoneNumber = c.String(nullable: false),
                        EmailAddress = c.String(nullable: false),
                        ProfilePicture = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Buses", "Driver_Id", "dbo.Drivers");
            DropForeignKey("dbo.Seats", "Bus_Id", "dbo.Buses");
            DropForeignKey("dbo.Seats", "Passenger_Id", "dbo.Passengers");
            DropForeignKey("dbo.Passengers", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Tickets", "Passenger_Id", "dbo.Passengers");
            DropForeignKey("dbo.Tickets", "Trip_Id", "dbo.Trips");
            DropForeignKey("dbo.Trips", "Line_Id", "dbo.Lines");
            DropForeignKey("dbo.Trips", "Bus_Id", "dbo.Buses");
            DropForeignKey("dbo.Tickets", "Payment_Id", "dbo.Payments");
            DropIndex("dbo.Trips", new[] { "Line_Id" });
            DropIndex("dbo.Trips", new[] { "Bus_Id" });
            DropIndex("dbo.Tickets", new[] { "Passenger_Id" });
            DropIndex("dbo.Tickets", new[] { "Trip_Id" });
            DropIndex("dbo.Tickets", new[] { "Payment_Id" });
            DropIndex("dbo.Passengers", new[] { "User_Id" });
            DropIndex("dbo.Seats", new[] { "Bus_Id" });
            DropIndex("dbo.Seats", new[] { "Passenger_Id" });
            DropIndex("dbo.Buses", new[] { "Driver_Id" });
            DropTable("dbo.Drivers");
            DropTable("dbo.Lines");
            DropTable("dbo.Trips");
            DropTable("dbo.Payments");
            DropTable("dbo.Tickets");
            DropTable("dbo.Passengers");
            DropTable("dbo.Seats");
            DropTable("dbo.Buses");
        }
    }
}
