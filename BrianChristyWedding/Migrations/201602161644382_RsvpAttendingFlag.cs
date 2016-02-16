namespace BrianChristyWedding.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RsvpAttendingFlag : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rsvps", "Attending", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rsvps", "Attending");
        }
    }
}
