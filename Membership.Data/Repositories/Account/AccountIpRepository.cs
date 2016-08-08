using System.Collections.Generic;
using Membership.Core.Domain.Account;
using Membership.Core.Enum;
using Membership.Core.Helper;

namespace Membership.Data.Repositories.Account
{
    public class AccountIpRepository : IAccountIpRepository
    {
        private readonly IRepository<AccountIp> _accountIpRepository;

        public AccountIpRepository(IRepository<AccountIp> accountIpRepository)
        {
            _accountIpRepository = accountIpRepository;
        }

        public List<AccountIp> GetAccountIpList(Core.Domain.Account.Account account)
        {
            if (account == null)
                ExceptionHelper.ThrowIfNull(() => account);

            var recordSetAccountIp = _accountIpRepository.Find(a => a.AccountId == account.Id
                && a.IsDeleted == (byte)GeneralEnum.IsDeleted.No);

            return recordSetAccountIp;
        }
    }
}