using Membership.Core.Domain.Account;

namespace Membership.Data.Repositories.Account
{
    public interface IAccountLoginRepository
    {
        void InsertLoginLog(AccountLoginLog accountLoginLog);

        void InsertLoginAttempt(AccountLoginAttempt accountLoginAttempt);
    }
}