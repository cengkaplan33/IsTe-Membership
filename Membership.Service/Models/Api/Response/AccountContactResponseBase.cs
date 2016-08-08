namespace Membership.Service.Models.Api.Response
{
    public class AccountContactResponseBase
    {
        /// <summary>
        ///     Kontak kodu
        /// </summary>
        public string ContactCode { get; set; }

        /// <summary>
        ///     Kullanıcı kodu
        /// </summary>
        public string AccountCode { get; set; }

        /// <summary>
        ///     Kontak başlığı
        /// </summary>
        public string ContactTitle { get; set; }

        /// <summary>
        ///     Kontak adı
        /// </summary>
        public string ContactName { get; set; }

        /// <summary>
        ///     Telefon
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        ///     Dahili numara
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        ///     Faks
        /// </summary>
        public string Fax { get; set; }

        /// <summary>
        ///     Gsm
        /// </summary>
        public string Gsm { get; set; }

        /// <summary>
        ///     Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     Web site
        /// </summary>
        public string WebSite { get; set; }
    }
}