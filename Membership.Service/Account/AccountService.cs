using Membership.Core.Domain.Account;
using Membership.Data.Repositories.Account;

namespace Membership.Service.Account
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public Core.Domain.Account.Account GetAccountById(int accountId)
        {
            return _accountRepository.GetAccountById(accountId);
        }

        public Core.Domain.Account.Account GetAccountByAccountCode(string accountCode)
        {
            return _accountRepository.GetAccountByAccountCode(accountCode);
        }

        public Core.Domain.Account.Account SaveAccount(Core.Domain.Account.Account account)
        {
            return _accountRepository.SaveAccount(account);
        }

        public void UpdateAccount(Core.Domain.Account.Account account)
        {
            _accountRepository.UpdateAccount(account);
        }

        public void DeleteAccount(Core.Domain.Account.Account account)
        {
            _accountRepository.DeleteAccount(account);
        }

        public void SavePasswordChange(AccountPasswordChange accountPasswordChange)
        {
            _accountRepository.SavePasswordChange(accountPasswordChange);
        }

        public bool IsEmailExistInApplication(int applicationId, string email)
        {
            return _accountRepository.IsEmailExistInApplication(applicationId, email);
        }      
    }
}