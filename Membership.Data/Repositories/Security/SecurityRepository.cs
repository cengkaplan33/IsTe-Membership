using System.Collections.Generic;
using Membership.Core.Domain.Security;
using Membership.Core.Enum;
using Membership.Core.Extension;
using Membership.Core.Helper;

namespace Membership.Data.Repositories.Security
{
    public class SecurityRepository : ISecurityRepository
    {
        private readonly IRepository<ApiUserIp> _apiUserIpRepository;
        private readonly IRepository<ApiUser> _apiUserRepository;

        public SecurityRepository(IRepository<ApiUser> apiUserRepository,
            IRepository<ApiUserIp> apiUserIpRepository)
        {
            _apiUserRepository = apiUserRepository;
            _apiUserIpRepository = apiUserIpRepository;
        }

        public ApiUser GetApiUserByApiKey(string apiKey, string secretKey)
        {
            if (apiKey.IsNullOrWhitespace())
                ExceptionHelper.ThrowIfNullOrEmpty(() => apiKey);

            if (secretKey.IsNullOrWhitespace())
                ExceptionHelper.ThrowIfNullOrEmpty(() => secretKey);

            var rowApiUser = _apiUserRepository.FindOne(a => a.ApiKey == apiKey && a.SecretKey == secretKey
                && a.IsDeleted == (byte) GeneralEnum.IsDeleted.No);

            return rowApiUser;
        }

        public List<ApiUserIp> GetApiUserIpList(ApiUser apiUser)
        {
            if (apiUser == null)
                ExceptionHelper.ThrowIfNull(() => apiUser);

            var recordSetApiUserIp = _apiUserIpRepository.Find(a => a.ApiUserId == apiUser.Id
                && a.IsDeleted == (byte) GeneralEnum.IsDeleted.No);

            return recordSetApiUserIp;
        }
    }
}