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
using Org.BouncyCastle.Crypto.Engines;
using Microsoft.Identity.Client;
using wleepay.Core.Common;
using System.Diagnostics;
using SendGrid.Helpers.Errors.Model;

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

        public async Task<TransactionResponseResource> GetIncomingOutgoingPaymentRequest(IEnumerable<long> walletIds, PaymentRequestType type, TransactionStatus transactionStatus, string transferTypeId, int pageSize, int pageIndex)
        {
            return await _unitOfWork.Transaction.GetIncomingOutgoingPaymentRequest(walletIds, type, transactionStatus, transferTypeId, pageSize, pageIndex);
        }

        public async Task<TransactionResponseResource> GetPaymentRequestByStatusAndType(PaymentRequestType type, TransactionStatus transactionStatus, string transferId, int pageSize, int pageIndex, string? searchTerm)
        {
            return await _unitOfWork.Transaction.GetPaymentRequestByStatusAndType(type, transactionStatus, transferId, pageSize, pageIndex, searchTerm);
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
            Transaction approveTransaction = new Transaction();
            if (transactionDetail.TransferTypeId == TransferTypes.FillAgent.GetHashCode() || transactionDetail.TransferTypeId == TransferTypes.MemberTopUpByWleePay.GetHashCode())
            {
                approveTransaction = await _unitOfWork.Transaction.AddUpdateAgentAndMemberTopUpTransaction(transactionDetail);
            }
            else if (transactionDetail.TransferTypeId == TransferTypes.AgentCashout.GetHashCode() || transactionDetail.TransferTypeId == TransferTypes.AgentCommissionCashout.GetHashCode())
            {
                approveTransaction = await _unitOfWork.Transaction.AddUpdateAgentCashOutTransactions(transactionDetail);
            }
            else
            {
                approveTransaction = await _unitOfWork.Transaction.AddUpdateTransaction(transactionDetail);
            }
            return approveTransaction;
        }

        public async Task<Transaction> InitiateCashOutWithWleePay(Transaction transactionDetail)
        {
            try
            {
                var transactions = new WalletTransactions();
                var transactionsList = new List<Transaction>();

                //get fee details
                var transferType = await _unitOfWork.TransferType.GetTransferTypeById(transactionDetail.TransferTypeId);
                var wallet = await _unitOfWork.Wallet.GetByIdAsync((long)transactionDetail.FromWalletId);
                
                if (transferType.TransferFeeDetail != null)
                {
                    int currencyId = 0;
                    double amountToTransfer = transactionDetail.Amount;

                    double taxAmount = amountToTransfer * (transferType.TransferFeeDetail.TaxAmount / 100);
                    double organizationFeeAmount = amountToTransfer * (transferType.TransferFeeDetail.OrganizationFee / 100);
                    var agentDirectTransferAmount = amountToTransfer + (taxAmount + organizationFeeAmount);

                    //transfer amount to agent account
                    transactionDetail.Amount = amountToTransfer;
                    transactions.AgentCashout = transactionDetail;
                    transactionsList.Add(transactionDetail);

                    //generate tax transaction
                    var taxTransaction = GetTaxTransaction((long)transactionDetail.FromWalletId, Math.Round(taxAmount, 2), transactionDetail.Id, currencyId);
                    transactions.taxAmount = taxTransaction;
                    transactionsList.Add(taxTransaction);

                    //generate and transfer organization amount
                    var organizationFeeTransaction = GetOrganizationFeeTransaction((long)transactionDetail.FromWalletId, Math.Round(organizationFeeAmount, 2), transactionDetail.Id, currencyId);
                    transactions.organizationFeeAmount = organizationFeeTransaction;
                    transactionsList.Add(organizationFeeTransaction);


                    //Debited from cashOnHand Wallet
                    var inHandWithdrawl = DebitedCashOnHand((long)transactionDetail.FromWalletId,(long)transactionDetail.ToWalletId, amountToTransfer, transactionDetail.Id, currencyId);
                    transactions.InHandWithdrawl = inHandWithdrawl;
                    transactionsList.Add(inHandWithdrawl);

                    //await BatchAddTransactionsAsync(transactions);
                    //await Cashout(transactions);

                    if (agentDirectTransferAmount <= wallet.Balance)
                    {
                        await BatchAddTransactionsAsync(transactionsList);
                    }
                    else
                    {
                        throw new Exception($"Wallet balance is not sufficient.");
                    }

                    return transactionDetail;
                }
                else
                {
                    throw new Exception("Error while getting tax rates");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Transaction> InitiateCashout(Transaction transactionDetail)
        {
            try
            {
                var transactions = new WalletTransactions();
                var transactionsList = new List<Transaction>();
                
                //get fee details
                var transferType = await _unitOfWork.TransferType.GetTransferTypeById(transactionDetail.TransferTypeId);
                var wallet = await _unitOfWork.Wallet.GetByIdAsync((long)transactionDetail.FromWalletId);
                var toUserDetail = await _unitOfWork.UserDetail.GetUserDetailsById(transactionDetail.ToWallet.UserId);

                if (transferType.TransferFeeDetail != null)
                {
                    int currencyId = 0;
                    double agentCommissionAmount = transactionDetail.Amount * (transferType.TransferFeeDetail.AgentCommission / 100);
                    double taxAmount = transactionDetail.Amount * (transferType.TransferFeeDetail.TaxAmount / 100);
                    double organizationFeeAmount = transactionDetail.Amount * (transferType.TransferFeeDetail.OrganizationFee / 100);
                    var agentDirectTransferAmount = transactionDetail.Amount + (agentCommissionAmount + taxAmount + organizationFeeAmount);

                    //transfer amount to agent account
                    transactionDetail.Amount = agentDirectTransferAmount;
                    transactions.AgentCashout = transactionDetail;
                    transactionsList.Add(transactionDetail);
                   
                    //generate agent commission
                    var commissionTransaction = GetAgentCommissionTransaction((long)transactionDetail.FromWalletId, (long)transactionDetail.ToWalletId, Math.Round(agentCommissionAmount, 2), transactionDetail.Id, out currencyId);
                    transactions.commissionTransaction = commissionTransaction;
                    transactionsList.Add(commissionTransaction);
                    
                    //generate tax transaction
                    var taxTransaction = GetTaxTransaction((long)transactionDetail.FromWalletId, Math.Round(taxAmount, 2), transactionDetail.Id, currencyId);
                    transactions.taxAmount = taxTransaction;
                    transactionsList.Add(taxTransaction);

                    //generate and transfer organization amount
                    var organizationFeeTransaction = GetOrganizationFeeTransaction((long)transactionDetail.FromWalletId, Math.Round(organizationFeeAmount, 2), transactionDetail.Id, currencyId);
                    transactions.organizationFeeAmount = organizationFeeTransaction;
                    transactionsList.Add(organizationFeeTransaction);

                    //await BatchAddTransactionsAsync(transactions);
                    //await Cashout(transactions);

                    if (agentDirectTransferAmount <= wallet.Balance)
                    {
                        await BatchAddTransactionsAsync(transactionsList);
                    }
                    else
                    {
                        throw new Exception($"Wallet balance is not sufficient.");
                    }

                }

                return transactionDetail;
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
       
        public async Task<IEnumerable<Transaction>> UpdateTransactionStatusInBulk(IEnumerable<long> walletIdList, Core.Enums.TransactionStatus status)
        {
            return await _unitOfWork.Transaction.UpdateTransactionStatusInBulk(walletIdList, status);
        }
       
        public async Task<double> CalculateTotalTransactionAmount(long walletId, bool isMonthly)
        {
            return await _unitOfWork.Transaction.CalculateWalletTransaction(walletId, isMonthly);
        }

        public async Task<TransactionResponseResource> GetTransactionsByTypeAndCurrency(int type, int currency, int pageIndex = 0, int pageSize = 10)
        {
            return await _unitOfWork.Transaction.GetTransactionsByTypeAndCurrency(type, currency, pageIndex, pageSize);
        }

        public async Task<TransactionResponseResource> GetTransactionsByUserIdAndType(string userId, int transferType, int pageIndex = 0, int pageSize = 10)
        {
            return await _unitOfWork.Transaction.GetTransactionsByUserIdAndType(userId, transferType, pageIndex, pageSize);
        }

        public async Task<TransactionResponseResource> GetTransactionsHistoryByUserIdAndType(string userId, int status, int transferType, int pageIndex = 0, int pageSize = 10)
        {
            return await _unitOfWork.Transaction.GetTransactionsHistoryByUserIdAndType(userId, status, transferType, pageIndex, pageSize);
        }

        public async Task<TransactionResponseResource> GetTransactionHistoryByUserId(string userId, int pageIndex, int pageSize)
        {
            return await _unitOfWork.Transaction.GetTransactionsHistoryByUserId(userId, pageIndex, pageSize);
        }

        public async Task<Transaction> BatchAddTransactionsAsync(IList<Transaction> transactionDetail)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                foreach (var transaction in transactionDetail)
                {
                    var transactionToUpdate = new Transaction();
                    if (transaction.Id > 0)
                    {
                        transactionToUpdate = await _unitOfWork.Transaction.GetByIdAsync(transaction.Id);
                        if (transactionToUpdate != null)
                        {
                            transaction.ParentId = transactionToUpdate.Id;
                            transactionToUpdate.Status = Core.Enums.TransactionStatus.Approved.GetHashCode();
                        }
                    }
                    else
                    {
                        transaction.Status = Core.Enums.TransactionStatus.Approved.GetHashCode();
                        transaction.CreatedDate = DateTime.Now;
                        //transaction.ParentId = transactionToUpdate.ParentId;
                        await _unitOfWork.Transaction.AddAsync(transaction);
                    }

                    if (transaction.FromWalletId != null && transaction.FromWalletId > 0)
                        await UpdateWalletBalance((long)transaction.FromWalletId, -transaction.Amount);
                    if (transaction.ToWalletId != null && transaction.ToWalletId > 0)
                        await UpdateWalletBalance((long)transaction.ToWalletId, transaction.Amount);

                    await _unitOfWork.CommitAsync();
                }

                // Commit the transaction
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                // Rollback if any exception occurs
                _unitOfWork.RollbackTransaction();
                throw;
            }

            return transactionDetail.FirstOrDefault();
        }

        public async Task<Transaction> BatchAddTransactionsAsync(WalletTransactions transactionDetail)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                // Create or update transaction for top/payment request
                if (transactionDetail.AgentCashout.Id > 0)
                {
                    var transactionToUpdate = await _unitOfWork.Transaction.GetByIdAsync(transactionDetail.AgentCashout.Id);
                    if (transactionToUpdate != null)
                    {
                        transactionToUpdate.Status = Core.Enums.TransactionStatus.Approved.GetHashCode();
                    }
                }
                else
                {
                    transactionDetail.AgentCashout.Status = Core.Enums.TransactionStatus.Approved.GetHashCode();
                    transactionDetail.AgentCashout.CreatedDate = DateTime.Now;
                    await _unitOfWork.Transaction.AddAsync(transactionDetail.AgentCashout);
                }

                await _unitOfWork.CommitAsync();

                // Update wallets and transactions
                await UpdateWalletAndTransactions(transactionDetail);

                // Commit the transaction
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                // Rollback if any exception occurs
                _unitOfWork.RollbackTransaction();
                throw;
            }

            return transactionDetail.AgentCashout;
        }

        private async Task UpdateWalletAndTransactions(WalletTransactions transactionDetail)
        {
            if (transactionDetail.commissionTransaction.TransferTypeId == TransferTypes.GeneratedAgentCommission.GetHashCode())
            {
                transactionDetail.commissionTransaction.ParentId = transactionDetail.commissionTransaction.Id;
                await _unitOfWork.Transaction.AddAsync(transactionDetail.commissionTransaction);

                await UpdateWalletBalance((long)transactionDetail.commissionTransaction.FromWalletId, -transactionDetail.commissionTransaction.Amount);
                await UpdateWalletBalance((long)transactionDetail.commissionTransaction.ToWalletId, transactionDetail.commissionTransaction.Amount);
            }

            if (transactionDetail.taxAmount.TransferTypeId == TransferTypes.WleeTax.GetHashCode())
            {
                transactionDetail.taxAmount.ParentId = transactionDetail.AgentCashout.Id;
                await _unitOfWork.Transaction.AddAsync(transactionDetail.taxAmount);

                await UpdateWalletBalance((long)transactionDetail.taxAmount.FromWalletId, -transactionDetail.taxAmount.Amount);
                await UpdateWalletBalance((long)transactionDetail.taxAmount.ToWalletId, transactionDetail.taxAmount.Amount);
            }

            if (transactionDetail.organizationFeeAmount.TransferTypeId == TransferTypes.CashOutStandardFeeSplit.GetHashCode())
            {
                transactionDetail.organizationFeeAmount.ParentId = transactionDetail.AgentCashout.Id;
                await _unitOfWork.Transaction.AddAsync(transactionDetail.organizationFeeAmount);

                await UpdateWalletBalance((long)transactionDetail.organizationFeeAmount.FromWalletId, -transactionDetail.organizationFeeAmount.Amount);
                await UpdateWalletBalance((long)transactionDetail.organizationFeeAmount.ToWalletId, transactionDetail.organizationFeeAmount.Amount);
            }

            if (transactionDetail.AgentCashout.TransferTypeId == TransferTypes.CashOut.GetHashCode())
            {
                await UpdateWalletBalance((long)transactionDetail.AgentCashout.FromWalletId, -transactionDetail.AgentCashout.Amount);
                await UpdateWalletBalance((long)transactionDetail.AgentCashout.ToWalletId, transactionDetail.AgentCashout.Amount);
            }

            await _unitOfWork.CommitAsync();
        }

        private async Task UpdateWalletBalance(long walletId, double amount)
        {
            var wallet = await _unitOfWork.Wallet.GetByIdAsync(walletId);
            wallet.Balance += amount;
            await _unitOfWork.CommitAsync();
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
                Status = TransactionStatus.Approved.GetHashCode(),
                TransferTypeId = TransferTypes.WleeTax.GetHashCode(),
                ToWalletId = currency == Currencies.LRD.GetHashCode() ? OrganizationWallet.WleeTax_LRD.GetHashCode() : OrganizationWallet.WleeTax_USD.GetHashCode(),
                Description = "wleepay tax"
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
                Status = TransactionStatus.Approved.GetHashCode(),
                TransferTypeId = TransferTypes.CashOutStandardFeeSplit.GetHashCode(),
                ToWalletId = currency == Currencies.LRD.GetHashCode() ? OrganizationWallet.OrgP_LRD.GetHashCode() : OrganizationWallet.OrgP_USD.GetHashCode(),
                Description = "Organization fee"
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
                Status = TransactionStatus.Approved.GetHashCode(),
                TransferTypeId = TransferTypes.GeneratedAgentCommission.GetHashCode(),
                ToWalletId = agentCommissionWallet.FirstOrDefault().Id,
                Description = "agent commission"
            };
            return orgFeeTransaction;

        }
        private Transaction DebitedCashOnHand(long fromWalletId, long toWalletId, double amount, long parentTransactionId, int currency)
        {
            var wallet = _unitOfWork.Wallet.GetUserByWalletIdAndTransferType(toWalletId, WalletTypes.SystemWallet.GetHashCode()).Result;

            var cashOnHandWallet = _unitOfWork.Wallet.GetWalletsByUserId(wallet.UserId, wallet.CurrencyId, WalletTypes.DebitFloatN.GetHashCode()).Result;

            Transaction addNewTransaction = new Transaction();
            addNewTransaction.FromWalletId = cashOnHandWallet.FirstOrDefault().Id;
            addNewTransaction.ToWalletId = null;
            addNewTransaction.Status = TransactionStatus.Approved.GetHashCode();
            addNewTransaction.TransferTypeId = TransferTypes.InHandWithdrawl.GetHashCode();
            addNewTransaction.CreatedDate = DateTime.Now;
            addNewTransaction.ParentId = parentTransactionId;
            addNewTransaction.Description = "Debited from cash on hand wallet";
            addNewTransaction.Amount = amount;

            return addNewTransaction;

        }
        private Transaction CreditedCashOnHand(long fromWalletId, long toWalletId, double amount, long parentTransactionId, int currency)
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

        }

        #endregion
    }
}
