using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Borrower> Borrowers { get; set; }
    public DbSet<LoanProduct> LoanProducts { get; set; }
    public DbSet<ProvidedDocument> ProvidedDocuments { get; set; }
    public DbSet<RequiredDocument> RequiredDocuments { get; set; }
    public DbSet<GuarantorType> GuarantorTypes { get; set; }
    public DbSet<PaymentModality> PaymentModalities { get; set; }
    public DbSet<Guarantor> Guarantors { get; set; }
    public DbSet<LoanApplication> LoanApplications { get; set; }
    public DbSet<DocumentType> DocumentTypes { get; set; }   // ← add this

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<LoanProduct>()
            .Property(x => x.InterestRate)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Guarantor>()
            .HasOne(g => g.LoanApplication)
            .WithMany()
            .HasForeignKey(g => g.LoanApplicationId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Guarantor>()
            .HasOne(g => g.GuarantorType)
            .WithMany()
            .HasForeignKey(g => g.GuarantorTypeId)
            .OnDelete(DeleteBehavior.NoAction);

        // RequiredDocument has no FK navigation now — no cascade config needed
    }
}