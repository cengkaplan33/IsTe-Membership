namespace Membership.Service.Reseller
{
    public interface IResellerService
    {
        Core.Domain.Reseller.Reseller GetResellerById(int resellerId);

        Core.Domain.Reseller.Reseller GetResellerByResellerCode(string resellerCode);
    }
}