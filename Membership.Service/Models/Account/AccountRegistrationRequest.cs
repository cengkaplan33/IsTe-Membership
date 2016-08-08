using System;

namespace Membership.Service.Models.Account
{
    public class AccountRegistrationRequest
    {
        public string ResellerCode { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string IdentityNo { get; set; }

        public byte Gender { get; set; }

        public string Gsm { get; set; }

        public DateTime? BirthDate { get; set; }

        public string AlternativeEmail { get; set; }
    }
}