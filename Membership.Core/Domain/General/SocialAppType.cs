using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Membership.Core.Domain.General
{
    [Table("SocialAppType")]
    public class SocialAppType : DomainBase
    {
        /// <summary>
        ///     Internal Id
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        ///     Sosyal Uygulama Adı
        /// </summary>
        public string AppName { get; set; }

        /// <summary>
        ///     Sosyal Uygulama Tipi
        /// </summary>
        public int AppType { get; set; }

        /// <summary>
        ///     Durumu
        /// </summary>
        public byte Status { get; set; }
    }
}