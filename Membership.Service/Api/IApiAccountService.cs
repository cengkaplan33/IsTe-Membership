using Membership.Service.Models.Api.Request;
using Membership.Service.Models.Api.Response;

namespace Membership.Service.Api
{
    public interface IApiAccountService
    {
        InsertAccountResponse InsertAccount(InsertAccountRequest request);

        UpdateAccountResponse UpdateAccount(UpdateAccountRequest request);

        DeleteAccountResponse DeleteAccount(DeleteAccountRequest request);

        GetAccountReponse GetAccountByAccountCode(GetAccountRequest request);

        InsertAccountAddressResponse InsertAccountAddress(InsertAccountAddressRequest request);

        UpdateAccountAddressResponse UpdateAccountAddress(UpdateAccountAddressRequest request);

        DeleteAccountAddressResponse DeleteAccountAddress(DeleteAccountAddressRequest request);

        GetAccountAddressResponse GetAccountAddressByAddressCode(GetAccountAddressRequest request);

        InsertAccountContactResponse InsertAccountContact(InsertAccountContactRequest request);

        UpdateAccountContactResponse UpdateAccountContact(UpdateAccountContactRequest request);

        DeleteAccountContactResponse DeleteAccountContact(DeleteAccountContactRequest request);

        GetAccountContactResponse GetAccountContactByContactCode(GetAccountContactRequest request);
    }
}