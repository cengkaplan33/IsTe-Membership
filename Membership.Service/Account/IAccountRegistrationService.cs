using Membership.Service.Models.Account;

namespace Membership.Service.Account
{
    public interface IAccountRegistrationService
    {
        AccountRegistrationResult RegisterAccount(AccountRegistrationRequest request);

        ChangePasswordResult ChangePassword(ChangePasswordRequest request);

        RecoveryPasswordResult RecoveryPassword(RecoveryPasswordRequest request);

        AccountBlockResult Block(AccountBlockRequest request);

        AccountBlockResult Unblock(AccountBlockRequest request);

        CheckEmailResult CheckEmail(CheckEmailRequest request);
    }
}