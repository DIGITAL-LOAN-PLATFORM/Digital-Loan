using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Infrastructure.Identity;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // --- DbSets ---
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Borrower> Borrowers { get; set; }
        public DbSet<Guarantor> Guarantors { get; set; }
        public DbSet<GuarantorType> GuarantorTypes { get; set; }
        public DbSet<LoanApplication> LoanApplications { get; set; }
        public DbSet<LoanDisbursement> LoanDisbursements { get; set; }
        public DbSet<LoanProduct> LoanProducts { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentModality> PaymentModalities { get; set; }
        public DbSet<Penalty> Penalties { get; set; }
        public DbSet<ProcessFeeDeposit> ProcessFeeDeposits { get; set; }
        public DbSet<ProvidedDocument> ProvidedDocuments { get; set; }
        public DbSet<Reason> Reasons { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }

        // Note: DbSet<User> is inherited from IdentityDbContext. Do not redeclare it here.

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. CONFIGURE OWNED TYPES (Value Objects)
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

            // 2. RELATIONSHIPS & CONSTRAINTS
            
            // Unique Index for Borrower NIDA
            modelBuilder.Entity<Borrower>()
                .HasIndex(b => b.IdentificationNumber)
                .IsUnique();

            // Link Guarantor to LoanApplication
            modelBuilder.Entity<Guarantor>()
                .HasOne(g => g.LoanApplication)
                .WithMany(l => l.Guarantors)
                .HasForeignKey(g => g.LoanApplicationId)
                .OnDelete(DeleteBehavior.Restrict);

            // Link Guarantor to GuarantorType
            modelBuilder.Entity<Guarantor>()
                .HasOne(g => g.GuarantorType)
                .WithMany()
                .HasForeignKey(g => g.GuarantorTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // LoanApplication FKs
            modelBuilder.Entity<LoanApplication>()
                .HasOne(l => l.loanProduct)
                .WithMany()
                .HasForeignKey(l => l.loanProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LoanApplication>()
                .HasOne(l => l.paymentModality)
                .WithMany()
                .HasForeignKey(l => l.paymentModalityId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LoanApplication>()
                .HasOne(l => l.Borrower)
                .WithMany() // Change this to .WithMany(b => b.LoanApplications) if Borrower has that list
                .HasForeignKey(l => l.BorrowerId)
                .OnDelete(DeleteBehavior.Restrict);

            // 3. IDENTITY TABLE CUSTOMIZATION
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<IdentityRole<int>>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims");
            modelBuilder.Entity<IdentityUserToken<int>>().ToTable("UserTokens");
            modelBuilder.Entity<IdentityUserRole<int>>().ToTable("UserRoles");

            // 4. THE GLOBAL FIX: Disable Cascade Delete (Safely)
            // We loop through all relationships but skip "Owned" types (Value Objects)
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                if (!relationship.IsOwnership) 
                {
                    relationship.DeleteBehavior = DeleteBehavior.Restrict;
                }
            }
        }
    }
}