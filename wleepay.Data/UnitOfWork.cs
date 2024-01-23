using System;
using System.Threading.Tasks;
using wleepay.Core;
using wleepay.Core.Repositories;
using wleepay.Data.Context;
using wleepay.Data.Repositories;

namespace wleepay.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WPContext _context;
        private UserDetailRepository _userDetailsRepository;
        private KYCDocumentsRepository _kycDocumentsRepository;
        private CountyRepository _countyRepository;
        private DocumentTypeRepository _documentTypeRepository;
        private KYCStatusRepository _kycStatusRepository;
        private ITransactionRepository _transactionRepository;
        private ICurrencyRepository _currencyRepository;
        private IWalletTypeRepository _walletTypeRepository;
        private IWalletRepository _walletRepository;
        private ISmileIdentityJobRepository _smileIdentityJobRepository;
        private ITransferTypeRepository _transferTypeRepository;
        private ITransferFeeRepository _transferFeeRepository;
        private ITransactionStatusRepository _transactionStatusRepository;
        private IAddressRepository _addressRepository;
        private IKYCLevelRepository _kycLevelRepository;
        private ISecurityQuestionsRepository _securityQuestionsRepository;
        private ITokenStorageRepository _tokenStorageRepository;
        private ISystemActivityLogsRepository _systemActivityLogsRepository;
        private IIssueTypeRepository _issueTypeRepository;
        private ICustomerTicketsRepository _customerTicketsRepository;
        private INotesRepository _notesRepository;
        private IStaffRolesRepository _staffRolesRepository;
        private IStaffPermissionTypeRepository _staffPermissionsTypeRepository;
        private IUsersStaffRoleRepository _usersStaffRoleRepository;
        private IStaffPermissionsRepository _staffPermissionsRepository;



        public UnitOfWork(WPContext context) {
            this._context = context;
        }
        public IUserDetailsRepository UserDetail => _userDetailsRepository = _userDetailsRepository ?? new UserDetailRepository(_context);
        public IKYCDocumentsRepository KYCDocuments => _kycDocumentsRepository = _kycDocumentsRepository ?? new KYCDocumentsRepository(_context);
        public ICountyRepository County => _countyRepository = _countyRepository ?? new CountyRepository(_context);
        public IDocumentTypeRepository DocumentType => _documentTypeRepository = _documentTypeRepository ?? new DocumentTypeRepository(_context);
        public IKYCStatusRepository KYCStatus => _kycStatusRepository = _kycStatusRepository ?? new KYCStatusRepository(_context);
        public ITransactionRepository Transaction => _transactionRepository = _transactionRepository ?? new TransactionRepository(_context);
        public IWalletRepository Wallet => _walletRepository = _walletRepository ?? new WalletRepository(_context);
        public IWalletTypeRepository WalletType => _walletTypeRepository = _walletTypeRepository ?? new WalletTypeRepository(_context);
        public ICurrencyRepository Currency => _currencyRepository = _currencyRepository ?? new CurrencyRepository(_context);
        public ISmileIdentityJobRepository SmileIdentityJob => _smileIdentityJobRepository = _smileIdentityJobRepository ?? new SmileIdentityJobRepository(_context);
        public ITransferTypeRepository TransferType => _transferTypeRepository = _transferTypeRepository ?? new TransferTypeRepository(_context);
        public ITransferFeeRepository TransferFee => _transferFeeRepository = _transferFeeRepository ?? new TransferFeeRepository(_context);
        public ITransactionStatusRepository TransactionStatus => _transactionStatusRepository = _transactionStatusRepository ?? new TransactionStatusRepository(_context);
        public IAddressRepository Address => _addressRepository = _addressRepository ?? new AddressRespository(_context);
        public IKYCLevelRepository KYCLevel => _kycLevelRepository = _kycLevelRepository ?? new KYCLevelRepository(_context);
        public ISecurityQuestionsRepository SecurityQuestions => _securityQuestionsRepository = _securityQuestionsRepository ?? new SecurityQuestionsRepository(_context);
        public ITokenStorageRepository TokenStorage => _tokenStorageRepository=_tokenStorageRepository ?? new TokenStorageRepository(_context);
        public ISystemActivityLogsRepository SystemActivity => _systemActivityLogsRepository= _systemActivityLogsRepository ?? new SystemActivityLogsRepository(_context);
        public IIssueTypeRepository IssueType => _issueTypeRepository = _issueTypeRepository ?? new IssueTypeRepository(_context);
        public ICustomerTicketsRepository CustomerTickets => _customerTicketsRepository = _customerTicketsRepository ?? new CustomerTicketsRepository(_context);
        public INotesRepository Notes => _notesRepository = _notesRepository ?? new NotesRepository(_context);
        public IStaffRolesRepository StaffRoles => _staffRolesRepository = _staffRolesRepository ?? new StaffRoleRepository(_context);
        public IStaffPermissionTypeRepository StaffPermissionType => _staffPermissionsTypeRepository = _staffPermissionsTypeRepository ?? new StaffPermissionTypeRepository(_context);
        public IUsersStaffRoleRepository UsersStaffRole => _usersStaffRoleRepository = _usersStaffRoleRepository ?? new UsersStaffRoleRepository(_context);
        public IStaffPermissionsRepository StaffPermissions => _staffPermissionsRepository = _staffPermissionsRepository ?? new StaffPermissionRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
