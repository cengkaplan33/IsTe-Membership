using System.Reflection;
using System.Web;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using Membership.Api.DependecyResolution;
using Membership.Api.Mapping;
using Membership.DependecyResolution;
using Membership.Service.Mapping;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Membership.Api
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new ServiceModule());
            builder.RegisterModule(new RepositoryModule());
            builder.RegisterModule(new MappingModule());
            builder.RegisterModule(new ApiModule());

            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceMappingProfile());
                cfg.AddProfile(new ApiMappingProfile());
            });

            var mapper = mapperConfiguration.CreateMapper();

            builder.RegisterInstance(mapper).As<IMapper>();

            var config = GlobalConfiguration.Configuration;

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            var container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            GlobalConfiguration.Configure(WebApiConfig.Register);

            var formatters = GlobalConfiguration.Configuration.Formatters;

            formatters.Remove(formatters.XmlFormatter);

            var jsonFormatter = formatters.JsonFormatter;
            var settings = jsonFormatter.SerializerSettings;

            settings.Formatting = Formatting.Indented;

            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}