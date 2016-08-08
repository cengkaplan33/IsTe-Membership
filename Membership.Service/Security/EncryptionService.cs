using System;
using System.Security.Cryptography;
using System.Text;
using Membership.Core.Helper;

namespace Membership.Service.Security
{
    public class EncryptionService : IEncryptionService
    {
        private const int SaltKeySize = 20;
        private const string PasswordHashFormat = "SHA1";
        private const string ValidAlphaNumericChar = "ABCDEFGHJKLMNPRSTUVWXYZ1234567890";
        private const string ValidNumericChar = "1234567890";        

        public string GenerateSaltKey()
        {
            var rng = new RNGCryptoServiceProvider();

            var buff = new byte[SaltKeySize];

            rng.GetBytes(buff);

            return Convert.ToBase64String(buff);
        }

        public string GenerateValueHash(string value, string key)
        {
            var saltAndPassword = string.Concat(value, key);

            var algorithm = HashAlgorithm.Create(PasswordHashFormat);

            if (algorithm == null)
                ExceptionHelper.ThrowIfInvalidArgument(() => algorithm);

            var hashByteArray = algorithm.ComputeHash(Encoding.UTF8.GetBytes(saltAndPassword));

            return BitConverter.ToString(hashByteArray).Replace("-", "");
        }

        public string GenerateUniqueNumericValue(int size)
        {
            var result = new StringBuilder();

            using (var rng = new RNGCryptoServiceProvider())
            {
                var uintBuffer = new byte[sizeof(uint)];

                var length = size;

                while (length-- > 0)
                {
                    rng.GetBytes(uintBuffer);

                    var num = BitConverter.ToUInt32(uintBuffer, 0);

                    result.Append(ValidNumericChar[(int)(num % (uint)ValidNumericChar.Length)]);
                }
            }

            return result.ToString();
        }

        public string GenerateUniqueAlphaNumericValue(int size)
        {
            var result = new StringBuilder();

            using (var rng = new RNGCryptoServiceProvider())
            {
                var uintBuffer = new byte[sizeof(uint)];

                var length = size;

                while (length-- > 0)
                {
                    rng.GetBytes(uintBuffer);

                    var num = BitConverter.ToUInt32(uintBuffer, 0);

                    result.Append(ValidAlphaNumericChar[(int)(num % (uint)ValidAlphaNumericChar.Length)]);
                }
            }

            return result.ToString();
        }
    }
}