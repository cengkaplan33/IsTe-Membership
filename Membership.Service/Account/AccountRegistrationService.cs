using System;
using AutoMapper;
using Membership.Core;
using Membership.Core.Domain.Account;
using Membership.Core.Enum;
using Membership.Service.Models.Account;
using Membership.Service.Resources;
using Membership.Service.Security;
using Membership.Service.Validation;

namespace Membership.Service.Account
{
    public class AccountRegistrationService : IAccountRegistrationService
    {
        private readonly IAccountService _accountService;
        private readonly IEncryptionService _encryptionService;
        private readonly IMapper _mapper;
        private readonly IValidationService _validationService;

        public AccountRegistrationService(IAccountService accountService,
            IEncryptionService encryptionService,
            IValidationService validationService,
            IMapper mapper)
        {
            _accountService = accountService;
            _encryptionService = encryptionService;
            _validationService = validationService;
            _mapper = mapper;
        }

        public AccountRegistrationResult RegisterAccount(AccountRegistrationRequest request)
        {
            var validationResult = _validationService.Validate(typeof (AccountInsertValidator), request);

            if (!validationResult.IsValid)
                return new AccountRegistrationResult
                {
                    Status = AccountRegistrationStatus.Invalid,
                    Message = validationResult.ErrorMessage
                };

            var account = _mapper.Map<Core.Domain.Account.Account>(request);

            account.RegistrationId = _encryptionService.GenerateUniqueNumericValue(Constants.RegistrationIdLength);
            account.AccountCode = _encryptionService.GenerateUniqueAlphaNumericValue(Constants.AccountCodeLength);
            account.Salt = _encryptionService.GenerateSaltKey();
            account.Password = _encryptionService.GenerateValueHash(account.Password, account.Salt);
            account.IdentityNo = _encryptionService.GenerateValueHash(account.IdentityNo, account.Salt);

            _accountService.SaveAccount(account);

            return new AccountRegistrationResult
            {
                Status = AccountRegistrationStatus.Successful
            };
        }

        public ChangePasswordResult ChangePassword(ChangePasswordRequest request)
        {
            var validationResult = _validationService.Validate(typeof (ChangePasswordValidator), request);

            if (!validationResult.IsValid)
                return new ChangePasswordResult
                {
                    IsSuccess = false,
                    Message = validationResult.ErrorMessage
                };

            var account = _accountService.GetAccountByAccountCode(request.AccountCode);

            if (account == null)
                return new ChangePasswordResult
                {
                    IsSuccess = false,
                    Message = ServiceResponseMessage.INVALID_ACCOUNT_CODE
                };

            var requestOldPasswordHash = _encryptionService.GenerateValueHash(request.OldPassword, account.Salt);

            if (account.Password != requestOldPasswordHash)
                return new ChangePasswordResult
                {
                    IsSuccess = false,
                    Message = ServiceResponseMessage.PASSWORD_NOT_MISMATCH
                };

            account.Salt = _encryptionService.GenerateSaltKey();
            account.Password = _encryptionService.GenerateValueHash(request.NewPassword, account.Salt);

            _accountService.UpdateAccount(account);

            _accountService.SavePasswordChange(new AccountPasswordChange
            {
                AccountId = account.Id,
                OldPassword = requestOldPasswordHash,
                NewPassword = account.Password
            });

            return new ChangePasswordResult
            {
                IsSuccess = true
            };
        }

        public RecoveryPasswordResult RecoveryPassword(RecoveryPasswordRequest request)
        {
            throw new NotImplementedException();
        }

        public AccountBlockResult Block(AccountBlockRequest request)
        {
            var validationResult = _validationService.Validate(typeof (AccountBlockValidator), request);

            if (!validationResult.IsValid)
                return new AccountBlockResult
                {
                    IsSuccess = false,
                    Message = validationResult.ErrorMessage
                };

            var account = _accountService.GetAccountByAccountCode(request.AccountCode);

            if (account == null)
                return new AccountBlockResult
                {
                    IsSuccess = false,
                    Message = ServiceResponseMessage.INVALID_ACCOUNT_CODE
                };

            account.Status = (byte) AccountEnum.AccountStatus.Passive;

            _accountService.UpdateAccount(account);

            return new AccountBlockResult
            {
                IsSuccess = true
            };
        }

        public AccountBlockResult Unblock(AccountBlockRequest request)
        {
            var validationResult = _validationService.Validate(typeof (AccountBlockValidator), request);

            if (!validationResult.IsValid)
                return new AccountBlockResult
                {
                    IsSuccess = false,
                    Message = validationResult.ErrorMessage
                };

            var account = _accountService.GetAccountByAccountCode(request.AccountCode);

            if (account == null)
                return new AccountBlockResult
                {
                    IsSuccess = false,
                    Message = ServiceResponseMessage.INVALID_ACCOUNT_CODE
                };

            account.Status = (byte) AccountEnum.AccountStatus.Active;

            _accountService.UpdateAccount(account);

            return new AccountBlockResult
            {
                IsSuccess = true
            };
        }

        public CheckEmailResult CheckEmail(CheckEmailRequest request)
        {
            var validationResult = _validationService.Validate(typeof (CheckEmailValidator), request);

            if (!validationResult.IsValid)
                return new CheckEmailResult
                {
                    IsEmailSuitable = false,
                    IsSuccess = false,
                    Message = validationResult.ErrorMessage
                };

            var result = _accountService.IsEmailExistInApplication(request.ApplicationId, request.Email);

            return new CheckEmailResult
            {
                IsEmailSuitable = !result,
                IsSuccess = true,
                Message = !result
                    ? ServiceResponseMessage.EMAIL_IS_SUITABLE
                    : ServiceResponseMessage.EMAIL_ALREADY_REGISTERED
            };
        }
    }
}