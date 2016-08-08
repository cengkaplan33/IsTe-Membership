namespace Membership.Service.Models.Security
{
    public enum TokenAuthenticationResult
    {
        /// <summary>
        ///     Giriş başarılı
        /// </summary>
        Successful = 1,
    
        /// <summary>
        ///     Geçersiz ip'den istek geliyor
        /// </summary>
        InvalidIp = 2,

        /// <summary>
        ///     Geçersiz token bilgisi
        /// </summary>
        InvalidToken = 3
    }
}