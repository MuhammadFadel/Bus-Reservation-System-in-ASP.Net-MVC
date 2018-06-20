namespace OnlineBusReservationV6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingContactUsFormsMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContactUsForms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        EmailAddress = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        TimeStamp = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ContactUsForms");
        }
    }
}
