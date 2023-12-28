using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wleepay.API.Resources;
using wleepay.Core.Models;

namespace wleepay.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<UserDetailResource, UserDetail>().ReverseMap();
            CreateMap<ApplicationUser, ApplicationUserResource>()
                .ForPath(dest => dest.UserRoles.Name, src => src.MapFrom(x => x.UserRoles.FirstOrDefault().Role.Name))
                .ForPath(dest => dest.UserRoles.Id, src => src.MapFrom(x => x.UserRoles.FirstOrDefault().Role.Id))
                .ForPath(dest => dest.UserRoles.NormalizedName, src => src.MapFrom(x => x.UserRoles.FirstOrDefault().Role.NormalizedName))


                .ReverseMap();
            CreateMap<ApplicationUser, UserAccountResource>().ReverseMap();
            
            CreateMap<AddUserDetailsResource, UserDetail>().ReverseMap();
            CreateMap<Address, AddressResource>().ReverseMap();

            CreateMap<AddUserDetailsResource, KYCdocuments>()
                    .ForMember(dest => dest.Type, src => src.MapFrom(x => x.DocumentTypeId))
                    .ForMember(dest => dest.DocumentImage, src => src.MapFrom(x => x.KYCDocumentImage))
                    .ReverseMap();

            CreateMap<KYCDocumentResource, wleepay.Core.Models.KYCdocuments>().ReverseMap();
            CreateMap<DocumentTypeResource, wleepay.Core.Models.DocumentType>().ReverseMap();

            CreateMap<SmileIdentityJobResource, wleepay.Core.Models.SmileIdentityJob>().ReverseMap();

            CreateMap<LookupResource, wleepay.Core.Models.KYCStatus>().ReverseMap();
            CreateMap<LookupResource, wleepay.Core.Models.DocumentType>().ReverseMap();
            CreateMap<LookupResource, wleepay.Core.Models.County>().ReverseMap();
            CreateMap<LookupResource, wleepay.Core.Models.TransactionStatus>().ReverseMap();

            CreateMap<Core.Models.County, API.Resources.County>().ReverseMap();
            CreateMap<Core.Models.KYCStatus, API.Resources.KYCStatus>().ReverseMap();
            CreateMap<Core.Models.KYCLevel, API.Resources.KYCLevel>().ReverseMap();

            CreateMap<AddWalletResource, Wallet>().ReverseMap();
            CreateMap<Wallet, WalletResource>().ReverseMap();
            
            CreateMap<WalletType, WalletTypeResource>().ReverseMap();

            CreateMap<CurrencyResource, Currency>().ReverseMap();

            CreateMap<TransferType, TransferTypeResource>().ReverseMap();
            CreateMap<Transaction, TransactionResource>()
                .ForMember(dest => dest.Currency, src => src.MapFrom(x => x.FromWallet.Currency));
            CreateMap<Transaction, TransactionHistoryResource>()
                .ForMember(dest => dest.Currency, src => src.MapFrom(x => x.FromWallet.Currency))
                .ForMember(dest => dest.FromWalletUser, src => src.MapFrom(x => x.FromWallet.ApplicationUser.UserDetail))
                .ForMember(dest => dest.ToWalletUser, src => src.MapFrom(x => x.ToWallet.ApplicationUser.UserDetail));

            CreateMap<Transaction, CreateTransactionResource>().ReverseMap();
            CreateMap<TaxRateResourceModel, TransferFee>().ReverseMap();
            CreateMap<AddStaffDetailsResource, UserDetail>().ReverseMap();
            CreateMap<AddStaffDetailsResource, KYCdocuments>()
                   .ForMember(dest => dest.Type, src => src.MapFrom(x => x.DocumentTypeId))
                   .ForMember(dest => dest.DocumentImage, src => src.MapFrom(x => x.KYCDocumentImage))
                   .ReverseMap();
            
        }
    }
}
