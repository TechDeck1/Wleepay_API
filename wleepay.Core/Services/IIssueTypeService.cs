using System;
using System.Collections.Generic;
using System.Text;
using wleepay.Core.Models;

namespace wleepay.Core.Services
{
    public interface IIssueTypeService
    {
        IEnumerable<IssueType> GetAllIssueTypes();
    }
}
