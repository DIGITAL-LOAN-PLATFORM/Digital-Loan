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

            modelBuilder.Entity<Borrower>().OwnsOne(b => b.HomeLocation, location =>
            {
                location.Property(l => l.Province).HasColumnName("Province");
                location.Property(l => l.District).HasColumnName("District");
                location.Property(l => l.Sector).HasColumnName("Sector");
                location.Property(l => l.Cell).HasColumnName("Cell");
                location.Property(l => l.Village).HasColumnName("Village");
            });

            modelBuilder.Entity<Guarantor>().OwnsOne(g => g.ResidentialAddress, location =>
            {
                location.Property(l => l.Province).HasColumnName("Res_Province");
                location.Property(l => l.District).HasColumnName("Res_District");
                location.Property(l => l.Sector).HasColumnName("Res_Sector");
                location.Property(l => l.Cell).HasColumnName("Res_Cell");
                location.Property(l => l.Village).HasColumnName("Res_Village");
            });

            modelBuilder.Entity<Guarantor>()
                .HasOne(g => g.LoanApplication)
                .WithMany(l => l.Guarantors)
                .HasForeignKey(g => g.LoanApplicationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Guarantor>()
                .HasOne(g => g.GuarantorType)
                .WithMany()
                .HasForeignKey(g => g.GuarantorTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.PaymentType)
                .WithMany()
                .HasForeignKey(p => p.PaymentTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Account)
                .WithMany()
                .HasForeignKey(p => p.AccountId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.LoanDisbursement)
                .WithMany()
                .HasForeignKey(p => p.LoanDisbursementId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Penalty>()
                .HasOne(p => p.LoanDisbursement)
                .WithMany()
                .HasForeignKey(p => p.LoanDisbursementId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Penalty>()
                .HasOne(p => p.PenaltyReason)
                .WithMany()
                .HasForeignKey(p => p.ReasonId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PaymentType>()
                .HasIndex(pt => pt.Name)
                .IsUnique();

            modelBuilder.Entity<Reason>()
                .HasIndex(r => r.Name)
                .IsUnique();

            modelBuilder.Entity<Borrower>()
                .HasIndex(b => b.IdentificationNumber)
                .IsUnique();

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<Borrower>().HasQueryFilter(b => b.IsActive);
        }
    }
}