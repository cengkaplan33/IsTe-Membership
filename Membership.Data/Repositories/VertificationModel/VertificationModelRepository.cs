using System.Collections.Generic;
using System.Linq;
using Membership.Core.Enum;

namespace Membership.Data.Repositories.VertificationModel
{
    public class VertificationModelRepository : IVertificationModelRepository
    {
        private readonly IRepository<Core.Domain.VertificationModel.VertificationModel> _vertificationModelRepository;

        public VertificationModelRepository(
            IRepository<Core.Domain.VertificationModel.VertificationModel> vertificationModelRepository)
        {
            _vertificationModelRepository = vertificationModelRepository;
        }

        public List<Core.Domain.VertificationModel.VertificationModel> GetVertificationModels()
        {
            var recordSetVertificationModel =
                _vertificationModelRepository.Find(c => c.Status == (byte) GeneralEnum.Status.Active
                    && c.IsDeleted == (byte) GeneralEnum.IsDeleted.No).ToList();

            return recordSetVertificationModel;
        }
    }
}