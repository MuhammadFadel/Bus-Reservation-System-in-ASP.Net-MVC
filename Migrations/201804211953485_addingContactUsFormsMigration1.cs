namespace OnlineBusReservationV6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingContactUsFormsMigration1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContactUsForms", "Message", c => c.String(nullable: false, maxLength: 500));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContactUsForms", "Message");
        }
    }
}
