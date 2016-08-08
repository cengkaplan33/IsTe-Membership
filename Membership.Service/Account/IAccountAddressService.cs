using System.Collections.Generic;

namespace Membership.Service.Account
{
    public interface IAccountAddressService
    {
        Core.Domain.Account.AccountAddress GetAccountAddressById(int accountAddressId);

        Core.Domain.Account.AccountAddress GetAccountAddressByAccountAddressCode(string accountAddressCode);

        List<Core.Domain.Account.AccountAddress> GetAccountAddressesByAccountId(int accountId);

        Core.Domain.Account.AccountAddress SaveAccountAddress(Core.Domain.Account.AccountAddress accountAddress);

        void UpdateAccountAddress(Core.Domain.Account.AccountAddress accountAddress);

        void DeleteAccountAddress(Core.Domain.Account.AccountAddress accountAddress);
    }
}