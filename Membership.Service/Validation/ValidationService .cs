using System;
using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Results;

namespace Membership.Service.Validation
{
    public class ValidationService : IValidationService
    {
        public ValidationResponse Validate(Type type, dynamic request)
        {
            var validator = Activator.CreateInstance(type) as IValidator;

            if (validator == null) return null;

            ValidationResult result = validator.Validate(request);

            var response = new ValidationResponse
            {
                IsValid = result.IsValid,
                ErrorMessage = result.IsValid
                    ? string.Empty
                    : result.Errors[0]?.ToString()
            };

            foreach (var item in result.Errors)
            {
                response.ErrorMessages.Add(item.ErrorMessage);
            }

            return response;
        }
    }

    public class ValidationResponse
    {
        public ValidationResponse()
        {
            ErrorMessages = new List<string>();
        }

        public bool IsValid { get; set; }

        public string ErrorMessage { get; set; }

        public List<string> ErrorMessages { get; set; }
    }
}