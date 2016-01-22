namespace BrianChristyWedding.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLastNameColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Guests", "FirstName", c => c.String());
            AddColumn("dbo.Guests", "LastName", c => c.String());
            AddColumn("dbo.Invitations", "FirstName", c => c.String(nullable: false));
            AddColumn("dbo.Invitations", "LastName", c => c.String(nullable: false));
            DropColumn("dbo.Guests", "Name");
            DropColumn("dbo.Invitations", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Invitations", "Name", c => c.String());
            AddColumn("dbo.Guests", "Name", c => c.String());
            DropColumn("dbo.Invitations", "LastName");
            DropColumn("dbo.Invitations", "FirstName");
            DropColumn("dbo.Guests", "LastName");
            DropColumn("dbo.Guests", "FirstName");
        }
    }
}
