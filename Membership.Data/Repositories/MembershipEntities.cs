using System.Data.Entity;
using Membership.Core.Domain.Account;
using Membership.Core.Domain.Address;
using Membership.Core.Domain.Application;
using Membership.Core.Domain.Customer;
using Membership.Core.Domain.Employee;
using Membership.Core.Domain.RecoveryQuestion;
using Membership.Core.Domain.Reseller;
using Membership.Core.Domain.Right;
using Membership.Core.Domain.Role;
using Membership.Core.Domain.Security;

namespace Membership.Data.Repositories
{
    public class MembershipEntities : DbContext
    {
        public MembershipEntities() : base("Membership")
        {
            Database.SetInitializer(new NullDatabaseInitializer<DbContext>());
        }

        public virtual DbSet<Core.Domain.Account.Account> Account { get; set; }

        public virtual DbSet<AccountAddress> AccountAddress { get; set; }

        public virtual DbSet<AccountContact> AccountContact { get; set; }

        public virtual DbSet<AccountIp> AccountIp { get; set; }

        public virtual DbSet<AccountLoginAttempt> AccountLoginAttempt { get; set; }

        public virtual DbSet<AccountLoginLog> AccountLoginLog { get; set; }

        public virtual DbSet<AccountPasswordChange> AccountPasswordChange { get; set; }

        public virtual DbSet<AccountPasswordRecovery> AccountPasswordRecovery { get; set; }

        public virtual DbSet<AccountRecoveryQuestion> AccountRecoveryQuestion { get; set; }

        public virtual DbSet<AccountRole> AccountRole { get; set; }

        public virtual DbSet<AccountSecurityCode> AccountSecurityCode { get; set; }

        public virtual DbSet<AccountSocialUser> AccountSocialUser { get; set; }

        public virtual DbSet<ApiUser> ApiUser { get; set; }

        public virtual DbSet<ApiUserIp> ApiUserIp { get; set; }

        public virtual DbSet<Core.Domain.Application.Application> Application { get; set; }

        public virtual DbSet<ApplicationVertificationModel> ApplicationVertificationModel { get; set; }

        public virtual DbSet<City> City { get; set; }

        public virtual DbSet<Country> Country { get; set; }

        public virtual DbSet<Customer> Customer { get; set; }

        public virtual DbSet<CustomerUser> CustomerUser { get; set; }

        public virtual DbSet<Core.Domain.Directory.Directory> Directory { get; set; }

        public virtual DbSet<District> District { get; set; }

        public virtual DbSet<Employee> Employee { get; set; }

        public virtual DbSet<Core.Domain.Group.Group> Group { get; set; }

        public virtual DbSet<RecoveryQuestion> RecoveryQuestion { get; set; }

        public virtual DbSet<Core.Domain.Reseller.Reseller> Reseller { get; set; }

        public virtual DbSet<ResellerType> ResellerType { get; set; }

        public virtual DbSet<Core.Domain.Right.Right> Right { get; set; }

        public virtual DbSet<RightVertificationModel> RightVertificationModel { get; set; }

        public virtual DbSet<Core.Domain.Role.Role> Role { get; set; }

        public virtual DbSet<RoleRight> RoleRight { get; set; }

        public virtual DbSet<RoleVertificationModel> RoleVertificationModel { get; set; }

        public virtual DbSet<Core.Domain.VertificationModel.VertificationModel> VertificationModel { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}