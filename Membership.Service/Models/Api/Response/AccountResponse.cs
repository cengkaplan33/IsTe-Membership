namespace Membership.Service.Models.Api.Response
{
    public class AccountResponse : AccountResponseBase
    {
    }

    public class GetAccountReponse : ResponseBase
    {
        public AccountResponse Account { get; set; }
    }

    public class InsertAccountResponse : ResponseBase
    {
        public AccountResponse Account { get; set; }
    }

    public class UpdateAccountResponse : ResponseBase
    {
    }

    public class DeleteAccountResponse : ResponseBase
    {
    }


    public class AccountAddressResponse : AccountAddressResponeBase
    {
    }

    public class GetAccountAddressResponse : ResponseBase
    {
        public AccountAddressResponse AccountAddress { get; set; }
    }

    public class InsertAccountAddressResponse : ResponseBase
    {
        public AccountAddressResponse AccountAddress { get; set; }
    }

    public class UpdateAccountAddressResponse : ResponseBase
    {
    }

    public class DeleteAccountAddressResponse : ResponseBase
    {
    }


    public class AccountContactResponse : AccountContactResponseBase
    {
    }

    public class GetAccountContactResponse : ResponseBase
    {
        public AccountContactResponse AccountContact { get; set; }
    }

    public class InsertAccountContactResponse : ResponseBase
    {
        public AccountContactResponse AccountContact { get; set; }
    }

    public class UpdateAccountContactResponse : ResponseBase
    {
    }

    public class DeleteAccountContactResponse : ResponseBase
    {
    }


    public class BlockAccountResponse : ResponseBase
    {
    }

    public class UnblockAccountResponse : ResponseBase
    {
    }
}