using System.Collections.Generic;
using Membership.Core.Domain.Security;

namespace Membership.Data.Repositories.Security
{
    public interface ISecurityRepository
    {
        ApiUser GetApiUserByApiKey(string apiKey, string secretKey);

        List<ApiUserIp> GetApiUserIpList(ApiUser apiUser);
    }
}