using FluentValidation;
using Membership.Core.Extension;
using Membership.Service.Models.Api.Request;
using Membership.Service.Resources;

namespace Membership.Service.Validation
{
    public class AccountContactInsertValidator : AbstractValidator<InsertAccountContactRequest>
    {
        public AccountContactInsertValidator()
        {
            ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(contact => contact.AccountCode)
                .Must(m => !m.IsNullOrWhitespace())
                .WithMessage(ServiceResponseMessage.INVALID_ACCOUNT_CODE);

            RuleFor(contact => contact.ContactName)
                .Must(m => !m.IsNullOrWhitespace())
                .WithMessage(ServiceResponseMessage.INVALID_ACCOUNT_CONTACT_CONTACT_NAME);
        }
    }

    public class AccountContactUpdateValidator : AbstractValidator<UpdateAccountContactRequest>
    {
        public AccountContactUpdateValidator()
        {
            ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(contact => contact.ContactCode)
                .Must(m => !m.IsNullOrWhitespace())
                .WithMessage(ServiceResponseMessage.INVALID_CONTACT_CODE);

            RuleFor(contact => contact.ContactName)
                .Must(m => !m.IsNullOrWhitespace())
                .WithMessage(ServiceResponseMessage.INVALID_ACCOUNT_CONTACT_CONTACT_NAME);
        }
    }
}