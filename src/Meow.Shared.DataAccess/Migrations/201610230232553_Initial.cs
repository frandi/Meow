namespace Meow.Shared.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attendees",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Email = c.String(),
                        EventId = c.Guid(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EventItems", t => t.EventId, cascadeDelete: true)
                .Index(t => t.EventId);
            
            CreateTable(
                "dbo.EventItems",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        OwnerId = c.Guid(nullable: false),
                        Location = c.String(),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attendees", "EventId", "dbo.EventItems");
            DropIndex("dbo.Attendees", new[] { "EventId" });
            DropTable("dbo.EventItems");
            DropTable("dbo.Attendees");
        }
    }
}
