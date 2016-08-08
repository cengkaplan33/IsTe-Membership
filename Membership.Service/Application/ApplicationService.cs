using Membership.Data.Repositories.Application;

namespace Membership.Service.Application
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository _applicationRepository;

        public ApplicationService(IApplicationRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        public Core.Domain.Application.Application GetAApplicationByCode(string applicationCode)
        {
            return _applicationRepository.GetAApplicationByCode(applicationCode);
        }
    }
}