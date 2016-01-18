namespace BrianChristyWedding.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTimestamps : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rsvps", "Created", c => c.DateTime(nullable: false));
            AddColumn("dbo.Rsvps", "Updated", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rsvps", "Updated");
            DropColumn("dbo.Rsvps", "Created");
        }
    }
}
