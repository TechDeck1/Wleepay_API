using System.Threading.Tasks;
using wleepay.Core.Models;

namespace wleepay.Core.Repositories
{
    public interface IAddressRepository : IRepository<Address>
    {
        Task<Address> GetAddressByUserId(string userId);
    }
}