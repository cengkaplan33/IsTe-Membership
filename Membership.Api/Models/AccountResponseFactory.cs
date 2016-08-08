using Membership.Service.Api;
using Membership.Service.Models;
using Membership.Service.Models.Account;
using Membership.Service.Models.Authentication;
using Membership.Service.Resources;

namespace Membership.Api.Models
{
    public static class AccountResponseFactory
    {
        public static ResponseBase CreateCheckEmailResponse(CheckEmailResult result)
        {
            return ApiHelper.BuildBaseResponse(
                result.IsSuccess
                    ? ServiceResponse.SERVICE_STATUS_SUCCESS
                    : ServiceResponse.SERVICE_STATUS_FAILED
                , result.IsSuccess
                    ? result.IsEmailSuitable
                        ? ServiceResponse.EMAIL_IS_SUITABLE
                        : ServiceResponse.EMAIL_ALREADY_REGISTERED
                    : ServiceResponse.SERVICE_STATUS_INPUT_ERROR
                , result.Message);
        }

        public static ResponseBase CreateAuthResponse(AuthenticationResult result)
        {
            switch (result.Status)
            {
                case AuthenticationStatus.Successful:
                    return ApiHelper.BuildBaseResponse(ServiceResponse.SERVICE_STATUS_SUCCESS,
                        ServiceResponse.SERVICE_STATUS_SUCCESS);
                case AuthenticationStatus.InvalidEmail:
                    return ApiHelper.BuildBaseResponse(ServiceResponse.SERVICE_STATUS_FAILED,
                        ServiceResponse.ACCOUNT_EMAIL_NOT_FOUND, result.Message);
                case AuthenticationStatus.InvalidPassword:
                    return ApiHelper.BuildBaseResponse(ServiceResponse.SERVICE_STATUS_FAILED,
                        ServiceResponse.WRONG_ACCOUNT_PASSWORD, result.Message);
                case AuthenticationStatus.AccountDisable:
                    return ApiHelper.BuildBaseResponse(ServiceResponse.SERVICE_STATUS_FAILED,
                        ServiceResponse.ACCOUNT_DISABLE, result.Message);
                case AuthenticationStatus.InvalidIp:
                    return ApiHelper.BuildBaseResponse(ServiceResponse.SERVICE_STATUS_FAILED,
                        ServiceResponse.INVALID_IP_ADDRESS, result.Message);
                default:
                    return ApiHelper.BuildBaseResponse(ServiceResponse.SERVICE_STATUS_FAILED,
                        ServiceResponse.ACCOUNT_EMAIL_NOT_FOUND, ServiceResponseMessage.ACCOUNT_EMAIL_NOT_FOUND);
            }
        }

        public static ResponseBase CreateChangePasswordResponse(ChangePasswordResult result)
        {
            return ApiHelper.BuildBaseResponse(result.IsSuccess
                ? ServiceResponse.SERVICE_STATUS_SUCCESS
                : ServiceResponse.SERVICE_STATUS_FAILED
                , result.IsSuccess
                    ? ServiceResponse.PASSWORD_CHANGED
                    : ServiceResponse.PASSWORD_NOT_CHANGED
                , result.IsSuccess
                    ? ServiceResponseMessage.PASSWORD_CHANGED
                    : result.Message);
        }

        public static ResponseBase CreateBlockResponse(AccountBlockResult result)
        {
            return ApiHelper.BuildBaseResponse(result.IsSuccess
                ? ServiceResponse.SERVICE_STATUS_SUCCESS
                : ServiceResponse.SERVICE_STATUS_FAILED
                , result.IsSuccess
                    ? ServiceResponse.ACCOUNT_BLOCKED
                    : ServiceResponse.SERVICE_STATUS_FAILED
                , result.IsSuccess
                    ? ServiceResponseMessage.ACCOUNT_BLOCKED
                    : result.Message);
        }

        public static ResponseBase CreateUnblockResponse(AccountBlockResult result)
        {
            return ApiHelper.BuildBaseResponse(result.IsSuccess
                ? ServiceResponse.SERVICE_STATUS_SUCCESS
                : ServiceResponse.SERVICE_STATUS_FAILED
                , result.IsSuccess
                    ? ServiceResponse.ACCOUNT_ACTIVATED
                    : ServiceResponse.SERVICE_STATUS_FAILED
                , result.IsSuccess
                    ? ServiceResponseMessage.ACCOUNT_ACTIVATED
                    : result.Message);
        }
    }
}