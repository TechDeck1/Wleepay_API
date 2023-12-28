using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wleepay.API.Resources
{
    public class PagingRequest
    {
        public int Index { get; set; }
        public int Size { get; set; }
        public Core.Enums.Role Role { get; set; }
    }

    public class Pagination
    {
        public int Index { get; set; }
        public int Size { get; set; }
    }

    public class FilterLogs 
    {
        public string email { get; set; }
    }

}
