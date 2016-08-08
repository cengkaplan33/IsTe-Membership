using System;

namespace Membership.Core.Domain.Cache
{
    public class AccountCache
    {
        public string AccountCode { get; set; }

        public DateTime LoginTime { get; set; }

        public DateTime LastProcessTime { get; set; }

        public string LastRequestIp { get; set; }
    }
}