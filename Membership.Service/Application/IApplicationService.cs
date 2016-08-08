namespace Membership.Service.Application
{
    public interface IApplicationService
    {
        Core.Domain.Application.Application GetAApplicationByCode(string applicationCode);
    }
}