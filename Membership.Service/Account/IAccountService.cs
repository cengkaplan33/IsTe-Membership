namespace Membership.Service.Account
{
    public interface IAccountService
    {
        Core.Domain.Account.Account GetAccountById(int accountId);

        Core.Domain.Account.Account GetAccountByAccountCode(string accountCode);

        Core.Domain.Account.Account SaveAccount(Core.Domain.Account.Account account);

        void UpdateAccount(Core.Domain.Account.Account account);

        void DeleteAccount(Core.Domain.Account.Account account);

        void SavePasswordChange(Core.Domain.Account.AccountPasswordChange accountPasswordChange);

        bool IsEmailExistInApplication(int applicationId, string email);        
    }
}