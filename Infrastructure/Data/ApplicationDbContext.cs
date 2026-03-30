using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Infrastructure.Identity;
using Domain.Entities;
using Infrastructure.Data;
using System.Linq;
using Application.Interface;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DbSets remain unchanged...
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Borrower> Borrowers { get; set; }
        public DbSet<Guarantor> Guarantors { get; set; }
        public DbSet<GuarantorType> GuarantorTypes { get; set; }
        public DbSet<LoanApplication> LoanApplications { get; set; }
        public DbSet<LoanDisbursement> LoanDisbursements { get; set; }
        public DbSet<LoanProduct> LoanProducts { get; set; }
        public DbSet<LoanRequirements> LoanRequirements { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentModality> PaymentModalities { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<Penalty> Penalties { get; set; }
        public DbSet<ProcessFeeDeposit> ProcessFeeDeposits { get; set; }
        public DbSet<ProvidedDocument> ProvidedDocuments { get; set; }
        public DbSet<Reason> Reasons { get; set; }
        public DbSet<RequiredDocument> RequiredDocuments { get; set; }
         public DbSet<RepaymentScheduleItem> RepaymentScheduleItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // --- FIX 1: GLOBAL DECIMAL PRECISION ---
            // This loop finds every decimal property across all tables and sets them to 18,2
            // preventing the "No store type was specified" warnings and data truncation.
            var decimalProperties = builder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?));

            foreach (var property in decimalProperties)
            {
                property.SetColumnType("decimal(18,2)");
            }

            // --- 1. CONFIGURE OWNED TYPES ---
            builder.Entity<Borrower>().OwnsOne(b => b.HomeLocation, location =>
            {
                location.Property(l => l.Province).HasColumnName("Province");
                location.Property(l => l.District).HasColumnName("District");
                location.Property(l => l.Sector).HasColumnName("Sector");
                location.Property(l => l.Cell).HasColumnName("Cell");
                location.Property(l => l.Village).HasColumnName("Village");
            });

            builder.Entity<Guarantor>().OwnsOne(g => g.ResidentialAddress, location =>
            {
                location.Property(l => l.Province).HasColumnName("Res_Province");
                location.Property(l => l.District).HasColumnName("Res_District");
                location.Property(l => l.Sector).HasColumnName("Res_Sector");
                location.Property(l => l.Cell).HasColumnName("Res_Cell");
                location.Property(l => l.Village).HasColumnName("Res_Village");
            });

            // --- 2. RELATIONSHIPS & CONSTRAINTS ---
            
            // Unique Index for Borrower NIDA
            builder.Entity<Borrower>()
                .HasIndex(b => b.IdentificationNumber)
                .IsUnique();

            // Link Guarantor to LoanApplication
            builder.Entity<Guarantor>()
                .HasOne(g => g.LoanApplication)
                .WithMany(l => l.Guarantors)
                .HasForeignKey(g => g.LoanApplicationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Guarantor>()
                .HasOne(g => g.GuarantorType)
                .WithMany()
                .HasForeignKey(g => g.GuarantorTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // LoanApplication FKs
            builder.Entity<LoanApplication>()
                .HasOne(l => l.loanProduct)
                .WithMany()
                .HasForeignKey(l => l.loanProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<LoanApplication>()
                .HasOne(l => l.paymentModality)
                .WithMany()
                .HasForeignKey(l => l.paymentModalityId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<LoanApplication>()
                .HasOne(l => l.Borrower)
                .WithMany()
                .HasForeignKey(l => l.BorrowerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Payments & Penalties
            builder.Entity<Payment>()
                .HasOne(p => p.PaymentType)
                .WithMany()
                .HasForeignKey(p => p.PaymentTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Payment>()
                .HasOne(p => p.Account)
                .WithMany()
                .HasForeignKey(p => p.AccountId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Payment>()
                .HasOne(p => p.LoanDisbursement)
                .WithMany()
                .HasForeignKey(p => p.LoanDisbursementId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Penalty>()
                .HasOne(p => p.LoanDisbursement)
                .WithMany()
                .HasForeignKey(p => p.LoanDisbursementId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Penalty>()
                .HasOne(p => p.PenaltyReason)
                .WithMany()
                .HasForeignKey(p => p.ReasonId)
                .OnDelete(DeleteBehavior.Restrict);

            // LoanDisbursement FK to PaymentModality
            builder.Entity<LoanDisbursement>()
                .HasOne(ld => ld.PaymentModality)
                .WithMany()
                .HasForeignKey(ld => ld.PaymentModalityId)
                .OnDelete(DeleteBehavior.Restrict);

            // ProvidedDocument relationships
            builder.Entity<ProvidedDocument>()
                .HasOne(pd => pd.LoanApplication)
                .WithMany()
                .HasForeignKey(pd => pd.LoanApplicationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ProvidedDocument>()
                .HasOne(pd => pd.LoanDisbursement)
                .WithMany()
                .HasForeignKey(pd => pd.LoanDisbursementId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ProvidedDocument>()
                .HasOne(pd => pd.RequiredDocument)
                .WithMany()
                .HasForeignKey(pd => pd.RequiredDocumentId)
                .OnDelete(DeleteBehavior.Restrict);

            // LoanRequirements relationships
            builder.Entity<LoanRequirements>()
                .HasOne(lr => lr.LoanProduct)
                .WithMany()
                .HasForeignKey(lr => lr.LoanProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<LoanRequirements>()
                .HasOne(lr => lr.RequiredDocument)
                .WithMany()
                .HasForeignKey(lr => lr.RequiredDocumentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.Entity<PaymentType>().HasIndex(pt => pt.Name).IsUnique();
            builder.Entity<Reason>().HasIndex(r => r.Name).IsUnique();

            // --- FIX 2: GLOBAL QUERY FILTERS ---
            // If Borrower is filtered by IsActive, LoanApplication must also be 
            // aware or it might try to load a "deactivated" borrower.
            builder.Entity<Borrower>().HasQueryFilter(b => b.IsActive);
            
            // To fix the CS10622 warning, ensure children are filtered similarly if applicable,
            // or ensure the navigation is handled carefully.
            // Example: builder.Entity<LoanApplication>().HasQueryFilter(l => l.Borrower.IsActive);

            // Disable Cascade Delete globally for non-owned relationships
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                if (!relationship.IsOwnership) 
                {
                    relationship.DeleteBehavior = DeleteBehavior.Restrict;
                }
            }

            // Conversions
            builder.Entity<RequiredDocument>()
                .Property(c => c.DocumentType)
                .HasConversion<string>();

              
        }
    }
}