using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using wleepay.Core.Models;
using wleepay.Data.Configurations;

namespace wleepay.Data.Context
{
    public class WPContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public DbSet<UserDetail> UserDetail { get; set; }
        public DbSet<WleeSendUserDetails> WleeSendUserDetail { get; set; }
        public DbSet<WleeSendUser> WleeSendUsers { get; set; }
        public DbSet<PaymentTransaction> UserTransactions { get; set; }

        public DbSet<KYCdocuments> KYCDocuments { get; set; } 
        public DbSet<KYCStatus> KYCStatus { get; set; }
        public DbSet<DocumentType> DocumentType { get; set; }
        public DbSet<County> County { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Wallet> Wallet { get; set; }
        public DbSet<WalletType> WalletTypes { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionStatus> TransactionStatuses { get; set; }
        public DbSet<SmileIdentityJob> SmileIdentityJobs { get; set; }
        public DbSet<TransferType> TransferTypes { get; set; }
        public DbSet<TransferFee> TransferFees { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<SecurityQuestions> SecurityQuestions { get; set; }
      
        // public DbSet<TokenStorage> TokenStorage { get; set; }
        public DbSet<SystemActivityLog> SystemActivities { get; set; }
        public DbSet<IssueType> IssueType { get; set; }
        public DbSet<CustomerTickets> CustomerTickets { get; set; }
        public DbSet<Notes> Notes { get; set; }
        public DbSet<UserNotes> UserNotes { get; set; }
        public DbSet<StaffRoles> StaffRoles { get; set; }
        public DbSet<StaffPermissionType> StaffPermissionType { get; set; }
        public DbSet<UsersStaffRole> UsersStaffRole { get; set; }
        public DbSet<Verification> Verification { get; set; }
        public DbSet<CashFlowHistory> CashFlowHistory { get; set; }
        public DbSet<SystemStatus> SystemStatus { get; set; }
        public DbSet<UserDevices> UserDevices { get; set; }
        public DbSet<SochitelTransactions> SochitelTransactions { get; set; }
        public DbSet<CreditCardTransactions> CreditCardTransactions { get; set; }

        public WPContext(DbContextOptions<WPContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new ApplicationUserConfiguration());
            builder.ApplyConfiguration(new ApplicationRoleConfiguration());
            builder.ApplyConfiguration(new ApplicationUserRoleConfiguration());
            builder.ApplyConfiguration(new DocumentTypeConfiguration());
            builder.ApplyConfiguration(new KYCDocumentConfiguration());
            builder.ApplyConfiguration(new KYCStatusConfiguration());
            builder.ApplyConfiguration(new UserDetailConfiguration());
            builder.ApplyConfiguration(new AddressConfiguration());
            builder.ApplyConfiguration(new CountyConfiguration());
            builder.ApplyConfiguration(new TransactionConfiguration());
            builder.ApplyConfiguration(new TransactionStatusConfiguration());
            builder.ApplyConfiguration(new WalletConfiguration());
            builder.ApplyConfiguration(new WalletTypeConfiguration());
            builder.ApplyConfiguration(new CurrencyConfiguration());
            builder.ApplyConfiguration(new TransferFeeConfiguration());
            builder.ApplyConfiguration(new TransferTypeConfiguration());
            builder.ApplyConfiguration(new KYCLevelConfiguration());
            builder.ApplyConfiguration(new SecurityQuestionsConfiguration());
            //builder.ApplyConfiguration(new TokenStorageConfiguration());
            builder.ApplyConfiguration(new SystemActivitiesConfiguration());
            builder.ApplyConfiguration(new IssueTypeConfiguration());
            builder.ApplyConfiguration(new CustomerTicketsConfiguration());
            builder.ApplyConfiguration(new NotesConfiguration());
            builder.ApplyConfiguration(new StaffRolesConfiguration());
            builder.ApplyConfiguration(new StaffPermissionTypeConfiguration());
            builder.ApplyConfiguration(new UserStaffRoleConfiguration());
            builder.ApplyConfiguration(new VerificationConfiguration());
            builder.ApplyConfiguration(new CashFlowHistoryConfiguration());
            builder.ApplyConfiguration(new SystemStatusConfiguration());
            builder.ApplyConfiguration(new UserDevicesConfiguration());
            builder.ApplyConfiguration(new SochitelTransactionConfiguration());
            builder.ApplyConfiguration(new CreditCardTransactionConfiguration());
            builder.ApplyConfiguration(new UserNotesConfiguration());
            builder.ApplyConfiguration(new PaymentTransactionConfiguration());



        }
    }
}
