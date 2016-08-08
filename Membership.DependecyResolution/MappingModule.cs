using Autofac;
using AutoMapper;
using Membership.Service.Mapping;

namespace Membership.DependecyResolution
{
    public class MappingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ServiceMappingProfile>().As<Profile>();         
        }
    }
}