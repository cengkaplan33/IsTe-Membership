using System.Collections.Generic;

namespace Membership.Data.Repositories.Account
{
    public interface IAccountContactRepository
    {
        Core.Domain.Account.AccountContact GetAccountContactById(int accountContactId);

        Core.Domain.Account.AccountContact GetAccountContactByAccountContactCode(string accountContactCode);

        List<Core.Domain.Account.AccountContact> GetAccountContactsByAccountId(int accountId);

        Core.Domain.Account.AccountContact SaveAccountContact(Core.Domain.Account.AccountContact accountContact);

        void UpdateAccountContact(Core.Domain.Account.AccountContact accountContact);

        void DeleteAccountContact(Core.Domain.Account.AccountContact accountContact);
    }
}