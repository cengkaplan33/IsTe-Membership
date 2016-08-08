using System.Net.Http;
using System.Web;
using Autofac;

namespace Membership.Api.DependecyResolution
{
    /// <summary>
    ///     Servis isteklerinin takip edilebilmesi için HttpContextBase sınıfından obje yaratır. 
    ///     IRequestContext sınıfını implemente eden sınıflar tarafından kullanılır.   
    /// </summary>    
    public class ApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApiRequestContext>();

            builder.Register(c =>
                HttpContext.Current != null
                    ? new HttpContextWrapper(HttpContext.Current)
                    : c.Resolve<HttpRequestMessage>().Properties["MS_HttpContext"])
                .As<HttpContextBase>();
        }
    }
}