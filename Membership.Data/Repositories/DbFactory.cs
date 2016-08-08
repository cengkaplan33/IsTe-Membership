using System.Data.Entity.Core.Objects;
using System.Web;

namespace Membership.Data.Repositories
{
    public class DbFactory
    {
        private const string DbContextKey = "MEMBERSHIP_CONTEXT";

        public static MembershipEntities DbInstance
        {
            get
            {
                if (!HttpContext.Current.Items.Contains(DbContextKey))
                    HttpContext.Current.Items.Add(DbContextKey, new MembershipEntities());
                return HttpContext.Current.Items[DbContextKey] as MembershipEntities;
            }
        }

        public static void RemoveContext()
        {
            if (HttpContext.Current == null || HttpContext.Current.Items[DbContextKey] == null) return;
            ((ObjectContext)HttpContext.Current.Items[DbContextKey]).Dispose();
            HttpContext.Current.Items.Remove(DbContextKey);
        }
    }
}