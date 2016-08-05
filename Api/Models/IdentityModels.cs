using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using Api.DAL;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using Api.DAL.DTO;

namespace Api.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser
    // class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        static ApplicationDbContext()
        {
            Database.SetInitializer(new MySqlInitializer());
        }

        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }


        public DbSet<ClayUser> ClayUsers { get; set; }
        public DbSet<DoorUserIntegration> DoorUserIntegrations { get; set; }
        public DbSet<ClayDoor> ClayDoor { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DoorUserIntegration>()
                     .HasRequired<ClayUser>(m => m.ClayUser)
                     .WithMany(m => m.DoorUserIntegration)
                     .HasForeignKey(m => m.ClayUserId);

            modelBuilder.Entity<DoorUserIntegration>()
                     .HasRequired<ClayDoor>(m => m.ClayDoor)
                     .WithMany(m => m.DoorUserIntegration)
                     .HasForeignKey(m => m.ClayDoorId);

        }

    }

}