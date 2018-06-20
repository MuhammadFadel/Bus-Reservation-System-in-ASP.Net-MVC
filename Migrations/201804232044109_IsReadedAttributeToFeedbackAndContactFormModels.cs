namespace OnlineBusReservationV6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsReadedAttributeToFeedbackAndContactFormModels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContactUsForms", "IsReaded", c => c.Boolean(nullable: false));
            AddColumn("dbo.Feedbacks", "Timestamp", c => c.String(nullable: false));
            AddColumn("dbo.Feedbacks", "IsReadeds", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Feedbacks", "IsReadeds");
            DropColumn("dbo.Feedbacks", "Timestamp");
            DropColumn("dbo.ContactUsForms", "IsReaded");
        }
    }
}
