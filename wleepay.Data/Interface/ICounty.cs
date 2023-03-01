using System;
using System.Collections.Generic;
using System.Text;
using wleepay.Core.Models;

namespace wleepay.Data.Interface
{
    public interface ICounty
    {
        IEnumerable<County> GetAllCounty();

        string GetNameCountyByID(int countyID);
    }
}
