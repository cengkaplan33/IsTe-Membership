using System.Collections.Generic;
using Membership.Core.Domain.Account;

namespace Membership.Data.Repositories.Account
{
    public interface IAccountIpRepository
    {
        List<AccountIp> GetAccountIpList(Core.Domain.Account.Account account);
    }
}