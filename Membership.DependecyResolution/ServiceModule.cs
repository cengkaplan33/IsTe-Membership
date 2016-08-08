using Autofac;
using Membership.Core;
using Membership.Service.Account;
using Membership.Service.Api;
using Membership.Service.Application;
using Membership.Service.Cache;
using Membership.Service.Reseller;
using Membership.Service.Security;
using Membership.Service.Validation;

namespace Membership.DependecyResolution
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationService>().As<IApplicationService>();
            builder.RegisterType<AccountService>().As<IAccountService>();
            builder.RegisterType<AccountAddressService>().As<IAccountAddressService>();
            builder.RegisterType<AccountContactService>().As<IAccountContactService>();
            builder.RegisterType<AccountRegistrationService>().As<IAccountRegistrationService>();
            builder.RegisterType<ApiAuthService>().As<IApiAuthService>();
            builder.RegisterType<TokenService>().As<ITokenService>();
            builder.RegisterType<WebHelper>().As<IWebHelper>();
            builder.RegisterType<ValidationService>().As<IValidationService>();
            builder.RegisterType<ApiAccountService>().As<IApiAccountService>();
            builder.RegisterType<EncryptionService>().As<IEncryptionService>();
            builder.RegisterType<ResellerService>().As<IResellerService>();
            builder.RegisterType<AuthenticationService>().As<IAuthenticationService>();
            builder.RegisterType<CacheService>().As<ICacheService>();
        }
    }
}