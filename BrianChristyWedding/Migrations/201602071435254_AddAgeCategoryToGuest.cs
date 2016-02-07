namespace BrianChristyWedding.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAgeCategoryToGuest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Guests", "Age", c => c.Int(nullable: false));
            AlterColumn("dbo.Guests", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Guests", "LastName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Guests", "LastName", c => c.String());
            AlterColumn("dbo.Guests", "FirstName", c => c.String());
            DropColumn("dbo.Guests", "Age");
        }
    }
}
