using Membership.Data.Repositories.Reseller;

namespace Membership.Service.Reseller
{
    public class ResellerService : IResellerService
    {
        private readonly IResellerRepository _resellerRepository;

        public ResellerService(IResellerRepository resellerRepository)
        {
            _resellerRepository = resellerRepository;
        }

        public Core.Domain.Reseller.Reseller GetResellerById(int resellerId)
        {
            return _resellerRepository.GetResellerById(resellerId);
        }

        public Core.Domain.Reseller.Reseller GetResellerByResellerCode(string resellerCode)
        {
            return _resellerRepository.GetResellerByResellerCode(resellerCode);
        }
    }
}