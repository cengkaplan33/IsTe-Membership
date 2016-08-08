namespace Membership.Service.Models.Security
{
    public enum ApiKeyAuthenticationResult
    {
        /// <summary>
        ///     Giriş başarılı
        /// </summary>
        Successful = 1,

        /// <summary>
        ///     ApiKey mevcut değil
        /// </summary>
        ApiKeyNotExist = 2,

        /// <summary>
        ///     ApiKey aktif değil
        /// </summary>
        NotActive = 3,

        /// <summary>
        ///     Geçersiz ip'den istek geliyor
        /// </summary>
        InvalidIp = 4       
    }
}