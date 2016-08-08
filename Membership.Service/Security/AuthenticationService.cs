using System;
using System.Linq;
using Membership.Core;
using Membership.Core.Domain.Account;
using Membership.Core.Domain.Cache;
using Membership.Core.Enum;
using Membership.Core.Extension;
using Membership.Core.Helper;
using Membership.Data.Repositories.Account;
using Membership.Service.Cache;
using Membership.Service.Models.Authentication;
using Membership.Service.Resources;

namespace Membership.Service.Security
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAccountIpRepository _accountIpRepository;
        private readonly IAccountLoginRepository _accountLoginRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IEncryptionService _encryptionService;
        private readonly IWebHelper _webHelper;
        private readonly ICacheService _cacheService;

        public AuthenticationService(IAccountRepository accountRepository,
            IAccountIpRepository accountIpRepository,
            IEncryptionService encryptionService,
            IAccountLoginRepository accountLoginRepository,
            IWebHelper webHelper, 
            ICacheService cacheService)
        {
            _accountRepository = accountRepository;
            _accountIpRepository = accountIpRepository;
            _encryptionService = encryptionService;
            _accountLoginRepository = accountLoginRepository;
            _webHelper = webHelper;
            _cacheService = cacheService;
        }

        public AuthenticationResult SignIn(AuthenticationRequest request)
        {
            if (request.Email.IsNullOrWhitespace())
                ExceptionHelper.ThrowIfNullOrEmpty(() => request.Email);

            if (request.Password.IsNullOrWhitespace())
                ExceptionHelper.ThrowIfNullOrEmpty(() => request.Password);

            var account = _accountRepository.GetAccountByApplicationIdAndEmail(request.ApplicationId, request.Email);

            if (account == null)
            {
                InsertLoginAttempt(null, request.Email, (byte)AuthenticationStatus.InvalidEmail);

                return new AuthenticationResult
                {
                    Status = AuthenticationStatus.InvalidEmail,
                    Message = ServiceResponseMessage.INVALID_EMAIL
                };
            }

            if (account.Status == (byte)GeneralEnum.Status.Passive)
            {
                InsertLoginAttempt(account, request.Email, (byte)AuthenticationStatus.AccountDisable);

                return new AuthenticationResult
                {
                    Message = ServiceResponseMessage.ACCOUNT_DISABLE,
                    Status = AuthenticationStatus.AccountDisable
                };
            }

            var password = _encryptionService.GenerateValueHash(request.Password, account.Salt);

            if (account.Password != password)
            {
                InsertLoginAttempt(account, request.Email, (byte)AuthenticationStatus.InvalidPassword);

                return new AuthenticationResult
                {
                    Status = AuthenticationStatus.InvalidPassword,
                    Message = ServiceResponseMessage.INVALID_PASSWORD
                };
            }

            var accountIpList = _accountIpRepository.GetAccountIpList(account);

            if (accountIpList.Count == 0)
            {
                InsertLoginLog(account);

                return new AuthenticationResult
                {
                    Status = AuthenticationStatus.Successful
                };
            }

            if (accountIpList.All(accountIp => accountIp.Ip != request.RequestIp))
            {
                InsertLoginAttempt(account, request.Email, (byte)AuthenticationStatus.InvalidIp);

                return new AuthenticationResult
                {
                    Status = AuthenticationStatus.InvalidIp,
                    Message = ServiceResponseMessage.INVALID_IP_ADDRESS
                };
            }

            InsertLoginLog(account);

            return new AuthenticationResult
            {
                Status = AuthenticationStatus.Successful
            };
        }

        public LogoutResult Logout(LogoutRequest request)
        {
            throw new NotImplementedException();
        }

        private void InsertLoginLog(Core.Domain.Account.Account account)
        {
            account.LastLoginTime = DateTime.Now;
            _accountRepository.UpdateAccount(account);

            _accountLoginRepository.InsertLoginLog(new AccountLoginLog
            {
                AccountId = account.Id,
                Ip = _webHelper.GetCurrentIpAddress()
            });

            _cacheService.SetAccountCache(new AccountCache
            {
                AccountCode = account.AccountCode,
                LoginTime = DateTime.Now,
                LastRequestIp = _webHelper.GetCurrentIpAddress()
            });
        }

        private void InsertLoginAttempt(Core.Domain.Account.Account account, string email, byte status)
        {
            _accountLoginRepository.InsertLoginAttempt(new AccountLoginAttempt
            {
                AccountId = account?.Id ?? 0,
                Email = account?.Email ?? email,
                Ip = _webHelper.GetCurrentIpAddress(),
                Status = status
            });
        }
    }
}