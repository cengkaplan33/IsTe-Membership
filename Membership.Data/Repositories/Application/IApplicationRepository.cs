using System.Collections.Generic;

namespace Membership.Data.Repositories.Application
{
    public interface IApplicationRepository
    {
        Core.Domain.Application.Application GetApplicationById(int applicationId);

        Core.Domain.Application.Application GetAApplicationByCode(string applicationCode);

        List<Core.Domain.VertificationModel.VertificationModel> GetVertificationModelsByApplicationId(int applicationId);
    }
}