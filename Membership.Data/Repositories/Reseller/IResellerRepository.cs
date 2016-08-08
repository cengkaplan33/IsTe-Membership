namespace Membership.Data.Repositories.Reseller
{
    public interface IResellerRepository
    {
        Core.Domain.Reseller.Reseller GetResellerById(int resellerId);

        Core.Domain.Reseller.Reseller GetResellerByResellerCode(string resellerCode);
    }
}