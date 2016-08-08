namespace Membership.Service.Models.Api.Request
{
    public class AccountAddressRequestBase
    {
        /// <summary>
        ///     Kullanıcı kodu
        /// </summary>
        public string AccountCode { get; set; }

        /// <summary>
        ///     Adres tipi
        /// </summary>
        public int AddressType { get; set; }

        /// <summary>
        ///     Adres başlığı
        /// </summary>
        public string AddressTitle { get; set; }

        /// <summary>
        ///     Ülke Id'si
        /// </summary>
        public int CountryId { get; set; }

        /// <summary>
        ///     Şehir Id'si
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        ///     Semt Id'si
        /// </summary>
        public int DistrictId { get; set; }

        /// <summary>
        ///     Açık adres
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        ///     Zip kodu
        /// </summary>
        public string ZipCode { get; set; }

        /// <summary>
        ///     Vergi dairesi
        /// </summary>
        public string TaxOffice { get; set; }

        /// <summary>
        ///     Vergi numarası
        /// </summary>
        public string TaxNumber { get; set; }

        /// <summary>
        ///     Firma unvanı
        /// </summary>
        public string CompanyName { get; set; }
    }
}