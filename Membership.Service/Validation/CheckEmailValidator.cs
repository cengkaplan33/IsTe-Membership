using FluentValidation;
using Membership.Core.Extension;
using Membership.Service.Models.Account;
using Membership.Service.Resources;

namespace Membership.Service.Validation
{
    public class CheckEmailValidator : AbstractValidator<CheckEmailRequest>
    {
        public CheckEmailValidator()
        {
            ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(account => account.ApplicationId)
              .Must(m => m.IsNumericAndGreaterThenZero())
              .WithMessage(ServiceResponseMessage.INVALID_ACCOUNT_APPLICATION_ID);

            RuleFor(account => account.Email)
                .EmailAddress()
                .WithMessage(ServiceResponseMessage.INVALID_ACCOUNT_EMAIL);
        }
    }
}