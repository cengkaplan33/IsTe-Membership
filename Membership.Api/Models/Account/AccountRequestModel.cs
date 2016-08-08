using Membership.Service.Models.Api.Request;

namespace Membership.Api.Models.Account
{
    public class InsertAccountRequestModel : AccountRequestBase
    {
    }

    public class UpdateAccountRequestModel : AccountRequestBase
    {
        /// <summary>
        ///     Benzersiz kullanıcı kodu
        /// </summary>
        public string AccountCode { get; set; }      
    }

    public class DeleteAccountRequestModel
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

    public class GetAccountRequestModel
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

    public class InsertAccountAddressRequestModel : AccountAddressRequestBase
    {
    }

    public class UpdateAccountAddressRequestModel : AccountAddressRequestBase
    {
        /// <summary>
        ///     Benzersiz adres kodu
        /// </summary>
        public string AddressCode { get; set; }
    }

    public class DeleteAccountAddressRequestModel
    {
        /// <summary>
        ///     Benzersiz kullanıcı kodu
        /// </summary>
        public string AccountCode { get; set; }

        /// <summary>
        ///     Benzersiz kullanıcı adres kaydı kodu
        /// </summary>
        public string AddressCode { get; set; }
    }

    public class GetAccountAddressRequestModel
    {
        /// <summary>
        ///     Benzersiz kullanıcı kodu
        /// </summary>
        public string AccountCode { get; set; }

        /// <summary>
        ///     Benzersiz kullanıcı adres kaydı kodu
        /// </summary>
        public string AddressCode { get; set; }
    }

    public class InsertAccountContactRequestModel : AccountContactRequestBase
    {
    }

    public class UpdateAccountContactRequestModel : AccountContactRequestBase
    {
        /// <summary>
        ///     Benzersiz kontak kodu
        /// </summary>
        public string ContactCode { get; set; }
    }

    public class DeleteAccountContactRequestModel
    {
        /// <summary>
        ///     Benzersiz kullanıcı kodu
        /// </summary>
        public string AccountCode { get; set; }

        /// <summary>
        ///     Benzersiz kullanıcı kontak kaydı kodu
        /// </summary>
        public string ContactCode { get; set; }
    }

    public class GetAccountContactRequestModel
    {
        /// <summary>
        ///     Benzersiz kullanıcı kodu
        /// </summary>
        public string AccountCode { get; set; }

        /// <summary>
        ///     Benzersiz kullanıcı kontak kaydı kodu
        /// </summary>
        public string ContactCode { get; set; }
    }

    public class ChangePasswordRequestModel
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
        ///     Mevcut şifre
        /// </summary>
        public string OldPassword { get; set; }

        /// <summary>
        ///     Yeni şifre
        /// </summary>
        public string NewPassword { get; set; }
    }

    public class AccountBlockRequestModel
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

    public class RecoveryPasswordRequestModel
    {
        /// <summary>
        ///     Uygulama Id'si
        /// </summary>
        public int ApplicationId { get; set; }

        /// <summary>
        ///     Email
        /// </summary>
        public string Email { get; set; }        
    }
}