using System;
using System.Threading.Tasks;
using wleepay.Core.Repositories;

namespace wleepay.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IUserDetailsRepository UserDetail { get; }
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
        ITokenStorageRepository TokenStorage { get; }
        ISystemActivityLogsRepository SystemActivity { get; }
        IIssueTypeRepository IssueType { get; }
        ICustomerTicketsRepository CustomerTickets { get; }



        Task<int> CommitAsync();
    }
}
