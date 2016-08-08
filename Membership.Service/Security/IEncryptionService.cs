namespace Membership.Service.Security
{
    public interface IEncryptionService
    {
        string GenerateSaltKey();

        string GenerateValueHash(string value, string saltkey);

        string GenerateUniqueNumericValue(int size);

        string GenerateUniqueAlphaNumericValue(int size);        
    }
}