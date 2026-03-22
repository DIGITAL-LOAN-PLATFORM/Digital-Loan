using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext  
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<RequiredDocument> RequiredDocuments { get; set; }
        // public DbSet<Guest> Guests { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // builder.Entity<Guest>()
            //     .Property(t => t.GuestStatus)
            //     .HasConversion<string>();

            builder.Entity<RequiredDocument>()
                .Property(c => c.DocumentType)
                .HasConversion<string>();
        }
    }
}