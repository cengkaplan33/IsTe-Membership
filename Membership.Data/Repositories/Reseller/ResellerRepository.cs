using Membership.Core.Enum;

namespace Membership.Data.Repositories.Reseller
{
    public class ResellerRepository : IResellerRepository
    {
        private readonly IRepository<Core.Domain.Reseller.Reseller> _resellerRepository;

        public ResellerRepository(IRepository<Core.Domain.Reseller.Reseller> resellerRepository)
        {
            _resellerRepository = resellerRepository;
        }

        public Core.Domain.Reseller.Reseller GetResellerById(int resellerId)
        {
            var rowReseller = _resellerRepository.FindOne(c => c.Id == resellerId
               && c.IsDeleted == (byte)GeneralEnum.IsDeleted.No);

            return rowReseller;
        }

        public Core.Domain.Reseller.Reseller GetResellerByResellerCode(string resellerCode)
        {
            var rowReseller = _resellerRepository.FindOne(c => c.ResellerCode == resellerCode
              && c.IsDeleted == (byte)GeneralEnum.IsDeleted.No);

            return rowReseller;
        }
    }
}