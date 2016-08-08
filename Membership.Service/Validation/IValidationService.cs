using System;

namespace Membership.Service.Validation
{
    public interface IValidationService
    {
        ValidationResponse Validate(Type type, dynamic request);
    }
}