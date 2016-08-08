using System.Collections.Generic;

namespace Membership.Data.Repositories.VertificationModel
{
    public interface IVertificationModelRepository
    {
        List<Core.Domain.VertificationModel.VertificationModel> GetVertificationModels();
    }
}