using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
        
       
                

            

           
        }
        
        
        
    }
