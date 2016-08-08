using System.Collections.Generic;
using System.Linq;
using Membership.Core.Domain.Account;
using Membership.Core.Enum;
using Membership.Core.Helper;

namespace Membership.Data.Repositories.Account
{
    public class AccountContactRepository : IAccountContactRepository
    {
        private readonly IRepository<AccountContact> _accountContactRepository;

        public AccountContactRepository(IRepository<AccountContact> accountContactRepository)
        {
            _accountContactRepository = accountContactRepository;
        }

        public AccountContact GetAccountContactById(int accountContactId)
        {
            var rowAccountContact = _accountContactRepository.FindOne(c => c.Id == accountContactId
            && c.IsDeleted == (byte)GeneralEnum.IsDeleted.No);

            return rowAccountContact;
        }

        public AccountContact GetAccountContactByAccountContactCode(string accountContactCode)
        {
            var rowAccountContact = _accountContactRepository.FindOne(c => c.ContactCode == accountContactCode
               && c.IsDeleted == (byte)GeneralEnum.IsDeleted.No);

            return rowAccountContact;
        }

        public List<AccountContact> GetAccountContactsByAccountId(int accountId)
        {
            var recordSetAccountContact = _accountContactRepository.Find(c => c.AccountId == accountId
             && c.IsDeleted == (byte)GeneralEnum.IsDeleted.No).ToList();

            return recordSetAccountContact;
        }

        public AccountContact SaveAccountContact(AccountContact accountContact)
        {
            if (accountContact == null)
                ExceptionHelper.ThrowIfNull(() => accountContact);

            return _accountContactRepository.Insert(accountContact);
        }

        public void UpdateAccountContact(AccountContact accountContact)
        {
            if (accountContact == null)
                ExceptionHelper.ThrowIfNull(() => accountContact);

            _accountContactRepository.Update(accountContact);
        }

        public void DeleteAccountContact(AccountContact accountContact)
        {
            if (accountContact == null)
                ExceptionHelper.ThrowIfNull(() => accountContact);

            _accountContactRepository.Delete(accountContact);
        }
    }
}