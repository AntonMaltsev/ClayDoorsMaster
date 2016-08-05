namespace Api.Migrations
{
    using Api.Common;
    using Api.DAL.DTO;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Api.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            SetSqlGenerator("MySql.Data.MySqlClient", new MySql.Data.Entity.MySqlMigrationSqlGenerator());
        }

        protected override void Seed(Api.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //to avoid creating duplicate seed data. E.g.

            context.ClayDoor.AddOrUpdate(
              p => p.Name,
              new ClayDoor { Name = "Door1" },
              new ClayDoor { Name = "Door2" },
              new ClayDoor { Name = "Door3" },
              new ClayDoor { Name = "Door4" },
              new ClayDoor { Name = "Door5" }
            );

            context.ClayUsers.AddOrUpdate(
             p => new { p.Name, p.LoginName, p.Token },
             new ClayUser { Name = "testUser", LoginName = "testUser", Token = "testUser-token" },
             new ClayUser { Name = "testUser1", LoginName = "testUser1", Token = "testUser1-token" },
             new ClayUser { Name = "testUser2", LoginName = "testUser2", Token = "testUser2-token" }
            );

            context.DoorUserIntegrations.AddOrUpdate(
             p => new { p.Desc, p.ClayUserId, p.ClayDoorId },
             new DoorUserIntegration { Desc = "John Bedroom", ClayUserId = 1, ClayDoorId = 1 },
             new DoorUserIntegration { Desc = "John Bathroom", ClayUserId = 1, ClayDoorId = 2 },
             new DoorUserIntegration { Desc = "John Kitchen", ClayUserId = 1, ClayDoorId = 2 },
             new DoorUserIntegration { Desc = "Babet main home door :)", ClayUserId = 2, ClayDoorId = 3 }
            );

        }
    }
}
