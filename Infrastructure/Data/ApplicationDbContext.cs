using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
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