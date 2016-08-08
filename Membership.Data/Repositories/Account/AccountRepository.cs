using Membership.Core.Domain.Account;
using Membership.Core.Enum;
using Membership.Core.Helper;

namespace Membership.Data.Repositories.Account
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IRepository<Core.Domain.Account.Account> _accountRepository;
        private readonly IRepository<AccountPasswordChange> _accountPasswordChangeRepository;

        public AccountRepository(IRepository<Core.Domain.Account.Account> accountRepository,
            IRepository<AccountPasswordChange> accountPasswordChangeRepository)
        {
            _accountRepository = accountRepository;
            _accountPasswordChangeRepository = accountPasswordChangeRepository;
        }

        public Core.Domain.Account.Account GetAccountById(int accountId)
        {
            var rowAccount = _accountRepository.FindOne(c => c.Id == accountId
                && c.IsDeleted == (byte)GeneralEnum.IsDeleted.No);

            return rowAccount;
        }

        public Core.Domain.Account.Account GetAccountByAccountCode(string accountCode)
        {
            var rowAccount = _accountRepository.FindOne(c => c.AccountCode == accountCode
                && c.IsDeleted == (byte)GeneralEnum.IsDeleted.No);

            return rowAccount;
        }

        public Core.Domain.Account.Account SaveAccount(Core.Domain.Account.Account account)
        {
            if (account == null)
                ExceptionHelper.ThrowIfNull(() => account);

            return _accountRepository.Insert(account);
        }

        public void UpdateAccount(Core.Domain.Account.Account account)
        {
            if (account == null)
                ExceptionHelper.ThrowIfNull(() => account);

            _accountRepository.Update(account);
        }

        public void DeleteAccount(Core.Domain.Account.Account account)
        {
            if (account == null)
                ExceptionHelper.ThrowIfNull(() => account);

            _accountRepository.Delete(account);
        }

        public void SavePasswordChange(AccountPasswordChange accountPasswordChange)
        {
            if (accountPasswordChange == null)
                ExceptionHelper.ThrowIfNull(() => accountPasswordChange);

            _accountPasswordChangeRepository.Insert(accountPasswordChange);
        }

        public bool IsEmailExistInApplication(int applicationId, string email)
        {
            var account = _accountRepository.FindOne(a => a.Email == email && a.ApplicationId == applicationId
                && a.IsDeleted == (byte)GeneralEnum.IsDeleted.No);

            return account != null;
        }

        public Core.Domain.Account.Account GetAccountByApplicationIdAndEmail(int applicationId, string email)
        {
            var account = _accountRepository.FindOne(a => a.ApplicationId == applicationId && a.Email == email
                && a.IsDeleted == (byte)GeneralEnum.IsDeleted.No);

            return account;
        }        
    }
}