namespace OnlineBusReservationV6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovenoRequiredAnnotationFromFeedbackModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Feedbacks", "PassengerId_Id", "dbo.Passengers");
            DropForeignKey("dbo.Feedbacks", "TripId_Id", "dbo.Trips");
            DropIndex("dbo.Feedbacks", new[] { "PassengerId_Id" });
            DropIndex("dbo.Feedbacks", new[] { "TripId_Id" });
            AlterColumn("dbo.Feedbacks", "PassengerId_Id", c => c.Int());
            AlterColumn("dbo.Feedbacks", "TripId_Id", c => c.Int());
            CreateIndex("dbo.Feedbacks", "PassengerId_Id");
            CreateIndex("dbo.Feedbacks", "TripId_Id");
            AddForeignKey("dbo.Feedbacks", "PassengerId_Id", "dbo.Passengers", "Id");
            AddForeignKey("dbo.Feedbacks", "TripId_Id", "dbo.Trips", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Feedbacks", "TripId_Id", "dbo.Trips");
            DropForeignKey("dbo.Feedbacks", "PassengerId_Id", "dbo.Passengers");
            DropIndex("dbo.Feedbacks", new[] { "TripId_Id" });
            DropIndex("dbo.Feedbacks", new[] { "PassengerId_Id" });
            AlterColumn("dbo.Feedbacks", "TripId_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Feedbacks", "PassengerId_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Feedbacks", "TripId_Id");
            CreateIndex("dbo.Feedbacks", "PassengerId_Id");
            AddForeignKey("dbo.Feedbacks", "TripId_Id", "dbo.Trips", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Feedbacks", "PassengerId_Id", "dbo.Passengers", "Id", cascadeDelete: true);
        }
    }
}
