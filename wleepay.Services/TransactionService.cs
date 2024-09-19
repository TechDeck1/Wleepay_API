using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core;
using wleepay.Core.Models;
using wleepay.Core.Services;
using System.Linq;
using wleepay.Core.Enums;
using TransactionStatus = wleepay.Core.Enums.TransactionStatus;
using wleepay.Core.DTO.Response;
using Microsoft.SqlServer.Management.Smo;
using static DevExpress.XtraPrinting.Native.PageSizeInfo;
using wleepay.Data.Context;

namespace wleepay.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Transaction>> GetPaymentRequestByStatus(IEnumerable<long> walletIds, PaymentRequestType type, TransactionStatus transactionStatus, int pageSize, int pageIndex)
        {
            return await _unitOfWork.Transaction.GetPaymentRequestByStatus(walletIds, type, transactionStatus, pageSize, pageIndex);
        }

        public async Task<TransactionResponseResource> GetIncomingOutgoingPaymentRequest(IEnumerable<long> walletIds, PaymentRequestType type, TransactionStatus transactionStatus,int transferTypeId, int pageSize, int pageIndex)
        {
            return await _unitOfWork.Transaction.GetIncomingOutgoingPaymentRequest(walletIds, type, transactionStatus, transferTypeId, pageSize, pageIndex);
        }

        public async Task<TransactionResponseResource> GetPaymentRequestByStatusAndType(PaymentRequestType type, TransactionStatus transactionStatus,int transferId, int pageSize, int pageIndex)
        {
            return await _unitOfWork.Transaction.GetPaymentRequestByStatusAndType(type, transactionStatus, transferId, pageSize, pageIndex);
        }

        public async Task<Transaction> GetTransactionsById(long Id)
        {
            return await _unitOfWork.Transaction.GetTransactionDetails(Id);
        }

        public async Task<TransactionResponseResource> GetTransactionsByWalletId(long walletId, int pageIndex = 0, int pageSize = 10)
        {
            return await _unitOfWork.Transaction.GetTransactionsByWalletId(walletId, pageIndex, pageSize);
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByType(TransferTypes type, int pageIndex = 0, int pageSize = 10)
        {
            return await _unitOfWork.Transaction.GetTransactionsByType(type, pageIndex, pageSize);
        }

        public async Task<IEnumerable<Transaction>> GetWalletsTransactions(IEnumerable<long> walletIdList)
        {
            return await _unitOfWork.Transaction.GetWalletsTransactions(walletIdList);
        }

        public async Task<Transaction> InitiateTransaction(Transaction transactionDetail)
        {
            var approveTransaction = await _unitOfWork.Transaction.AddUpdateTransactions(transactionDetail);
            if (transactionDetail.TransferTypeId == TransferTypes.TopUp.GetHashCode())
            {
                var getCashOnHandTransaction = AddCashOnHandCreditTransaction((long)transactionDetail.FromWalletId, transactionDetail.Amount, approveTransaction.Id);
                await _unitOfWork.Transaction.AddUpdateTransactionsForInHand(getCashOnHandTransaction);

            }
            if (transactionDetail.TransferTypeId == TransferTypes.FillAgent.GetHashCode())
            {
                var getCashOnHandTransaction = AddCashOnHandCreditTransaction((long)transactionDetail.FromWalletId, transactionDetail.Amount, approveTransaction.Id);
                await _unitOfWork.Transaction.AddUpdateTransactionsForInHand(getCashOnHandTransaction);

            }
            //await _unitOfWork.CommitAsync();
            return approveTransaction;
        }

        public async Task<Transaction> InitiateCashOnHandTransaction(Transaction transactionDetail)
        {
            var approveRequest = await InitiateTransaction(transactionDetail);
            if (transactionDetail.TransferTypeId == TransferTypes.FillAgent.GetHashCode())
            {
                var getCashOnHandTransaction = AddCashOnHandCreditTransaction((long)transactionDetail.FromWalletId, transactionDetail.Amount, approveRequest.Id);
                await InitiateTransaction(getCashOnHandTransaction);

            }
            return approveRequest;
        }
        //Intitate Cashout
        public async Task<Transaction> InitiateCashout(Transaction transactionDetail)
        {
            try
            {
                //transfer amount to agent account
                var agentTranfer = await InitiateTransaction(transactionDetail);

                //get fee details
                var transferType = await _unitOfWork.TransferType.GetTransferTypeById(transactionDetail.TransferTypeId);
                if (transferType.TransferFeeDetail != null)
                {
                    int currencyId = 0;
                    //generate agent commission
                    double agentCommissionAmount = agentTranfer.Amount * (transferType.TransferFeeDetail.AgentCommission / 100);
                    var commissionTransaction = GetAgentCommissionTransaction((long)agentTranfer.FromWalletId, (long)agentTranfer.ToWalletId, Math.Round(agentCommissionAmount, 2), agentTranfer.Id, out currencyId);
                    //transafer commission amount to agent account
                    await InitiateTransaction(commissionTransaction);

                    //generate tax transaction
                    double taxAmount = agentTranfer.Amount * (transferType.TransferFeeDetail.TaxAmount / 100);
                    var taxTransaction = GetTaxTransaction((long)agentTranfer.FromWalletId, Math.Round(taxAmount, 2), agentTranfer.Id, currencyId);
                    //transafer tax amount to organization tax account
                    await InitiateTransaction(taxTransaction);

                    double organizationFeeAmount = agentTranfer.Amount * (transferType.TransferFeeDetail.OrganizationFee / 100);
                    var organizationFeeTransaction = GetOrganizationFeeTransaction((long)agentTranfer.FromWalletId, Math.Round(organizationFeeAmount, 2), agentTranfer.Id, currencyId);
                    //transafer organization amount to organization fee account
                    await InitiateTransaction(organizationFeeTransaction);
                }
                return agentTranfer;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Transaction> InitiateTransactionRequest(Transaction transactionDetail)
        {
            var createRequest = await _unitOfWork.Transaction.CreatePaymentRequest(transactionDetail);
            return createRequest;
        }

        public async Task<Transaction> UpdateTransactionStatus(long id, Core.Enums.TransactionStatus status)
        {
            return await _unitOfWork.Transaction.UpdateTransactionStatus(id, status);
        }

        public async Task<double> CalculateTotalTransactionAmount(long walletId, bool isMonthly)
        {
            return await _unitOfWork.Transaction.CalculateWalletTransaction(walletId, isMonthly);
        }

        public async Task<TransactionResponseResource> GetCashoutTransactions(int pageIndex , int pageSize)
        {
            return await _unitOfWork.Transaction.GetCashoutTransactions(pageIndex, pageSize);
        }

        public async Task<TransactionResponseResource> GetTopUpTransactions(int pageIndex, int pageSize)
        {
            return await _unitOfWork.Transaction.GetTopUpTransactions(pageIndex, pageSize);
        }

        public async Task<TransactionResponseResource> GetTransactionsByTypeAndCurrency(int type, int currency, int pageIndex = 0, int pageSize = 10)
        {
            return await _unitOfWork.Transaction.GetTransactionsByTypeAndCurrency(type, currency, pageIndex, pageSize);
        }

        public async Task<TransactionResponseResource> GetTransactionsByUserIdAndType(string userId,int transferType, int pageIndex = 0, int pageSize = 10)
        {
            return await _unitOfWork.Transaction.GetTransactionsByUserIdAndType(userId,transferType, pageIndex, pageSize);
        }
        
        public async Task<TransactionResponseResource> GetTransactionsHistoryByUserIdAndType(string userId, int status, int transferType, int pageIndex = 0, int pageSize = 10)
        {
            return await _unitOfWork.Transaction.GetTransactionsHistoryByUserIdAndType(userId,status, transferType, pageIndex, pageSize);
        }

        public async Task<TransactionResponseResource> GetTransactionHistoryByUserId(string userId, int pageIndex, int pageSize)
        {
            return await _unitOfWork.Transaction.GetTransactionsHistoryByUserId(userId, pageIndex, pageSize);
        }

        public async Task<IList<TransactionAmountBasedOnMonthsResponseModel>> GetTransactionAmountByMonthLD()
        {
            return await _unitOfWork.Transaction.GetTransactionAmountByMonthLD();

        }

        public async Task<IList<TransactionAmountBasedOnMonthsResponseModel>> GetTransactionAmountByMonthUSD()
        {
            return await _unitOfWork.Transaction.GetTransactionAmountByMonthUSD();

        }

        public async Task<IList<TransactionCountByTypeResponseModel>> GetTransactionCounttByTypeLD()
        {
            return await _unitOfWork.Transaction.GetTransactionCounttByTypeLD();

        }
        public async Task<IList<TransactionCountByTypeResponseModel>> GetTransactionCounttByTypeUSD()
        {
            return await _unitOfWork.Transaction.GetTransactionCounttByTypeUSD();

        } 
        
        public async Task<IList<TransactionSummaryResponseModel>> GetTransactionData(int monthId, int currencyId)
        {
            return await _unitOfWork.Transaction.GetTransactionData(monthId, currencyId);

        }
        #region Helper function
        private Transaction GetTaxTransaction(long fromWalletId, double amount, long parentTransactionId, int currency)
        {
            Transaction taxTransaction = new Transaction
            {
                Amount = amount,
                CreatedDate = DateTime.Now,
                FromWalletId = fromWalletId,
                ParentId = parentTransactionId,
                Status = TransactionStatus.Pending.GetHashCode(),
                TransferTypeId = TransferTypes.WleeTax.GetHashCode(),
                ToWalletId = currency == Currencies.LRD.GetHashCode() ? OrganizationWallet.WleeTax_LRD.GetHashCode() : OrganizationWallet.WleeTax_USD.GetHashCode()
            };
            return taxTransaction;
        }
        private Transaction GetOrganizationFeeTransaction(long fromWalletId, double amount, long parentTransactionId, int currency)
        {
            Transaction orgFeeTransaction = new Transaction
            {
                Amount = amount,
                CreatedDate = DateTime.Now,
                FromWalletId = fromWalletId,
                ParentId = parentTransactionId,
                Status = TransactionStatus.Pending.GetHashCode(),
                TransferTypeId = TransferTypes.CashOutStandardFeeSplit.GetHashCode(),
                ToWalletId = currency == Currencies.LRD.GetHashCode() ? OrganizationWallet.OrgP_LRD.GetHashCode() : OrganizationWallet.OrgP_USD.GetHashCode()
            };
            return orgFeeTransaction;
        }
        private Transaction GetAgentCommissionTransaction(long fromWalletId, long agentWalletId, double amount, long parentTransactionId, out int currency)
        {
            var wallet = _unitOfWork.Wallet.GetUserByWalletIdAndTransferType(agentWalletId, WalletTypes.AgentAccount.GetHashCode()).Result;
            currency = wallet.CurrencyId;
            var agentCommissionWallet = _unitOfWork.Wallet.GetWalletsByUserId(wallet.UserId, wallet.CurrencyId, WalletTypes.AgentComission.GetHashCode()).Result;
            Transaction orgFeeTransaction = new Transaction
            {
                Amount = amount,
                CreatedDate = DateTime.Now,
                FromWalletId = fromWalletId,
                ParentId = parentTransactionId,
                Status = TransactionStatus.Pending.GetHashCode(),
                TransferTypeId = TransferTypes.GeneratedAgentCommission.GetHashCode(),
                ToWalletId = agentCommissionWallet.FirstOrDefault().Id
            };
            return orgFeeTransaction;
        }

        private Transaction AddCashOnHandCreditTransaction(long fromWalletId, double amount, long parentTransactionId)
        {
            var wallet = _unitOfWork.Wallet.GetUserByWalletIdAndTransferType(fromWalletId, WalletTypes.SystemWallet.GetHashCode()).Result;
            var cashOnHandWallet = _unitOfWork.Wallet.GetWalletsByUserId(wallet.UserId, wallet.CurrencyId, WalletTypes.DebitFloatN.GetHashCode()).Result;

            Transaction addNewTransaction = new Transaction();
            addNewTransaction.FromWalletId = null;
            addNewTransaction.ToWalletId = cashOnHandWallet.FirstOrDefault().Id;
            addNewTransaction.Status = TransactionStatus.Approved.GetHashCode();
            addNewTransaction.TransferTypeId = TransferTypes.InHandCredit.GetHashCode();
            addNewTransaction.CreatedDate = DateTime.Now;
            addNewTransaction.ParentId = parentTransactionId;
            addNewTransaction.Description = "Credited to cash on hand wallet";
            addNewTransaction.Amount = amount;
            
            return addNewTransaction;

            //update Cash on hand wallet
            //var cohWallet = await WPContext.Wallet.Where(w => w.Id == 1).FirstOrDefaultAsync();
            //cohWallet.Balance = transactionDetail.Amount;
            //await WPContext.SaveChangesAsync();
        }

        #endregion
    }
}
