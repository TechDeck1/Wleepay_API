using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;
using wleepay.Core;
using wleepay.Core.Repositories;
using wleepay.Data.Context;
using wleepay.Data.Repositories;
using AutoMapper;

namespace wleepay.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WPContext _context;
        private readonly IMapper _mapper;
        private UserDetailRepository _userDetailsRepository;
        private WleeSendUserDetailRepository _sendUserDetailRepository;
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
        private ISystemActivityLogsRepository _systemActivityLogsRepository;
        private IIssueTypeRepository _issueTypeRepository;
        private ICustomerTicketsRepository _customerTicketsRepository;
        private INotesRepository _notesRepository;
        private IStaffRolesRepository _staffRolesRepository;
        private IStaffPermissionTypeRepository _staffPermissionsTypeRepository;
        private IUsersStaffRoleRepository _usersStaffRoleRepository;
        private IVerificationRepository _verificationRepository;
        private ICashFlowHistoryRepository _cashFlowHistoryRepository;
        private ISystemStatusRepository _systemStatusRepository;
        private IUserDeviceRepository _userDeviceRepository;
        private ISochitelTransactionRepository _sochitelTransactionRepository;
        private ICreditCardTransactionRepository _creditCardTransactionRepository;
        private IDbContextTransaction _currentTransaction;
        private IPaymentTransactionRepository _paymentTransactionRepository;



        public UnitOfWork(WPContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IUserDetailsRepository UserDetail => _userDetailsRepository = _userDetailsRepository ?? new UserDetailRepository(_context);
        public IWleeSendUserDetailsRepository WleeSendUserDetail => _sendUserDetailRepository = _sendUserDetailRepository ?? new WleeSendUserDetailRepository(_context);
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
        public ISystemActivityLogsRepository SystemActivity => _systemActivityLogsRepository= _systemActivityLogsRepository ?? new SystemActivityLogsRepository(_context);
        public IIssueTypeRepository IssueType => _issueTypeRepository = _issueTypeRepository ?? new IssueTypeRepository(_context);
        public ICustomerTicketsRepository CustomerTickets => _customerTicketsRepository = _customerTicketsRepository ?? new CustomerTicketsRepository(_context);
        public INotesRepository Notes => _notesRepository = _notesRepository ?? new NotesRepository(_context);
        public IStaffRolesRepository StaffRoles => _staffRolesRepository = _staffRolesRepository ?? new StaffRoleRepository(_context);
        public IStaffPermissionTypeRepository StaffPermissionType => _staffPermissionsTypeRepository = _staffPermissionsTypeRepository ?? new StaffPermissionTypeRepository(_context);
        public IUsersStaffRoleRepository UsersStaffRole => _usersStaffRoleRepository = _usersStaffRoleRepository ?? new UsersStaffRoleRepository(_context);
        public IVerificationRepository Verification => _verificationRepository = _verificationRepository ?? new VerificationRepository(_context);
        public ICashFlowHistoryRepository CashFlowHistory => _cashFlowHistoryRepository= _cashFlowHistoryRepository ?? new CashFlowHistoryRepository(_context);
        public ISystemStatusRepository SystemStatus => _systemStatusRepository= _systemStatusRepository ?? new SystemStatusRepository(_context);    
        public IUserDeviceRepository UserDevice => _userDeviceRepository = _userDeviceRepository ?? new UserDeviceRepository(_context);
        public ISochitelTransactionRepository SochitelTransaction => _sochitelTransactionRepository = _sochitelTransactionRepository ?? new SochitelTransactionRepository(_context);

        public ICreditCardTransactionRepository CreditCardTransaction => _creditCardTransactionRepository = _creditCardTransactionRepository ?? new CreditCardTransactionRepository(_context);

        public IPaymentTransactionRepository PaymentTransactions => _paymentTransactionRepository ??= new PaymentTransactionRepository(_context,_mapper);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public async Task BeginTransactionAsync()
        {
            _currentTransaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.CommitAsync();
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }

        public void RollbackTransaction()
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Rollback();
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
