using System.Collections.Generic;
using Membership.Core.Domain.Account;
using Membership.Data.Repositories.Account;

namespace Membership.Service.Account
{
    public class AccountAddressService : IAccountAddressService
    {
        private readonly IAccountAddressRepository _accountAddressRepository;

        public AccountAddressService(IAccountAddressRepository accountAddressRepository)
        {
            _accountAddressRepository = accountAddressRepository;
        }

        public AccountAddress GetAccountAddressById(int accountAddressId)
        {
            return _accountAddressRepository.GetAccountAddressById(accountAddressId);
        }

        public AccountAddress GetAccountAddressByAccountAddressCode(string accountAddressCode)
        {
            return _accountAddressRepository.GetAccountAddressByAccountAddressCode(accountAddressCode);
        }

        public List<AccountAddress> GetAccountAddressesByAccountId(int accountId)
        {
            return _accountAddressRepository.GetAccountAddressesByAccountId(accountId);
        }

        public AccountAddress SaveAccountAddress(AccountAddress accountAddress)
        {
            return _accountAddressRepository.SaveAccountAddress(accountAddress);
        }

        public void UpdateAccountAddress(AccountAddress accountAddress)
        {
            _accountAddressRepository.UpdateAccountAddress(accountAddress);
        }

        public void DeleteAccountAddress(AccountAddress accountAddress)
        {
            _accountAddressRepository.DeleteAccountAddress(accountAddress);
        }
    }
}