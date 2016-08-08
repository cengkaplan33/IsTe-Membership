using AutoMapper;
using Membership.Core;
using Membership.Core.Extension;
using Membership.Data.Repositories.Reseller;
using Membership.Service.Account;
using Membership.Service.Models.Api.Request;
using Membership.Service.Models.Api.Response;
using Membership.Service.Resources;
using Membership.Service.Security;
using Membership.Service.Validation;

namespace Membership.Service.Api
{
    public class ApiAccountService : IApiAccountService
    {
        private readonly IAccountService _accountService;
        private readonly IAccountAddressService _accountAddressService;
        private readonly IAccountContactService _accountContactService;
        private readonly IMapper _mapper;
        private readonly IValidationService _validationService;
        private readonly IEncryptionService _encryptionService;
        private readonly IResellerRepository _resellerRepository;

        public ApiAccountService(IMapper mapper,
            IAccountService accountService,
            IValidationService validationService,
            IEncryptionService encryptionService,
            IResellerRepository resellerRepository,
            IAccountAddressService accountAddressService,
            IAccountContactService accountContactService)
        {
            _mapper = mapper;
            _accountService = accountService;
            _validationService = validationService;
            _encryptionService = encryptionService;
            _resellerRepository = resellerRepository;
            _accountAddressService = accountAddressService;
            _accountContactService = accountContactService;
        }

        public  InsertAccountResponse InsertAccount(InsertAccountRequest request)
        {
            var validationResult = _validationService.Validate(typeof(AccountInsertValidator), request);

            if (!validationResult.IsValid)
                return new InsertAccountResponse
                {
                    Status = ServiceResponse.SERVICE_STATUS_INPUT_ERROR,
                    Code = ServiceResponse.INVALID_INPUT_ERROR,
                    Message = validationResult.ErrorMessage
                };

            var account = _mapper.Map<Core.Domain.Account.Account>(request);

            var isEmailAlreadyRegistered = _accountService.IsEmailExistInApplication(request.ApplicationId, request.Email);

            if (isEmailAlreadyRegistered)
                return new InsertAccountResponse
                {
                    Status = ServiceResponse.SERVICE_STATUS_FAILED,
                    Code = ServiceResponse.EMAIL_ALREADY_REGISTERED,
                    Message = ServiceResponseMessage.EMAIL_ALREADY_REGISTERED
                };

            account.RegistrationId = _encryptionService.GenerateUniqueNumericValue(Constants.RegistrationIdLength);
            account.AccountCode = _encryptionService.GenerateUniqueAlphaNumericValue(Constants.AccountCodeLength);
            account.Salt = _encryptionService.GenerateSaltKey();
            account.Password = _encryptionService.GenerateValueHash(account.Password, account.Salt);
            account.IdentityNo = _encryptionService.GenerateValueHash(account.IdentityNo, Constants.Key);

            if (!request.ResellerCode.IsNullOrWhitespace())
            {
                var reseller = _resellerRepository.GetResellerByResellerCode(request.ResellerCode);
                if (reseller?.Id != null) account.ResellerId = reseller.Id;
            }

            _accountService.SaveAccount(account);

            var resultResource = _mapper.Map<AccountResponse>(account);

            resultResource.ResellerCode = account.ResellerId.IsNumericAndGreaterThenZero()
                ? request.ResellerCode
                : string.Empty;

            return new InsertAccountResponse
            {
                Status = ServiceResponse.SERVICE_STATUS_SUCCESS,
                Code = ServiceResponse.RESOURCE_INSERTED,
                Message = ServiceResponseMessage.RESOURCE_INSERTED,
                Account = resultResource
            };
        }

        public UpdateAccountResponse UpdateAccount(UpdateAccountRequest request)
        {
            var validationResult = _validationService.Validate(typeof(AccountUpdateValidator), request);

            if (!validationResult.IsValid)
                return new UpdateAccountResponse
                {
                    Status = ServiceResponse.SERVICE_STATUS_INPUT_ERROR,
                    Code = ServiceResponse.INVALID_INPUT_ERROR,
                    Message = validationResult.ErrorMessage
                };

            var rowAccount = _accountService.GetAccountByAccountCode(request.AccountCode);

            if (rowAccount == null)
                return new UpdateAccountResponse
                {
                    Status = ServiceResponse.SERVICE_STATUS_FAILED,
                    Code = ServiceResponse.RESOURCE_NOT_FOUND,
                    Message = ServiceResponseMessage.RESOURCE_NOT_FOUND
                };
            
            rowAccount.Name = request.Name;
            rowAccount.Surname = request.Surname;
            rowAccount.IdentityNo = _encryptionService.GenerateValueHash(request.IdentityNo, Constants.Key);
            rowAccount.Gender = request.Gender;
            rowAccount.Gsm = request.Gsm;
            rowAccount.AccountType = request.AccountType;            
            rowAccount.AlternativeEmail = request.AlternativeEmail;
            rowAccount.RiskLevel = request.RiskLevel;

            _accountService.UpdateAccount(rowAccount);

            return new UpdateAccountResponse
            {
                Status = ServiceResponse.SERVICE_STATUS_SUCCESS,
                Code = ServiceResponse.RESOURCE_UPDATED,
                Message = ServiceResponseMessage.RESOURCE_UPDATED
            };
        }

        public DeleteAccountResponse DeleteAccount(DeleteAccountRequest request)
        {
            if (request.AccountCode.IsNullOrWhitespace())
                return new DeleteAccountResponse
                {
                    Status = ServiceResponse.SERVICE_STATUS_INPUT_ERROR,
                    Code = ServiceResponse.INVALID_INPUT_ERROR
                };

            var rowAccount = _accountService.GetAccountByAccountCode(request.AccountCode);

            if (rowAccount == null)
                return new DeleteAccountResponse
                {
                    Status = ServiceResponse.SERVICE_STATUS_FAILED,
                    Code = ServiceResponse.RESOURCE_NOT_FOUND,
                    Message = ServiceResponseMessage.RESOURCE_NOT_FOUND
                };

            _accountService.DeleteAccount(rowAccount);

            return new DeleteAccountResponse
            {
                Status = ServiceResponse.SERVICE_STATUS_SUCCESS,
                Code = ServiceResponse.RESOURCE_DELETED,
                Message = ServiceResponseMessage.RESOURCE_DELETED
            };
        }

        public GetAccountReponse GetAccountByAccountCode(GetAccountRequest request)
        {
            if (request.AccountCode.IsNullOrWhitespace())
                return new GetAccountReponse
                {
                    Status = ServiceResponse.SERVICE_STATUS_INPUT_ERROR,
                    Code = ServiceResponse.INVALID_INPUT_ERROR
                };

            var rowAccount = _accountService.GetAccountByAccountCode(request.AccountCode);

            if (rowAccount == null)
                return new GetAccountReponse
                {
                    Status = ServiceResponse.SERVICE_STATUS_FAILED,
                    Code = ServiceResponse.RESOURCE_NOT_FOUND,
                    Message = ServiceResponseMessage.RESOURCE_NOT_FOUND
                };

            var resultResource = _mapper.Map<AccountResponse>(rowAccount);

            if (rowAccount.ResellerId.IsNumericAndGreaterThenZero())
            {
                var reseller = _resellerRepository.GetResellerById(rowAccount.ResellerId);

                resultResource.ResellerCode = reseller != null
                    ? reseller.ResellerCode
                    : string.Empty;
            }

            return new GetAccountReponse
            {
                Status = ServiceResponse.SERVICE_STATUS_SUCCESS,
                Code = ServiceResponse.SERVICE_STATUS_SUCCESS,
                Account = resultResource
            };
        }

        public InsertAccountAddressResponse InsertAccountAddress(InsertAccountAddressRequest request)
        {
            var validationResult = _validationService.Validate(typeof(AccountAddressInsertValidator), request);

            if (!validationResult.IsValid)
                return new InsertAccountAddressResponse
                {
                    Status = ServiceResponse.SERVICE_STATUS_INPUT_ERROR,
                    Code = ServiceResponse.INVALID_INPUT_ERROR,
                    Message = validationResult.ErrorMessage
                };

            var accountAddress = _mapper.Map<Core.Domain.Account.AccountAddress>(request);

            var account = _accountService.GetAccountByAccountCode(request.AccountCode);

            if (account?.Id != null) accountAddress.AccountId = account.Id;

            accountAddress.AddressCode = _encryptionService.GenerateUniqueAlphaNumericValue(Constants.AccountCodeLength);

            accountAddress = _accountAddressService.SaveAccountAddress(accountAddress);

            var resultResource = _mapper.Map<AccountAddressResponse>(accountAddress);

            resultResource.AccountCode = account?.AccountCode;

            return new InsertAccountAddressResponse
            {
                Status = ServiceResponse.SERVICE_STATUS_SUCCESS,
                Code = ServiceResponse.RESOURCE_INSERTED,
                Message = ServiceResponseMessage.RESOURCE_INSERTED,
                AccountAddress = resultResource
            };
        }

        public UpdateAccountAddressResponse UpdateAccountAddress(UpdateAccountAddressRequest request)
        {
            var validationResult = _validationService.Validate(typeof(AccountAddressUpdateValidator), request);

            if (!validationResult.IsValid)
                return new UpdateAccountAddressResponse
                {
                    Status = ServiceResponse.SERVICE_STATUS_INPUT_ERROR,
                    Code = ServiceResponse.INVALID_INPUT_ERROR,
                    Message = validationResult.ErrorMessage
                };

            var rowAccountAddress = _accountAddressService.GetAccountAddressByAccountAddressCode(request.AddressCode);

            if (rowAccountAddress == null)
                return new UpdateAccountAddressResponse
                {
                    Status = ServiceResponse.SERVICE_STATUS_FAILED,
                    Code = ServiceResponse.RESOURCE_NOT_FOUND,
                    Message = ServiceResponseMessage.RESOURCE_NOT_FOUND
                };

            rowAccountAddress.AddressTitle = request.AddressTitle;
            rowAccountAddress.Address = request.Address;
            rowAccountAddress.AddressType = request.AddressType;
            rowAccountAddress.CityId = request.CityId;
            rowAccountAddress.CountryId = request.CountryId;
            rowAccountAddress.DistrictId = request.DistrictId;
            rowAccountAddress.CompanyName = request.CompanyName;
            rowAccountAddress.TaxNumber = request.TaxNumber;
            rowAccountAddress.TaxOffice = request.TaxOffice;
            rowAccountAddress.ZipCode = request.ZipCode;

            _accountAddressService.UpdateAccountAddress(rowAccountAddress);

            return new UpdateAccountAddressResponse
            {
                Status = ServiceResponse.SERVICE_STATUS_SUCCESS,
                Code = ServiceResponse.RESOURCE_UPDATED,
                Message = ServiceResponseMessage.RESOURCE_UPDATED
            };
        }

        public DeleteAccountAddressResponse DeleteAccountAddress(DeleteAccountAddressRequest request)
        {
            if (request.AccountCode.IsNullOrWhitespace() || request.AddressCode.IsNullOrWhitespace())
                return new DeleteAccountAddressResponse
                {
                    Status = ServiceResponse.SERVICE_STATUS_INPUT_ERROR,
                    Code = ServiceResponse.INVALID_INPUT_ERROR
                };

            var rowAccountAddress = _accountAddressService.GetAccountAddressByAccountAddressCode(request.AddressCode);

            if (rowAccountAddress == null)
                return new DeleteAccountAddressResponse
                {
                    Status = ServiceResponse.SERVICE_STATUS_FAILED,
                    Code = ServiceResponse.RESOURCE_NOT_FOUND,
                    Message = ServiceResponseMessage.RESOURCE_NOT_FOUND
                };

            _accountAddressService.DeleteAccountAddress(rowAccountAddress);

            return new DeleteAccountAddressResponse
            {
                Status = ServiceResponse.SERVICE_STATUS_SUCCESS,
                Code = ServiceResponse.RESOURCE_DELETED,
                Message = ServiceResponseMessage.RESOURCE_DELETED
            };
        }

        public GetAccountAddressResponse GetAccountAddressByAddressCode(GetAccountAddressRequest request)
        {
            if (request.AccountCode.IsNullOrWhitespace() || request.AddressCode.IsNullOrWhitespace())
                return new GetAccountAddressResponse
                {
                    Status = ServiceResponse.SERVICE_STATUS_INPUT_ERROR,
                    Code = ServiceResponse.INVALID_INPUT_ERROR,
                };

            var rowAccountAddress = _accountAddressService.GetAccountAddressByAccountAddressCode(request.AddressCode);

            if (rowAccountAddress == null)
                return new GetAccountAddressResponse
                {
                    Status = ServiceResponse.SERVICE_STATUS_FAILED,
                    Code = ServiceResponse.RESOURCE_NOT_FOUND,
                    Message = ServiceResponseMessage.RESOURCE_NOT_FOUND
                };

            var resultResource = _mapper.Map<AccountAddressResponse>(rowAccountAddress);

            var rowAccount = _accountService.GetAccountById(rowAccountAddress.AccountId);

            resultResource.AccountCode = rowAccount != null
                ? rowAccount.AccountCode
                : string.Empty;

            return new GetAccountAddressResponse
            {
                Status = ServiceResponse.SERVICE_STATUS_SUCCESS,
                Code = ServiceResponse.SERVICE_STATUS_SUCCESS,
                AccountAddress = resultResource
            };
        }

        public InsertAccountContactResponse InsertAccountContact(InsertAccountContactRequest request)
        {
            var validationResult = _validationService.Validate(typeof(AccountContactInsertValidator), request);

            if (!validationResult.IsValid)
                return new InsertAccountContactResponse
                {
                    Status = ServiceResponse.SERVICE_STATUS_INPUT_ERROR,
                    Code = ServiceResponse.INVALID_INPUT_ERROR,
                    Message = validationResult.ErrorMessage
                };

            var accountContact = _mapper.Map<Core.Domain.Account.AccountContact>(request);

            var account = _accountService.GetAccountByAccountCode(request.AccountCode);

            if (account?.Id != null) accountContact.AccountId = account.Id;

            accountContact.ContactCode = _encryptionService.GenerateUniqueAlphaNumericValue(Constants.AccountCodeLength);

            accountContact = _accountContactService.SaveAccountContact(accountContact);

            var resultResource = _mapper.Map<AccountContactResponse>(accountContact);

            resultResource.AccountCode = account?.AccountCode;

            return new InsertAccountContactResponse
            {
                Status = ServiceResponse.SERVICE_STATUS_SUCCESS,
                Code = ServiceResponse.RESOURCE_INSERTED,
                Message = ServiceResponseMessage.RESOURCE_INSERTED,
                AccountContact = resultResource
            };
        }

        public UpdateAccountContactResponse UpdateAccountContact(UpdateAccountContactRequest request)
        {
            var validationResult = _validationService.Validate(typeof(AccountContactUpdateValidator), request);

            if (!validationResult.IsValid)
                return new UpdateAccountContactResponse
                {
                    Status = ServiceResponse.SERVICE_STATUS_INPUT_ERROR,
                    Code = ServiceResponse.INVALID_INPUT_ERROR,
                    Message = validationResult.ErrorMessage
                };

            var rowAccountContact = _accountContactService.GetAccountContactByAccountContactCode(request.ContactCode);

            if (rowAccountContact == null)
                return new UpdateAccountContactResponse
                {
                    Status = ServiceResponse.SERVICE_STATUS_FAILED,
                    Code = ServiceResponse.RESOURCE_NOT_FOUND,
                    Message = ServiceResponseMessage.RESOURCE_NOT_FOUND
                };

            rowAccountContact.ContactTitle = request.ContactTitle;
            rowAccountContact.ContactName = request.ContactName;
            rowAccountContact.Phone = request.Phone;
            rowAccountContact.Extension = request.Extension;
            rowAccountContact.Fax = request.Fax;
            rowAccountContact.Gsm = request.Gsm;
            rowAccountContact.Email = request.Email;
            rowAccountContact.WebSite = request.WebSite;

            _accountContactService.UpdateAccountContact(rowAccountContact);

            return new UpdateAccountContactResponse
            {
                Status = ServiceResponse.SERVICE_STATUS_SUCCESS,
                Code = ServiceResponse.RESOURCE_UPDATED,
                Message = ServiceResponseMessage.RESOURCE_UPDATED
            };
        }

        public DeleteAccountContactResponse DeleteAccountContact(DeleteAccountContactRequest request)
        {
            if (request.AccountCode.IsNullOrWhitespace() || request.ContactCode.IsNullOrWhitespace())
                return new DeleteAccountContactResponse
                {
                    Status = ServiceResponse.SERVICE_STATUS_INPUT_ERROR,
                    Code = ServiceResponse.INVALID_INPUT_ERROR
                };

            var rowAccountContact = _accountContactService.GetAccountContactByAccountContactCode(request.ContactCode);

            if (rowAccountContact == null)
                return new DeleteAccountContactResponse
                {
                    Status = ServiceResponse.SERVICE_STATUS_FAILED,
                    Code = ServiceResponse.RESOURCE_NOT_FOUND,
                    Message = ServiceResponseMessage.RESOURCE_NOT_FOUND
                };

            _accountContactService.DeleteAccountContact(rowAccountContact);

            return new DeleteAccountContactResponse
            {
                Status = ServiceResponse.SERVICE_STATUS_SUCCESS,
                Code = ServiceResponse.RESOURCE_DELETED,
                Message = ServiceResponseMessage.RESOURCE_DELETED
            };
        }

        public GetAccountContactResponse GetAccountContactByContactCode(GetAccountContactRequest request)
        {
            if (request.AccountCode.IsNullOrWhitespace() || request.ContactCode.IsNullOrWhitespace())
                return new GetAccountContactResponse
                {
                    Status = ServiceResponse.SERVICE_STATUS_INPUT_ERROR,
                    Code = ServiceResponse.INVALID_INPUT_ERROR,
                };

            var rowAccountContact = _accountContactService.GetAccountContactByAccountContactCode(request.ContactCode);

            if (rowAccountContact == null)
                return new GetAccountContactResponse
                {
                    Status = ServiceResponse.SERVICE_STATUS_FAILED,
                    Code = ServiceResponse.RESOURCE_NOT_FOUND,
                    Message = ServiceResponseMessage.RESOURCE_NOT_FOUND
                };

            var resultResource = _mapper.Map<AccountContactResponse>(rowAccountContact);

            var rowAccount = _accountService.GetAccountById(rowAccountContact.AccountId);

            resultResource.AccountCode = rowAccount != null
                ? rowAccount.AccountCode
                : string.Empty;

            return new GetAccountContactResponse
            {
                Status = ServiceResponse.SERVICE_STATUS_SUCCESS,
                Code = ServiceResponse.SERVICE_STATUS_SUCCESS,
                AccountContact = resultResource
            };
        }
    }
}