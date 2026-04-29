using System;
using System.Collections.Generic;
using System.Text;

namespace wleepay.Core.Models
{
    public class PaginatedTransactionResult<T>
    {
        public int TotalCount { get; set; }
        public List<T> Data { get; set; }
    }
}
