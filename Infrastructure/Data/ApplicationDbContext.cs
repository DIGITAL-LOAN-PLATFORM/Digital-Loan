using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Borrower> Borrowers { get; set; }
        public DbSet<Guarantor> Guarantors { get; set; }
        public DbSet<GuarantorType> GuarantorTypes { get; set; }
        public DbSet<LoanApplication> LoanApplications { get; set; }
        public DbSet<LoanDisbursement> LoanDisbursements { get; set; }
        public DbSet<LoanProduct> LoanProducts { get; set; }
        public DbSet<LoanRecquirements> LoanRecquirementses { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentModality> PaymentModalities { get; set; }
        public DbSet<Penalty> Penalties { get; set; }
        public DbSet<ProcessFeeDeposit> ProcessFeeDeposits { get; set; }
        public DbSet<ProvidedDocument> ProvidedDocuments { get; set; }
        public DbSet<Reason> Reasons { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<RequirementDocument> RequiredDocuments { get; set; }

       protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    // 1. THE GLOBAL FIX: Disable Cascade Delete
    // This loops through every foreign key and sets it to 'Restrict'.
    // This is mandatory for your "System of Record" requirements.
    foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
    {
        relationship.DeleteBehavior = DeleteBehavior.Restrict;
    }

    // 2. CONFIGURE OWNED TYPES (Location)
    // Ensures Borrower and Guarantor addresses are stored correctly.
    modelBuilder.Entity<Borrower>().OwnsOne(b => b.HomeLocation, location =>
    {
        location.Property(l => l.Province).HasColumnName("Province");
        location.Property(l => l.District).HasColumnName("District");
        location.Property(l => l.Sector).HasColumnName("Sector");
        location.Property(l => l.Cell).HasColumnName("Cell");
        location.Property(l => l.Village).HasColumnName("Village");
    });

    modelBuilder.Entity<Guarantor>().OwnsOne(g => g.ResidentialAddress);

    // 3. GLOBAL IDENTITY LAYER
    // Ensures NIDA is unique.
    modelBuilder.Entity<Borrower>()
        .HasIndex(b => b.IdentificationNumber)
        .IsUnique();

    // 4. SOFT-DELETE FILTER
    // Hides "deleted" records from view while keeping them in the DB.
    modelBuilder.Entity<Borrower>().HasQueryFilter(b => b.IsActive);
}
    }
}