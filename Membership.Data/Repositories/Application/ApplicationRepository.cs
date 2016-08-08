using System.Collections.Generic;
using System.Linq;
using Membership.Core.Domain.Application;
using Membership.Core.Enum;

namespace Membership.Data.Repositories.Application
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly IRepository<Core.Domain.Application.Application> _applicationRepository;
        private readonly IRepository<ApplicationVertificationModel> _applicationVertificationModelRepository;
        private readonly IRepository<Core.Domain.VertificationModel.VertificationModel> _vertificationModelRepository;

        public ApplicationRepository(IRepository<Core.Domain.Application.Application> applicationRepository,
            IRepository<ApplicationVertificationModel> applicationVertificationModelRepository,
            IRepository<Core.Domain.VertificationModel.VertificationModel> vertificationModelRepository)
        {
            _applicationRepository = applicationRepository;
            _applicationVertificationModelRepository = applicationVertificationModelRepository;
            _vertificationModelRepository = vertificationModelRepository;
        }

        public Core.Domain.Application.Application GetApplicationById(int applicationId)
        {
            var rowApplication = _applicationRepository.FindOne(m => m.Id == applicationId
                && m.IsDeleted == (byte)GeneralEnum.IsDeleted.No);

            return rowApplication;
        }

        public Core.Domain.Application.Application GetAApplicationByCode(string applicationCode)
        {
            var rowApplication = _applicationRepository.FindOne(m => m.ApplicationCode == applicationCode
                && m.IsDeleted == (byte)GeneralEnum.IsDeleted.No);

            return rowApplication;
        }

        public List<Core.Domain.VertificationModel.VertificationModel> GetVertificationModelsByApplicationId(
            int applicationId)
        {
            var recordSetVertificationModel = (from vertificationModel in _vertificationModelRepository.FindAll()
                join applicationVertificationModel in _applicationVertificationModelRepository.FindAll()
                    on vertificationModel.Id equals applicationVertificationModel.VertificationModelId
                where applicationVertificationModel.ApplicationId == applicationId
                select vertificationModel).ToList();

            return recordSetVertificationModel;
        }
    }
}