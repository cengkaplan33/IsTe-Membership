using Membership.Service.Models;
using Membership.Service.Resources;

namespace Membership.Service.Api
{
    public class ApiHelper
    {
        public static ResponseBase BuildBaseResponse(string status, string code, string message = "")
        {
            return new ResponseBase
            {
                Status = status,
                Code = code,
                Message = message
            };
        }

        public static ResponseBase BuildInputErrorResponse(string message)
        {
            return BuildBaseResponse(ServiceResponse.SERVICE_STATUS_INPUT_ERROR,
                ServiceResponse.INVALID_INPUT_ERROR, message);
        }
    }
}