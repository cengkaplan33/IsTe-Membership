using FluentValidation;
using Membership.Core.Extension;
using Membership.Service.Models.Api.Request;
using Membership.Service.Resources;

namespace Membership.Service.Validation
{
    public class AccountAddressInsertValidator : AbstractValidator<InsertAccountAddressRequest>
    {
        public AccountAddressInsertValidator()
        {
            ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(address => address.AccountCode)
                .Must(m => !m.IsNullOrWhitespace())
                .WithMessage(ServiceResponseMessage.INVALID_ACCOUNT_CODE);

            RuleFor(address => address.Address)
                .Must(m => !m.IsNullOrWhitespace())
                .WithMessage(ServiceResponseMessage.INVALID_ACCOUNT_ADDRESS_ADDRESS);

            RuleFor(address => address.CountryId)
                .Must(m => m.IsNumericAndGreaterThenZero())
                .WithMessage(ServiceResponseMessage.INVALID_ACCOUNT_ADDRESS_COUNTRY_ID);

            RuleFor(address => address.CityId)
                .Must(m => m.IsNumericAndGreaterThenZero())
                .WithMessage(ServiceResponseMessage.INVALID_ACCOUNT_ADDRESS_CITY_ID);

            RuleFor(address => address.DistrictId)
                .Must(m => m.IsNumericAndGreaterThenZero())
                .WithMessage(ServiceResponseMessage.INVALID_ACCOUNT_ADDRESS_DISTRICT_ID);
        }
    }

    public class AccountAddressUpdateValidator : AbstractValidator<UpdateAccountAddressRequest>
    {
        public AccountAddressUpdateValidator()
        {
            ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(address => address.AddressCode)
                .Must(m => !m.IsNullOrWhitespace())
                .WithMessage(ServiceResponseMessage.INVALID_ADDRESS_CODE);

            RuleFor(address => address.Address)
                .Must(m => !m.IsNullOrWhitespace())
                .WithMessage(ServiceResponseMessage.INVALID_ACCOUNT_ADDRESS_ADDRESS);

            RuleFor(address => address.CountryId)
                .Must(m => m.IsNumericAndGreaterThenZero())
                .WithMessage(ServiceResponseMessage.INVALID_ACCOUNT_ADDRESS_COUNTRY_ID);

            RuleFor(address => address.CityId)
                .Must(m => m.IsNumericAndGreaterThenZero())
                .WithMessage(ServiceResponseMessage.INVALID_ACCOUNT_ADDRESS_CITY_ID);

            RuleFor(address => address.DistrictId)
                .Must(m => m.IsNumericAndGreaterThenZero())
                .WithMessage(ServiceResponseMessage.INVALID_ACCOUNT_ADDRESS_DISTRICT_ID);
        }
    }
}