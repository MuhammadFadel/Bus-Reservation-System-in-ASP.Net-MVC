namespace OnlineBusReservationV6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingInPassengerAndTicketModels4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Passengers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Passengers", new[] { "ApplicationUser_Id" });
            AlterColumn("dbo.Passengers", "ApplicationUser_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Passengers", "ApplicationUser_Id");
            AddForeignKey("dbo.Passengers", "ApplicationUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            DropColumn("dbo.Passengers", "ApplicationUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Passengers", "ApplicationUserId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Passengers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Passengers", new[] { "ApplicationUser_Id" });
            AlterColumn("dbo.Passengers", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Passengers", "ApplicationUser_Id");
            AddForeignKey("dbo.Passengers", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
