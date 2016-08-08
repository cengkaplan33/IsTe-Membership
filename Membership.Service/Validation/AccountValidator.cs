using System.Text.RegularExpressions;
using FluentValidation;
using Membership.Core.Extension;
using Membership.Service.Models.Api.Request;
using Membership.Service.Resources;

namespace Membership.Service.Validation
{
    public class AccountInsertValidator : AbstractValidator<InsertAccountRequest>
    {
        public AccountInsertValidator()
        {
            ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(account => account.ApplicationId)
                .Must(m => m.IsNumericAndGreaterThenZero())
                .WithMessage(ServiceResponseMessage.INVALID_ACCOUNT_APPLICATION_ID);

            RuleFor(account => account.Email)
                .EmailAddress()
                .WithMessage(ServiceResponseMessage.INVALID_ACCOUNT_EMAIL);

            RuleFor(account => account.Password)
                .Must(PasswordValidator.IsPasswordValid)
                .WithMessage(ServiceResponseMessage.INVALID_ACCOUNT_PASSWORD);

            RuleFor(account => account.Name)
                .Must(AccountValidator.IsNameValid)
                .WithMessage(ServiceResponseMessage.INVALID_ACCOUNT_NAME);

            RuleFor(account => account.Surname)
                .Must(AccountValidator.IsNameValid)
                .WithMessage(ServiceResponseMessage.INVALID_ACCOUNT_SURNAME);

            RuleFor(account => account.IdentityNo)
                .Must(AccountValidator.ValidateIdentityNumber)
                .WithMessage(ServiceResponseMessage.INVALID_ACCOUNT_IDENTITY_NUMBER);

            RuleFor(account => account.Gender)
                .Must(AccountValidator.IsGenderValid)
                .WithMessage(ServiceResponseMessage.INVALID_ACCOUNT_GENDER);

            RuleFor(account => account.Gsm)
             .Must(AccountValidator.IsGsmValid)
             .WithMessage(ServiceResponseMessage.INVALID_ACCOUNT_GSM);

            RuleFor(account => account.RiskLevel)
                .Must(AccountValidator.IsRiskLevelValid)
                .WithMessage(ServiceResponseMessage.INVALID_ACCOUNT_RISK_LEVEL);

            RuleFor(account => account.AlternativeEmail)
               .Must(AccountValidator.ValidateEmail)
               .WithMessage(ServiceResponseMessage.INVALID_ACCOUNT_ALTERNATIVE_EMAIL);

            RuleFor(account => account.AccountType)
                .Must(AccountValidator.IsAccountTypeValid)
                .WithMessage(ServiceResponseMessage.INVALID_ACCOUNT_ACCOUNT_TYPE);

            RuleFor(account => account.Status)
                .Must(m => m.IsNumeric())
                .WithMessage(ServiceResponseMessage.INVALID_ACCOUNT_STATUS);
        }
    }

    public class AccountUpdateValidator : AbstractValidator<UpdateAccountRequest>
    {
        public AccountUpdateValidator()
        {
            ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(account => account.Name)
                .Must(AccountValidator.IsNameValid)
                .WithMessage(ServiceResponseMessage.INVALID_ACCOUNT_NAME);

            RuleFor(account => account.Surname)
                .Must(AccountValidator.IsNameValid)
                .WithMessage(ServiceResponseMessage.INVALID_ACCOUNT_SURNAME);

            RuleFor(account => account.IdentityNo)
                .Must(AccountValidator.ValidateIdentityNumber)
                .WithMessage(ServiceResponseMessage.INVALID_ACCOUNT_IDENTITY_NUMBER);

            RuleFor(account => account.Gender)
                .Must(AccountValidator.IsGenderValid)
                .WithMessage(ServiceResponseMessage.INVALID_ACCOUNT_GENDER);

            RuleFor(account => account.Gsm)
             .Must(AccountValidator.IsGsmValid)
             .WithMessage(ServiceResponseMessage.INVALID_ACCOUNT_GSM);

            RuleFor(account => account.RiskLevel)
              .Must(AccountValidator.IsRiskLevelValid)
              .WithMessage(ServiceResponseMessage.INVALID_ACCOUNT_RISK_LEVEL);

            RuleFor(account => account.AlternativeEmail)
             .Must(AccountValidator.ValidateEmail)
             .WithMessage(ServiceResponseMessage.INVALID_ACCOUNT_ALTERNATIVE_EMAIL);

            RuleFor(account => account.AccountType)
                 .Must(AccountValidator.IsAccountTypeValid)
                 .WithMessage(ServiceResponseMessage.INVALID_ACCOUNT_ACCOUNT_TYPE);

            RuleFor(account => account.Status)
                .Must(m => m.IsNumeric())
                .WithMessage(ServiceResponseMessage.INVALID_ACCOUNT_STATUS);
        }
    }

    public static class AccountValidator
    {
        public static bool IsNameValid(string name)
        {
            if (name.IsNullOrWhitespace())
                return false;

            return name.Trim().Length >= 2 && name.Trim().Length <= 25;
        }

        public static bool IsGenderValid(byte genderType)
        {
            return genderType == 1 || genderType == 2;
        }

        public static bool IsAccountTypeValid(int accountType)
        {
            return accountType == 1 || accountType == 2;
        }

        public static bool IsRiskLevelValid(int riskLevel)
        {
            return riskLevel == 1 || riskLevel == 2;
        }

        public static bool IsGsmValid(string gsm)
        {
            const string pattern = @"^(05)[0-9][0-9][1-9]([0-9]){6}$";

            return Regex.Match(gsm, pattern, RegexOptions.IgnoreCase).Success;
        }

        public static bool ValidateEmail(string email)
        {
            if (email.IsNullOrWhitespace()) return true;

            const string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";

            return Regex.Match(email, pattern, RegexOptions.IgnoreCase).Success;
        }

        public static bool ValidateIdentityNumber(string identityNumber)
        {
            if (identityNumber.Length != 11) return false;

            var tcNo = long.Parse(identityNumber);

            var atcno = tcNo / 100;
            var btcno = tcNo / 100;

            var c1 = atcno % 10;
            atcno = atcno / 10;
            var c2 = atcno % 10;
            atcno = atcno / 10;
            var c3 = atcno % 10;
            atcno = atcno / 10;
            var c4 = atcno % 10;
            atcno = atcno / 10;
            var c5 = atcno % 10;
            atcno = atcno / 10;
            var c6 = atcno % 10;
            atcno = atcno / 10;
            var c7 = atcno % 10;
            atcno = atcno / 10;
            var c8 = atcno % 10;
            atcno = atcno / 10;
            var c9 = atcno % 10;
            var q1 = (10 - ((c1 + c3 + c5 + c7 + c9) * 3 + c2 + c4 + c6 + c8) % 10) % 10;
            var q2 = (10 - ((c2 + c4 + c6 + c8 + q1) * 3 + c1 + c3 + c5 + c7 + c9) % 10) % 10;

            return btcno * 100 + q1 * 10 + q2 == tcNo;
        }
    }
}