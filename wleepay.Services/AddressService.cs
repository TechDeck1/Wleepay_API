using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using wleepay.Core;
using wleepay.Core.Models;
using wleepay.Core.Services;
using System.Linq;

namespace wleepay.Services
{
    public class AddressService : IAddressService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddressService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<Address> AddOrUpdateAddress(Address addressModel)
        {
            var addressToUpdate = await _unitOfWork.Address.GetAddressByUserId(addressModel.UserId);
            if (addressToUpdate != null)
                addressToUpdate = addressModel;
            else
                await _unitOfWork.Address.AddAsync(addressModel);

            await _unitOfWork.CommitAsync();

            return addressModel;
        }

        public async Task<Address> GetAddressByUserId(string id)
        {
            return await _unitOfWork.Address.GetAddressByUserId(id);
        }
    }
}
