using System;
using System.Collections.Generic;
using System.Text;
using wleepay.Core.Models;
using wleepay.Core.Repositories;
using wleepay.Data.Context;

using System.Linq;

namespace wleepay.Data.Repositories
{
    public class IssueTypeRepository : Repository<IssueType>, IIssueTypeRepository
    {
        public IssueTypeRepository(WPContext context): base(context) { }

        private WPContext WPContext
        {
            get { return Context as WPContext; }
        }

        public IEnumerable<IssueType> GetAllIssueTypes()
        {
            var result = WPContext.IssueType.ToList();
            return result; 
        }

    }
}
