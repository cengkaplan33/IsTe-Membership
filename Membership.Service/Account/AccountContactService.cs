using System.Collections.Generic;
using Membership.Core.Domain.Account;
using Membership.Data.Repositories.Account;

namespace Membership.Service.Account
{
    public class AccountContactService : IAccountContactService
    {
        private readonly IAccountContactRepository _accountContactRepository;

        public AccountContactService(IAccountContactRepository accountContactRepository)
        {
            _accountContactRepository = accountContactRepository;
        }

        public AccountContact GetAccountContactById(int accountContactId)
        {
            return _accountContactRepository.GetAccountContactById(accountContactId);
        }

        public AccountContact GetAccountContactByAccountContactCode(string accountContactCode)
        {
            return _accountContactRepository.GetAccountContactByAccountContactCode(accountContactCode);
        }

        public List<AccountContact> GetAccountContactsByAccountId(int accountId)
        {
            return _accountContactRepository.GetAccountContactsByAccountId(accountId);
        }

        public AccountContact SaveAccountContact(AccountContact accountContact)
        {
            return _accountContactRepository.SaveAccountContact(accountContact);
        }

        public void UpdateAccountContact(AccountContact accountContact)
        {
            _accountContactRepository.UpdateAccountContact(accountContact);
        }

        public void DeleteAccountContact(AccountContact accountContact)
        {
            _accountContactRepository.DeleteAccountContact(accountContact);
        }
    }
}