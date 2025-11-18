using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wleepay.API.Controllers;
using wleepay.API.Resources;
using wleepay.Core.DTO.Request;
using wleepay.Core.Models;

namespace wleepay.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<UserDetailResource, UserDetail>().ReverseMap();
            CreateMap<EditUserDetailResource, UserDetail>().ReverseMap();
            CreateMap<ApplicationUser, ApplicationUserResource>()
                .ForPath(dest => dest.UserRoles.Name, src => src.MapFrom(x => x.UserRoles.FirstOrDefault().Role.Name))
                .ForPath(dest => dest.UserRoles.Id, src => src.MapFrom(x => x.UserRoles.FirstOrDefault().Role.Id))
                .ForPath(dest => dest.UserRoles.NormalizedName, src => src.MapFrom(x => x.UserRoles.FirstOrDefault().Role.NormalizedName))

                .ReverseMap();
            CreateMap<ApplicationUser, UserAccountResource>() .ForPath(dest => dest.UserRoles.Name, src => src.MapFrom(x => x.UserRoles.FirstOrDefault().Role.Name))
                .ForPath(dest => dest.UserRoles.Id, src => src.MapFrom(x => x.UserRoles.FirstOrDefault().Role.Id))
                .ForPath(dest => dest.UserRoles.NormalizedName, src => src.MapFrom(x => x.UserRoles.FirstOrDefault().Role.NormalizedName))
                .ReverseMap();
            
            CreateMap<AddUserDetailsResource, UserDetail>().ReverseMap();

            CreateMap<RegisterUserRequest, UserDetail>()
            .AfterMap((src, dest) =>
            {
                dest.KYCStatusId = 1;                          
                dest.KYCLevel = 1;                            
                dest.Status = 1;                               
                dest.CreatedDate = DateTime.UtcNow;
                dest.UpdatedDate = DateTime.UtcNow;
                dest.IsLoggedIn = false;
                dest.FailedLoginAttempt = 0;
                dest.Image = "";                             
                dest.SecurityQuestionId = 1;                   
                dest.SecurityAnswerText = "";
                dest.CreatedBy = "System";                     
          })
         .ReverseMap();

            CreateMap<RechargeRequestDetails, ExecTransactionRequest>()
           .ForMember(dest => dest.Auth, opt => opt.MapFrom(src => new SochitelAuth
         {
             username = src.username,
             salt = src.salt,
             password = src.password
         }));

            CreateMap<PaymentTransaction, TransactionHistory>()
          .ForMember(dest => dest.TransferTypeName, opt => opt.Ignore());

            CreateMap<PaymentTransaction, TransactionReport>()
              .ForMember(dest => dest.Name, opt => opt.Ignore())
              .ForMember(dest => dest.Address, opt => opt.Ignore());

            CreateMap<PaymentTransactionDto, PaymentTransaction>()
                       .ForMember(dest => dest.Id, opt => opt.Ignore()) 
                       .ReverseMap();            

            CreateMap<Address, AddressResource>().ReverseMap();

           CreateMap<AddUserDetailsResource, KYCdocuments>()
                    .ForMember(dest => dest.Type, src => src.MapFrom(x => x.DocumentTypeId))
                    .ForMember(dest => dest.DocumentImage, src => src.MapFrom(x => x.KYCDocumentImage))
                    .ReverseMap();
            
            CreateMap<AddSystemAdminResource, KYCdocuments>()
                    .ForMember(dest => dest.Type, src => src.MapFrom(x => x.DocumentTypeId))
                    .ForMember(dest => dest.DocumentImage, src => src.MapFrom(x => x.KYCDocumentImage))
                    .ReverseMap();

            CreateMap<AddAgentDetailResource, KYCdocuments>()
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

            CreateMap<CurrencyResource, Core.Models.Currency>().ReverseMap();

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
            CreateMap<AddSystemAdminResource, UserDetail>().ReverseMap();
            CreateMap<AddAgentDetailResource, UserDetail>().ReverseMap();
            CreateMap<AddStaffDetailsResource, KYCdocuments>()
                   .ForMember(dest => dest.Type, src => src.MapFrom(x => x.DocumentTypeId))
                   .ForMember(dest => dest.DocumentImage, src => src.MapFrom(x => x.KYCDocumentImage))
                   .ReverseMap();

            CreateMap<EditUserDetailResource, KYCdocuments>()
                  .ForMember(dest => dest.Type, src => src.MapFrom(x => x.DocumentTypeId))
                  .ForMember(dest => dest.DocumentImage, src => src.MapFrom(x => x.KYCDocumentImage))
                  .ReverseMap();

            CreateMap<CustomerTickets, AddCustomerTicketResponseResource>().ReverseMap();
            CreateMap<CustomerTickets, AddCustomerTicketRequestResponse>().ReverseMap();

            CreateMap<Notes, NotesResource>().ReverseMap();
            CreateMap<IssueType, IssueTypeResource>().ReverseMap();
            CreateMap<UsersStaffRole, UsersStaffRoleResource>().ReverseMap();
            CreateMap<StaffRoles, StaffRoleResource>().ReverseMap();
            CreateMap<StaffPermissionType, StaffPermissionTypeResource>().ReverseMap();
            CreateMap<ApplicationUser, StaffPermissionsResponseResource>().ReverseMap();
            CreateMap<CashFlowHistory, CashFlowHistoryResource>().ReverseMap();
            CreateMap<SystemStatus, SystemStatusResource>().ReverseMap();

            CreateMap<SochitelTransactions, SochitelTransactionCommissionEarnedResource>().ReverseMap();

            CreateMap<ExecTransactionRequest, SochitelTransactionRequest>()
                 .ForMember(dest => dest.Amount, src => src.MapFrom(x => x.AmountInUsd))
                 .ReverseMap();

            CreateMap<PaymentRequestForStripe, SochitelTransactionRequest>().ReverseMap();
            CreateMap<PaymentRequestForStripe, SochitelBillPaymentTransactionRequest>().ReverseMap();

            CreateMap<BillPaymentTransactionRequest, SochitelBillPaymentTransactionRequest>()
                 .ForMember(dest => dest.Amount, src => src.MapFrom(x => x.AmountInUsd))
                 .ReverseMap();
        }
    }
}
