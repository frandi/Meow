namespace Meow.Shared.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetRequiredFields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Attendees", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Attendees", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.EventItems", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EventItems", "Name", c => c.String());
            AlterColumn("dbo.Attendees", "Email", c => c.String());
            AlterColumn("dbo.Attendees", "Name", c => c.String());
        }
    }
}
