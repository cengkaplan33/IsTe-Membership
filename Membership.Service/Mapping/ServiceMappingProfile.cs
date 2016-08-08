using AutoMapper;
using Membership.Core.Domain.Account;
using Membership.Service.Models.Api.Request;
using Membership.Service.Models.Api.Response;

namespace Membership.Service.Mapping
{
    public class ServiceMappingProfile : Profile
    {
        public ServiceMappingProfile()
        {
            CreateMap<InsertAccountRequest, Core.Domain.Account.Account>();
            CreateMap<UpdateAccountRequest, Core.Domain.Account.Account>();
            CreateMap<Core.Domain.Account.Account, AccountResponse>();

            CreateMap<InsertAccountAddressRequest, AccountAddress>();
            CreateMap<AccountAddress, AccountAddressResponse>();
            CreateMap<UpdateAccountAddressRequest, AccountAddress>();

            CreateMap<InsertAccountContactRequest, AccountContact>();
            CreateMap<UpdateAccountContactRequest, AccountContact>();
            CreateMap<AccountContact, AccountContactResponse>();
        }
    }
}