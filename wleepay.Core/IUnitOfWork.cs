using System;
using System.Threading.Tasks;
using wleepay.Core.Models;
using wleepay.Core.Repositories;

namespace wleepay.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IUserDetailsRepository UserDetail { get; }
        public IWleeSendUserDetailsRepository WleeSendUserDetail { get; }
        IKYCDocumentsRepository KYCDocuments { get; }
        IDocumentTypeRepository DocumentType { get; }
        ICountyRepository County { get; }
        IKYCStatusRepository KYCStatus { get; }
        ITransactionRepository Transaction { get; }
        IWalletRepository Wallet { get; }
        IWalletTypeRepository WalletType { get; }
        ICurrencyRepository Currency { get; }
        ISmileIdentityJobRepository SmileIdentityJob { get; }
        ITransferTypeRepository TransferType { get; }
        ITransferFeeRepository TransferFee { get; }
        ITransactionStatusRepository TransactionStatus { get; }
        IAddressRepository Address { get; }
        IKYCLevelRepository KYCLevel { get; }
        ISecurityQuestionsRepository SecurityQuestions { get; }
        ISystemActivityLogsRepository SystemActivity { get; }
        IIssueTypeRepository IssueType { get; }
        ICustomerTicketsRepository CustomerTickets { get; }
        INotesRepository Notes { get; }
        IStaffRolesRepository StaffRoles { get; }
        IStaffPermissionTypeRepository StaffPermissionType { get; }
        IUsersStaffRoleRepository UsersStaffRole { get; }
        IVerificationRepository Verification { get; }
        ICashFlowHistoryRepository CashFlowHistory { get; }
        ISystemStatusRepository SystemStatus { get; }
        IUserDeviceRepository UserDevice { get; }
        ISochitelTransactionRepository SochitelTransaction { get; }
        ICreditCardTransactionRepository CreditCardTransaction { get; }

        IPaymentTransactionRepository PaymentTransactions { get; }
        Task<int> CommitAsync();
        Task BeginTransactionAsync(); // Begins the transaction
        Task CommitTransactionAsync(); // Commits the transaction
        void RollbackTransaction(); // Rollback transaction in case of failure
    }
}
