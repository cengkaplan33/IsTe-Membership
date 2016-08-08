using System;
using System.Web.Http;
using AutoMapper;
using Membership.Api.Attribute;
using Membership.Api.Models;
using Membership.Api.Models.Account;
using Membership.Api.Models.Authentication;
using Membership.Core.Extension;
using Membership.Service.Account;
using Membership.Service.Api;
using Membership.Service.Models.Account;
using Membership.Service.Models.Api.Request;
using Membership.Service.Models.Authentication;
using Membership.Service.Resources;
using Membership.Service.Security;

namespace Membership.Api.Controllers
{
    [TokenValidation]
    public class AccountController : ApiController
    {
        private readonly IAccountRegistrationService _accountRegistrationService;
        private readonly IApiAccountService _apiAccountService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly ApiRequestContext _requestContext;

        public AccountController(
            IMapper mapper,
            ApiRequestContext requestContext,
            IApiAccountService apiAccountService,
            IAccountRegistrationService accountRegistrationService,
            IAuthenticationService authenticationService)
        {
            _mapper = mapper;
            _requestContext = requestContext;
            _apiAccountService = apiAccountService;
            _accountRegistrationService = accountRegistrationService;
            _authenticationService = authenticationService;
        }

        #region Account

        [HttpPost]
        [Route("accounts")]
        public IHttpActionResult AccountCreate(InsertAccountRequestModel request)
        {
            if (request == null)
                return Ok(ApiHelper.BuildBaseResponse(ServiceResponse.SERVICE_STATUS_INPUT_ERROR,
                    ServiceResponse.INVALID_ACCOUNT_CREATE_REQUEST,
                    ServiceResponseMessage.INVALID_ACCOUNT_CREATE_REQUEST));

            var serviceRequest = _mapper.Map<InsertAccountRequest>(request);
            serviceRequest.ApplicationId = _requestContext.CurrentApplication.Id;

            return Ok(_apiAccountService.InsertAccount(serviceRequest));
        }

        [HttpGet]
        [Route("accounts/{accountCode?}")]
        public IHttpActionResult GetAccount(string accountCode = null)
        {
            if (accountCode == null)
                return Ok(ApiHelper.BuildBaseResponse(ServiceResponse.SERVICE_STATUS_INPUT_ERROR,
                    ServiceResponse.INVALID_ACCOUNT_QUERY_REQUEST,
                    ServiceResponseMessage.INVALID_ACCOUNT_QUERY_REQUEST));

            var serviceRequest = new GetAccountRequest
            {
                ApplicationId = _requestContext.CurrentApplication.Id,
                AccountCode = accountCode
            };

            return Ok(_apiAccountService.GetAccountByAccountCode(serviceRequest));
        }

        [HttpPut]
        [Route("accounts/{accountCode?}")]
        public IHttpActionResult AccountUpdate(UpdateAccountRequestModel request, string accountCode = null)
        {
            if ((request == null) || accountCode.IsNullOrWhitespace())
                return Ok(ApiHelper.BuildBaseResponse(ServiceResponse.SERVICE_STATUS_INPUT_ERROR,
                    ServiceResponse.INVALID_ACCOUNT_UPDATE_REQUEST,
                    ServiceResponseMessage.INVALID_ACCOUNT_UPDATE_REQUEST));

            var serviceRequest = _mapper.Map<UpdateAccountRequest>(request);
            serviceRequest.ApplicationId = _requestContext.CurrentApplication.Id;

            return Ok(_apiAccountService.UpdateAccount(serviceRequest));
        }

        [HttpDelete]
        [Route("accounts/{accountCode?}")]
        public IHttpActionResult AccountDelete(string accountCode = null)
        {
            if (accountCode.IsNullOrWhitespace())
                return Ok(ApiHelper.BuildBaseResponse(ServiceResponse.SERVICE_STATUS_INPUT_ERROR,
                    ServiceResponse.INVALID_ACCOUNT_DELETE_REQUEST,
                    ServiceResponseMessage.INVALID_ACCOUNT_DELETE_REQUEST));

            var serviceRequest = new DeleteAccountRequest
            {
                ApplicationId = _requestContext.CurrentApplication.Id,
                AccountCode = accountCode
            };

            return Ok(_apiAccountService.DeleteAccount(serviceRequest));
        }

        #endregion

        #region AccountAddress

        [HttpPost]
        [Route("accounts/{accountCode}/addresses")]
        public IHttpActionResult AccountAddresCreate(InsertAccountAddressRequestModel request)
        {
            if (request == null)
                return Ok(ApiHelper.BuildBaseResponse(ServiceResponse.SERVICE_STATUS_INPUT_ERROR,
                    ServiceResponse.INVALID_ACCOUNT_ADDRESS_CREATE_REQUEST,
                    ServiceResponseMessage.INVALID_ACCOUNT_ADDRESS_CREATE_REQUEST));

            var serviceRequest = _mapper.Map<InsertAccountAddressRequest>(request);
            serviceRequest.ApplicationId = _requestContext.CurrentApplication.Id;

            return Ok(_apiAccountService.InsertAccountAddress(serviceRequest));
        }

        [HttpPut]
        [Route("accounts/{accountCode}/addresses/{addressCode?}")]
        public IHttpActionResult AccountAddresUpdate(UpdateAccountAddressRequestModel request, string addressCode = null)
        {
            if ((request == null) || addressCode.IsNullOrWhitespace())
                return Ok(ApiHelper.BuildBaseResponse(ServiceResponse.SERVICE_STATUS_INPUT_ERROR,
                    ServiceResponse.INVALID_ACCOUNT_ADDRESS_UPDATE_REQUEST,
                    ServiceResponseMessage.INVALID_ACCOUNT_ADDRESS_UPDATE_REQUEST));

            var serviceRequest = _mapper.Map<UpdateAccountAddressRequest>(request);
            serviceRequest.ApplicationId = _requestContext.CurrentApplication.Id;

            return Ok(_apiAccountService.UpdateAccountAddress(serviceRequest));
        }

        [HttpDelete]
        [Route("accounts/{accountCode}/addresses/{addressCode?}")]
        public IHttpActionResult AccountAddressDelete(string accountCode, string addressCode = null)
        {
            if (addressCode.IsNullOrWhitespace())
                return Ok(ApiHelper.BuildBaseResponse(ServiceResponse.SERVICE_STATUS_INPUT_ERROR,
                    ServiceResponse.INVALID_ACCOUNT_ADDRESS_DELETE_REQUEST,
                    ServiceResponseMessage.INVALID_ACCOUNT_ADDRESS_DELETE_REQUEST));

            var serviceRequest = new DeleteAccountAddressRequest
            {
                ApplicationId = _requestContext.CurrentApplication.Id,
                AccountCode = accountCode,
                AddressCode = addressCode
            };

            return Ok(_apiAccountService.DeleteAccountAddress(serviceRequest));
        }

        [HttpGet]
        [Route("accounts/{accountCode}/addresses/{addressCode?}")]
        public IHttpActionResult GetAccountAddress(string accountCode, string addressCode = null)
        {
            if (accountCode == null)
                return Ok(ApiHelper.BuildBaseResponse(ServiceResponse.SERVICE_STATUS_INPUT_ERROR,
                    ServiceResponse.INVALID_ACCOUNT_ADDRESS_QUERY_REQUEST,
                    ServiceResponseMessage.INVALID_ACCOUNT_ADDRESS_QUERY_REQUEST));

            var serviceRequest = new GetAccountAddressRequest
            {
                ApplicationId = _requestContext.CurrentApplication.Id,
                AccountCode = accountCode,
                AddressCode = addressCode
            };

            return Ok(_apiAccountService.GetAccountAddressByAddressCode(serviceRequest));
        }

        #endregion

        #region AccountContact

        [HttpPost]
        [Route("accounts/{accountCode}/contacts")]
        public IHttpActionResult AccountContactCreate(InsertAccountContactRequestModel request)
        {
            if (request == null)
                return Ok(ApiHelper.BuildBaseResponse(ServiceResponse.SERVICE_STATUS_INPUT_ERROR,
                    ServiceResponse.INVALID_ACCOUNT_CONTACT_CREATE_REQUEST,
                    ServiceResponseMessage.INVALID_ACCOUNT_CONTACT_CREATE_REQUEST));

            var serviceRequest = _mapper.Map<InsertAccountContactRequest>(request);
            serviceRequest.ApplicationId = _requestContext.CurrentApplication.Id;

            return Ok(_apiAccountService.InsertAccountContact(serviceRequest));
        }

        [HttpPut]
        [Route("accounts/{accountCode}/contacts/{contactCode?}")]
        public IHttpActionResult AccountContactUpdate(UpdateAccountContactRequestModel request,
            string contactCode = null)
        {
            if ((request == null) || contactCode.IsNullOrWhitespace())
                return Ok(ApiHelper.BuildBaseResponse(ServiceResponse.SERVICE_STATUS_INPUT_ERROR,
                    ServiceResponse.INVALID_ACCOUNT_CONTACT_UPDATE_REQUEST,
                    ServiceResponseMessage.INVALID_ACCOUNT_CONTACT_UPDATE_REQUEST));

            var serviceRequest = _mapper.Map<UpdateAccountContactRequest>(request);
            serviceRequest.ApplicationId = _requestContext.CurrentApplication.Id;

            return Ok(_apiAccountService.UpdateAccountContact(serviceRequest));
        }

        [HttpDelete]
        [Route("accounts/{accountCode}/contacts/{contactCode?}")]
        public IHttpActionResult AccountContactDelete(string accountCode, string contactCode = null)
        {
            if (contactCode.IsNullOrWhitespace())
                return Ok(ApiHelper.BuildBaseResponse(ServiceResponse.SERVICE_STATUS_INPUT_ERROR,
                    ServiceResponse.INVALID_ACCOUNT_CONTACT_DELETE_REQUEST,
                    ServiceResponseMessage.INVALID_ACCOUNT_CONTACT_DELETE_REQUEST));

            var serviceRequest = new DeleteAccountContactRequest
            {
                ApplicationId = _requestContext.CurrentApplication.Id,
                AccountCode = accountCode,
                ContactCode = contactCode
            };

            return Ok(_apiAccountService.DeleteAccountContact(serviceRequest));
        }

        [HttpGet]
        [Route("accounts/{accountCode}/contacts/{contactCode?}")]
        public IHttpActionResult GetAccountContact(string accountCode, string contactCode = null)
        {
            if (contactCode == null)
                return Ok(ApiHelper.BuildBaseResponse(ServiceResponse.SERVICE_STATUS_INPUT_ERROR,
                    ServiceResponse.INVALID_ACCOUNT_CONTACT_QUERY_REQUEST,
                    ServiceResponseMessage.INVALID_ACCOUNT_CONTACT_QUERY_REQUEST));

            var serviceRequest = new GetAccountContactRequest
            {
                ApplicationId = _requestContext.CurrentApplication.Id,
                AccountCode = accountCode,
                ContactCode = contactCode
            };

            return Ok(_apiAccountService.GetAccountContactByContactCode(serviceRequest));
        }

        #endregion

        #region Auth

        [HttpPost]
        [Route("auth")]
        public IHttpActionResult Auth(AuthenticationRequestModel request)
        {
            if (request == null)
                return Ok(ApiHelper.BuildBaseResponse(ServiceResponse.SERVICE_STATUS_INPUT_ERROR,
                    ServiceResponse.INVALID_AUTH_REQUEST,
                    ServiceResponseMessage.INVALID_AUTH_REQUEST));

            var serviceRequest = _mapper.Map<AuthenticationRequest>(request);
            serviceRequest.ApplicationId = _requestContext.CurrentApplication.Id;

            var authResult = _authenticationService.SignIn(serviceRequest);

            return Ok(AccountResponseFactory.CreateAuthResponse(authResult));
        }

        [HttpPost]
        [Route("signup")]
        public IHttpActionResult Register()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("logout")]
        public IHttpActionResult Logout()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("account/changepassword")]
        public IHttpActionResult ChangePassword(ChangePasswordRequestModel request)
        {
            if (request == null)
                return Ok(ApiHelper.BuildBaseResponse(ServiceResponse.SERVICE_STATUS_INPUT_ERROR,
                    ServiceResponse.INVALID_PASSWORD_CHANGE_REQUEST,
                    ServiceResponseMessage.INVALID_PASSWORD_CHANGE_REQUEST));

            var serviceRequest = _mapper.Map<ChangePasswordRequest>(request);
            serviceRequest.ApplicationId = _requestContext.CurrentApplication.Id;

            var changePasswordResult = _accountRegistrationService.ChangePassword(serviceRequest);

            return Ok(AccountResponseFactory.CreateChangePasswordResponse(changePasswordResult));
        }

        [HttpPost]
        [Route("account/passwordrecovery")]
        public IHttpActionResult PasswordRecovery(RecoveryPasswordRequestModel request)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("account/block")]
        public IHttpActionResult Block(AccountBlockRequestModel request)
        {
            if (request == null)
                return Ok(ApiHelper.BuildBaseResponse(ServiceResponse.SERVICE_STATUS_INPUT_ERROR,
                    ServiceResponse.INVALID_ACCOUNT_BLOCK_REQUEST,
                    ServiceResponseMessage.INVALID_ACCOUNT_BLOCK_REQUEST));

            var serviceRequest = _mapper.Map<AccountBlockRequest>(request);
            serviceRequest.ApplicationId = _requestContext.CurrentApplication.Id;

            var accountBlockResult = _accountRegistrationService.Block(serviceRequest);

            return Ok(AccountResponseFactory.CreateBlockResponse(accountBlockResult));
        }

        [HttpPost]
        [Route("account/unblock")]
        public IHttpActionResult Unblock(AccountBlockRequestModel request)
        {
            if (request == null)
                return Ok(ApiHelper.BuildBaseResponse(ServiceResponse.SERVICE_STATUS_INPUT_ERROR,
                    ServiceResponse.INVALID_ACCOUNT_UNBLOCK_REQUEST,
                    ServiceResponseMessage.INVALID_ACCOUNT_UNBLOCK_REQUEST));

            var serviceRequest = _mapper.Map<AccountBlockRequest>(request);
            serviceRequest.ApplicationId = _requestContext.CurrentApplication.Id;

            var accountBlockResult = _accountRegistrationService.Unblock(serviceRequest);

            return Ok(AccountResponseFactory.CreateUnblockResponse(accountBlockResult));
        }

        [HttpPost]
        [Route("account/checkemail")]
        public IHttpActionResult CheckEmail(CheckEmailRequestModel request)
        {
            if (request == null)
                return Ok(ApiHelper.BuildBaseResponse(ServiceResponse.SERVICE_STATUS_INPUT_ERROR,
                    ServiceResponse.INVALID_CHECK_EMAIL_REQUEST,
                    ServiceResponseMessage.INVALID_CHECK_EMAIL_REQUEST));

            var serviceRequest = _mapper.Map<CheckEmailRequest>(request);
            serviceRequest.ApplicationId = _requestContext.CurrentApplication.Id;

            var checkEmailResult = _accountRegistrationService.CheckEmail(serviceRequest);

            return Ok(AccountResponseFactory.CreateCheckEmailResponse(checkEmailResult));
        }

        [HttpPost]
        [Route("account/facebookauth")]
        public IHttpActionResult FacebookAuth()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("account/twitterauth")]
        public IHttpActionResult TwitterAuth()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("account/googleauth")]
        public IHttpActionResult GoogleAuth()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}