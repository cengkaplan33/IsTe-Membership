using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Membership.Core.Domain.Application
{
    [Table("ApplicationSocialApp")]
    public class ApplicationSocialApp : DomainBase
    {
        /// <summary>
        ///     Internal Id 
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        ///     Uygulama Id'si
        /// </summary>
        public int ApplicationId { get; set; }

        /// <summary>
        ///     Sosyal uygulama Id'si
        /// </summary>
        public int SocialAppTypeId { get; set; }

        /// <summary>
        ///     Sosyal Uygulama Id'si
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        ///     Sosyal Uygulama Secret Key'i
        /// </summary>
        public string AppSecret { get; set; }

        /// <summary>
        ///     Üye iş yeri uygulaması geri dönüş url'i
        /// </summary>
        public string CallbackUrl { get; set; }
    }
}