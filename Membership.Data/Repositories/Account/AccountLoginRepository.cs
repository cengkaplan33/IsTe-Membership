using Membership.Core.Domain.Account;
using Membership.Core.Helper;

namespace Membership.Data.Repositories.Account
{
    public class AccountLoginRepository : IAccountLoginRepository
    {
        private readonly IRepository<AccountLoginLog> _accountLoginLogRepository;
        private readonly IRepository<AccountLoginAttempt> _accountLoginAttemptRepository;

        public AccountLoginRepository(IRepository<AccountLoginLog> accountLoginLogRepository,
            IRepository<AccountLoginAttempt> accountLoginAttemptRepository)
        {
            _accountLoginLogRepository = accountLoginLogRepository;
            _accountLoginAttemptRepository = accountLoginAttemptRepository;
        }

        public void InsertLoginLog(AccountLoginLog accountLoginLog)
        {
            if (accountLoginLog == null)
                ExceptionHelper.ThrowIfNull(() => accountLoginLog);

            _accountLoginLogRepository.Insert(accountLoginLog);
        }

        public void InsertLoginAttempt(AccountLoginAttempt accountLoginAttempt)
        {
            if (accountLoginAttempt == null)
                ExceptionHelper.ThrowIfNull(() => accountLoginAttempt);

            _accountLoginAttemptRepository.Insert(accountLoginAttempt);
        }
    }
}