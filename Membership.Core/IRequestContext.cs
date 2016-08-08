using System;
using Membership.Core.Domain.Application;

namespace Membership.Core
{
    /// <summary>
    ///     Servis isteklerinin takip edilebilmesi ve loglanması için kullanılır.
    /// </summary>
    public interface IRequestContext
    {
        Application CurrentApplication { get; set; }

        string RequestIpAddress { get; set; }

        string UserAgent { get; set; }

        string RequestUrl { get; set; }

        DateTime RequestTime { get; set; }
    }
}