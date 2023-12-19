using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core;
using wleepay.Core.Models;
using wleepay.Core.Services;

namespace wleepay.Services
{
    public class TokenStorageService : ITokenStorageService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TokenStorageService(IUnitOfWork unitOfWork, ITransactionService transactionService)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<TokenStorage> GetTokenStorageById(string userId)
        {
            var tokenStorage = new TokenStorage();
            var token = await _unitOfWork.TokenStorage.GetTokenStorageById(userId);
            return token;

        }

        public async Task<TokenStorage> UpdateToken(TokenStorage tokenStorage)
        {
            var TokenToUpdate =await _unitOfWork.TokenStorage.GetTokenStorageById(tokenStorage.UserId);
            if (TokenToUpdate != null)
            {
                TokenToUpdate.ExpirationTime = DateTime.Now;
                TokenToUpdate.Token = tokenStorage.Token;
                await _unitOfWork.CommitAsync();
            }
            else 
            {
                tokenStorage.ExpirationTime = DateTime.Now;
                await _unitOfWork.TokenStorage.AddAsync(tokenStorage);
            }

            _unitOfWork.CommitAsync();
            return TokenToUpdate;
        }
    }
}
