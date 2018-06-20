namespace OnlineBusReservationV6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addSomeModelAttributes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lines", "LinePicture", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Lines", "LinePicture");
        }
    }
}
