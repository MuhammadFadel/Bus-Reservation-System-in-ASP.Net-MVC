namespace OnlineBusReservationV6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingInPassengerAndTicketModels : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Passengers", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Passengers", new[] { "User_Id" });
            AddColumn("dbo.Passengers", "ApplicationUserId", c => c.Int(nullable: false));
            AddColumn("dbo.Tickets", "IsBlocked", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Passengers", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Passengers", "User_Id");
            AddForeignKey("dbo.Passengers", "User_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Passengers", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Passengers", new[] { "User_Id" });
            AlterColumn("dbo.Passengers", "User_Id", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Tickets", "IsBlocked");
            DropColumn("dbo.Passengers", "ApplicationUserId");
            CreateIndex("dbo.Passengers", "User_Id");
            AddForeignKey("dbo.Passengers", "User_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
