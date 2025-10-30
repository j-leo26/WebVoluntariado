namespace Voluntariado.Data
{
    using Microsoft.EntityFrameworkCore;
    using Voluntariado.Models;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<VolunteerOffer> VolunteerOffers { get; set; }
        public DbSet<Application> Applications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Application>()
                .HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Application>()
                .HasOne(p => p.VolunteerOffer)
                .WithMany()
                .HasForeignKey(p => p.VolunteerOfferId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
