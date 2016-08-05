namespace Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClayDoors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        CreatedUTC = c.DateTime(nullable: false, precision: 0),
                        UpdatedUTC = c.DateTime(precision: 0),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.DoorUserIntegrations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Desc = c.String(maxLength: 250, storeType: "nvarchar"),
                        ClayUserId = c.Int(nullable: false),
                        ClayDoorId = c.Int(nullable: false),
                        CreatedUTC = c.DateTime(nullable: false, precision: 0),
                        UpdatedUTC = c.DateTime(precision: 0),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClayDoors", t => t.ClayDoorId, cascadeDelete: true)
                .ForeignKey("dbo.ClayUsers", t => t.ClayUserId, cascadeDelete: true)
                .Index(t => t.ClayUserId)
                .Index(t => t.ClayDoorId);
            
            CreateTable(
                "dbo.ClayUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        LoginName = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        Token = c.String(nullable: false, maxLength: 150, storeType: "nvarchar"),
                        CreatedUTC = c.DateTime(nullable: false, precision: 0),
                        UpdatedUTC = c.DateTime(precision: 0),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DoorUserIntegrations", "ClayUserId", "dbo.ClayUsers");
            DropForeignKey("dbo.DoorUserIntegrations", "ClayDoorId", "dbo.ClayDoors");
            DropIndex("dbo.ClayUsers", new[] { "Name" });
            DropIndex("dbo.DoorUserIntegrations", new[] { "ClayDoorId" });
            DropIndex("dbo.DoorUserIntegrations", new[] { "ClayUserId" });
            DropIndex("dbo.ClayDoors", new[] { "Name" });
            DropTable("dbo.ClayUsers");
            DropTable("dbo.DoorUserIntegrations");
            DropTable("dbo.ClayDoors");
        }
    }
}
