using System;

namespace Membership.Service.Models.Api.Response
{
    public class AccountResponseBase
    {
        /// <summary>
        ///     Benzersiz kullanıcı kodu
        /// </summary>
        public string AccountCode { get; set; }

        /// <summary>
        ///     Bayi kodu
        /// </summary>
        public string ResellerCode { get; set; }

        /// <summary>
        ///     Kullanıcı email'i
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     Kullanıcı adı
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Kullanıcı soyadı
        /// </summary>
        public string Surname { get; set; }      

        /// <summary>
        ///     Cinsiyet
        /// </summary>
        public byte Gender { get; set; }

        /// <summary>
        ///     Gsm
        /// </summary>
        public string Gsm { get; set; }

        /// <summary>
        ///     Doğum tarihi
        /// </summary>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        ///     Kullanıcı iletişim dili
        /// </summary>
        public string Locale { get; set; }

        /// <summary>
        ///     Hesap tipi
        /// </summary>
        public int AccountType { get; set; }

        /// <summary>
        ///     Alternatif email
        /// </summary>
        public string AlternativeEmail { get; set; }

        /// <summary>
        ///     Risk seviyesi
        /// </summary>
        public int RiskLevel { get; set; }
    }
}