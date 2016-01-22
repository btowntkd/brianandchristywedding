namespace BrianChristyWedding.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Guests",
                c => new
                    {
                        GuestID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        RsvpID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GuestID)
                .ForeignKey("dbo.Rsvps", t => t.RsvpID, cascadeDelete: true)
                .Index(t => t.RsvpID);
            
            CreateTable(
                "dbo.Rsvps",
                c => new
                    {
                        InvitationID = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.InvitationID)
                .ForeignKey("dbo.Invitations", t => t.InvitationID, cascadeDelete: true)
                .Index(t => t.InvitationID);
            
            CreateTable(
                "dbo.Invitations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        MaxAllowedGuests = c.Int(nullable: false),
                        Shortcode = c.String(maxLength: 4),
                        Address_AddressLine1 = c.String(),
                        Address_AddressLine2 = c.String(),
                        Address_City = c.String(),
                        Address_State = c.String(),
                        Address_Zip = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Shortcode, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Guests", "RsvpID", "dbo.Rsvps");
            DropForeignKey("dbo.Rsvps", "InvitationID", "dbo.Invitations");
            DropIndex("dbo.Invitations", new[] { "Shortcode" });
            DropIndex("dbo.Rsvps", new[] { "InvitationID" });
            DropIndex("dbo.Guests", new[] { "RsvpID" });
            DropTable("dbo.Invitations");
            DropTable("dbo.Rsvps");
            DropTable("dbo.Guests");
        }
    }
}
