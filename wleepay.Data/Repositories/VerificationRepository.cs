using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Pkcs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core.Models;
using wleepay.Core.Repositories;
using wleepay.Data.Context;

namespace wleepay.Data.Repositories
{
    public class VerificationRepository : Repository<Verification>, IVerificationRepository
    {

        public VerificationRepository(WPContext context)
            : base(context) { }

        private WPContext WPContext
        {
            get { return Context as WPContext; }
        }

        public async Task<Verification> AddVerification(Verification verificationRequest)
        {
           var verification = await WPContext.Verification.Where(x => x.PhoneNumber == verificationRequest.PhoneNumber).FirstOrDefaultAsync();
           if(verification != null)
            {
                verification.VerificationCode = verificationRequest.VerificationCode;
                verification.VerificationExpiry = verificationRequest.VerificationExpiry;
                WPContext.Verification.Update(verification);
                WPContext.SaveChanges();
                return verification;

            }
            else
            {
                await WPContext.Verification.AddAsync(verificationRequest);
                WPContext.SaveChanges();
                return verificationRequest;

            }

        }

        public async Task<Verification> GetVerificationByPhone(string phoneNumber)
        {
           var verification=  await WPContext.Verification.Where(x => x.PhoneNumber == phoneNumber).FirstOrDefaultAsync();
           return verification;
        }
    }
}
