namespace OnlineBusReservationV6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditingOfSomeModels : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Trips", "Time", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Trips", "Time", c => c.String(nullable: false));
        }
    }
}
