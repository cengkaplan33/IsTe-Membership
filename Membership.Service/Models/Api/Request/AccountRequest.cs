namespace Membership.Service.Models.Api.Request
{
    public class InsertAccountRequest : AccountRequestBase
    {
        /// <summary>
        ///     Uygulama Id'si
        /// </summary>
        public int ApplicationId { get; set; }

        /// <summary>
        ///     Durumu
        /// </summary>
        public byte Status { get; set; }
    }

    public class UpdateAccountRequest : AccountRequestBase
    {
        /// <summary>
        ///     Uygulama Id'si
        /// </summary>
        public int ApplicationId { get; set; }

        /// <summary>
        ///     Benzersiz kullanıcı kodu
        /// </summary>
        public string AccountCode { get; set; }

        /// <summary>
        ///     Durumu
        /// </summary>
        public byte Status { get; set; }
    }

    public class DeleteAccountRequest
    {
        /// <summary>
        ///     Uygulama Id'si
        /// </summary>
        public int ApplicationId { get; set; }

        /// <summary>
        ///     Benzersiz kullanıcı kodu
        /// </summary>
        public string AccountCode { get; set; }
    }

    public class GetAccountRequest : AccountRequestBase
    {
        /// <summary>
        ///     Uygulama Id'si
        /// </summary>
        public int ApplicationId { get; set; }

        /// <summary>
        ///     Benzersiz kullanıcı kodu
        /// </summary>
        public string AccountCode { get; set; }
    }


    public class InsertAccountAddressRequest : AccountAddressRequestBase
    {
        /// <summary>
        ///     Uygulama Id'si
        /// </summary>
        public int ApplicationId { get; set; }       
    }

    public class UpdateAccountAddressRequest : AccountAddressRequestBase
    {
        /// <summary>
        ///     Uygulama Id'si
        /// </summary>
        public int ApplicationId { get; set; }

        /// <summary>
        ///     Adres kodu
        /// </summary>
        public string AddressCode { get; set; }
    }

    public class DeleteAccountAddressRequest
    {
        /// <summary>
        ///     Uygulama Id'si
        /// </summary>
        public int ApplicationId { get; set; }

        /// <summary>
        ///     Benzersiz kullanıcı kodu
        /// </summary>
        public string AccountCode { get; set; }

        /// <summary>
        ///     Benzersiz kullanıcı adres kaydı kodu
        /// </summary>
        public string AddressCode { get; set; }
    }

    public class GetAccountAddressRequest : AccountAddressRequestBase
    {
        /// <summary>
        ///     Uygulama Id'si
        /// </summary>
        public int ApplicationId { get; set; }

        /// <summary>
        ///     Benzersiz kullanıcı adres kaydı kodu
        /// </summary>
        public string AddressCode { get; set; }
    }


    public class InsertAccountContactRequest : AccountContactRequestBase
    {
        /// <summary>
        ///     Uygulama Id'si
        /// </summary>
        public int ApplicationId { get; set; }
    }

    public class UpdateAccountContactRequest : AccountContactRequestBase
    {
        /// <summary>
        ///     Uygulama Id'si
        /// </summary>
        public int ApplicationId { get; set; }

        /// <summary>
        ///     Kontak kodu
        /// </summary>
        public string ContactCode { get; set; }

    }

    public class DeleteAccountContactRequest
    {
        /// <summary>
        ///     Uygulama Id'si
        /// </summary>
        public int ApplicationId { get; set; }

        /// <summary>
        ///     Benzersiz kullanıcı kodu
        /// </summary>
        public string AccountCode { get; set; }

        /// <summary>
        ///     Benzersiz kullanıcı kontak kaydı kodu
        /// </summary>
        public string ContactCode { get; set; }
    }

    public class GetAccountContactRequest : AccountContactRequestBase
    {
        /// <summary>
        ///     Uygulama Id'si
        /// </summary>
        public int ApplicationId { get; set; }      

        /// <summary>
        ///     Benzersiz kullanıcı kontak kaydı kodu
        /// </summary>
        public string ContactCode { get; set; }
    }
}