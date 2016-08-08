using System.Collections.Generic;
using System.Linq;
using Membership.Core.Domain.Account;
using Membership.Core.Enum;
using Membership.Core.Helper;

namespace Membership.Data.Repositories.Account
{
    public class AccountAddressRepository : IAccountAddressRepository
    {
        private readonly IRepository<AccountAddress> _accountAddressRepository;

        public AccountAddressRepository(IRepository<AccountAddress> accountAddressRepository)
        {
            _accountAddressRepository = accountAddressRepository;
        }

        public AccountAddress GetAccountAddressById(int accountAddressId)
        {
            var rowAccountAddress = _accountAddressRepository.FindOne(c => c.Id == accountAddressId
              && c.IsDeleted == (byte)GeneralEnum.IsDeleted.No);

            return rowAccountAddress;
        }

        public AccountAddress GetAccountAddressByAccountAddressCode(string accountAddressCode)
        {
            var rowAccountAddress = _accountAddressRepository.FindOne(c => c.AddressCode == accountAddressCode
               && c.IsDeleted == (byte)GeneralEnum.IsDeleted.No);

            return rowAccountAddress;
        }

        public List<AccountAddress> GetAccountAddressesByAccountId(int accountId)
        {
            var recordSetAccountAddress = _accountAddressRepository.Find(c => c.AccountId== accountId
                && c.IsDeleted == (byte)GeneralEnum.IsDeleted.No).ToList();

            return recordSetAccountAddress;
        }

        public AccountAddress SaveAccountAddress(AccountAddress accountAddress)
        {
            if (accountAddress == null)
                ExceptionHelper.ThrowIfNull(() => accountAddress);

            return _accountAddressRepository.Insert(accountAddress);
        }

        public void UpdateAccountAddress(AccountAddress accountAddress)
        {
            if (accountAddress == null)
                ExceptionHelper.ThrowIfNull(() => accountAddress);

            _accountAddressRepository.Update(accountAddress);
        }

        public void DeleteAccountAddress(AccountAddress accountAddress)
        {
            if (accountAddress == null)
                ExceptionHelper.ThrowIfNull(() => accountAddress);

            _accountAddressRepository.Delete(accountAddress);
        }
    }
}