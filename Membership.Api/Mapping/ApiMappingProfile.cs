using AutoMapper;
using Membership.Api.Models.Account;
using Membership.Api.Models.Authentication;
using Membership.Service.Models.Account;
using Membership.Service.Models.Api.Request;
using Membership.Service.Models.Authentication;

namespace Membership.Api.Mapping
{
    internal class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<InsertAccountRequestModel, InsertAccountRequest>();

            CreateMap<UpdateAccountRequestModel, UpdateAccountRequest>();

            CreateMap<DeleteAccountRequestModel, DeleteAccountRequest>();


            CreateMap<InsertAccountAddressRequestModel, InsertAccountAddressRequest>();

            CreateMap<UpdateAccountAddressRequestModel, UpdateAccountAddressRequest>();

            CreateMap<DeleteAccountAddressRequestModel, DeleteAccountAddressRequest>();


            CreateMap<InsertAccountContactRequestModel, InsertAccountContactRequest>();

            CreateMap<UpdateAccountContactRequestModel, UpdateAccountContactRequest>();

            CreateMap<DeleteAccountContactRequestModel, DeleteAccountContactRequest>();


            CreateMap<ChangePasswordRequestModel, ChangePasswordRequest>();

            CreateMap<RecoveryPasswordRequestModel, RecoveryPasswordRequest>();

            CreateMap<AccountBlockRequestModel, AccountBlockRequest>();

            CreateMap<CheckEmailRequestModel, CheckEmailRequest>();

            CreateMap<AuthenticationRequestModel, AuthenticationRequest>();
        }
    }
}