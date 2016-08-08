using Autofac;
using Membership.Core.Domain.Account;
using Membership.Core.Domain.Application;
using Membership.Core.Domain.Cache;
using Membership.Core.Domain.Reseller;
using Membership.Core.Domain.Security;
using Membership.Core.Domain.VertificationModel;
using Membership.Data.Redis;
using Membership.Data.Redis.Account;
using Membership.Data.Redis.Cache;
using Membership.Data.Repositories;
using Membership.Data.Repositories.Account;
using Membership.Data.Repositories.Application;
using Membership.Data.Repositories.Reseller;
using Membership.Data.Repositories.Security;

namespace Membership.DependecyResolution
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            #region Repository Imp

            builder.RegisterType<ApplicationRepository>().As<IApplicationRepository>();
            builder.RegisterType<SecurityRepository>().As<ISecurityRepository>();
            builder.RegisterType<AccountRepository>().As<IAccountRepository>();
            builder.RegisterType<AccountAddressRepository>().As<IAccountAddressRepository>();
            builder.RegisterType<AccountContactRepository>().As<IAccountContactRepository>();
            builder.RegisterType<AccountIpRepository>().As<IAccountIpRepository>();
            builder.RegisterType<AccountIpRepository>().As<IAccountIpRepository>();
            builder.RegisterType<AccountLoginRepository>().As<IAccountLoginRepository>();
            builder.RegisterType<ResellerRepository>().As<IResellerRepository>();

            #endregion

            #region Domain

            builder.RegisterType<Repository<Application>>().As<IRepository<Application>>();
            builder.RegisterType<Repository<ApiUser>>().As<IRepository<ApiUser>>();
            builder.RegisterType<Repository<ApiUserIp>>().As<IRepository<ApiUserIp>>();
            builder.RegisterType<Repository<Account>>().As<IRepository<Account>>();
            builder.RegisterType<Repository<AccountAddress>>().As<IRepository<AccountAddress>>();
            builder.RegisterType<Repository<AccountContact>>().As<IRepository<AccountContact>>();
            builder.RegisterType<Repository<AccountPasswordChange>>().As<IRepository<AccountPasswordChange>>();
            builder.RegisterType<Repository<Reseller>>().As<IRepository<Reseller>>();            
            builder.RegisterType<Repository<ApplicationVertificationModel>>().As<IRepository<ApplicationVertificationModel>>();
            builder.RegisterType<Repository<VertificationModel>>().As<IRepository<VertificationModel>>();
            builder.RegisterType<Repository<AccountIp>>().As<IRepository<AccountIp>>();
            builder.RegisterType<Repository<AccountLoginLog>>().As<IRepository<AccountLoginLog>>();
            builder.RegisterType<Repository<AccountLoginAttempt>>().As<IRepository<AccountLoginAttempt>>();

            #endregion

            #region Redis

            builder.RegisterType<ApplicationCacheRepository>().As<IApplicationCacheRepository>();
            builder.RegisterType<AccountCacheRepository>().As<IAccountCacheRepository>();

            builder.RegisterType<RedisRepository<Token>>().As<IRedisRepository<Token>>();
            builder.RegisterType<RedisRepository<AccountCache>>().As<IRedisRepository<AccountCache>>();

            #endregion
        }
    }
}