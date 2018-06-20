namespace OnlineBusReservationV6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFeedbackModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Feedbacks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FeedbackMessage = c.String(nullable: false, maxLength: 500),
                        PassengerId_Id = c.Int(nullable: false),
                        TripId_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Passengers", t => t.PassengerId_Id, cascadeDelete: true)
                .ForeignKey("dbo.Trips", t => t.TripId_Id, cascadeDelete: true)
                .Index(t => t.PassengerId_Id)
                .Index(t => t.TripId_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Feedbacks", "TripId_Id", "dbo.Trips");
            DropForeignKey("dbo.Feedbacks", "PassengerId_Id", "dbo.Passengers");
            DropIndex("dbo.Feedbacks", new[] { "TripId_Id" });
            DropIndex("dbo.Feedbacks", new[] { "PassengerId_Id" });
            DropTable("dbo.Feedbacks");
        }
    }
}
