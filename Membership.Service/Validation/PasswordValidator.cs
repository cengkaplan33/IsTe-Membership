using FluentValidation;
using Membership.Core;
using Membership.Core.Extension;
using Membership.Service.Models.Account;
using Membership.Service.Resources;

namespace Membership.Service.Validation
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordRequest>
    {
        public ChangePasswordValidator()
        {
            ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(password => password.OldPassword)
                .Must(m => !m.IsNullOrWhitespace())
                .WithMessage(ServiceResponseMessage.INVALID_PASSWORD);

            RuleFor(password => password.NewPassword)
                .Must(m => !m.IsNullOrWhitespace())
                .WithMessage(ServiceResponseMessage.INVALID_PASSWORD);

            RuleFor(password => password.OldPassword)
                .Must(PasswordValidator.IsPasswordValid)
                .WithMessage(ServiceResponseMessage.INVALID_PASSWORD);

            RuleFor(password => password.NewPassword)
                .Must(PasswordValidator.IsPasswordValid)
                .WithMessage(ServiceResponseMessage.INVALID_PASSWORD);

            RuleFor(password => password.OldPassword)
                .Must((password, oldPassword) => oldPassword != password.NewPassword)
                .WithMessage(ServiceResponseMessage.OLD_AND_NEW_PASSWORD_MUST_BE_DIFFERENT);
        }
    }

    public static class PasswordValidator
    {
        public static bool IsPasswordValid(string password)
        {
            if (password.IsNullOrWhitespace())
                return false;

            return password.Trim().Length >= Constants.PasswordMinLength &&
                   password.Trim().Length <= Constants.PasswordMaxLength;
        }

        public static bool IsPasswordMatch(string oldPassword, string newPassword)
        {
            return oldPassword == newPassword;
        }
    }
}