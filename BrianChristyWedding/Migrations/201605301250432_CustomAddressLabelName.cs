namespace BrianChristyWedding.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomAddressLabelName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invitations", "CustomAddressLabelName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invitations", "CustomAddressLabelName");
        }
    }
}
