using FluentValidation;
using Membership.Core.Extension;
using Membership.Service.Models.Account;
using Membership.Service.Resources;

namespace Membership.Service.Validation
{
    public class AccountBlockValidator : AbstractValidator<AccountBlockRequest>
    {
        public AccountBlockValidator()
        {
            ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(account => account.AccountCode)
                .Must(m => !m.IsNullOrWhitespace())
                .WithMessage(ServiceResponseMessage.INVALID_ACCOUNT_CODE);          
        }
    }
}