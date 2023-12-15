using DevExpress.Xpo;
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
    public class SecurityQuestionsRepository : Repository<UserDetail>, ISecurityQuestionsRepository
    {

        public SecurityQuestionsRepository(WPContext context)
           : base(context) { }

        private WPContext WPContext
        {
            get { return Context as WPContext; }
        }

        public IEnumerable<SecurityQuestions> GetAllSecurityQuestions()
        {
            var result = WPContext.SecurityQuestions.ToList();
            return result;
        }
    }
}
