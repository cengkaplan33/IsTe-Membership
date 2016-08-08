using System;
using System.Security.Cryptography;
using System.Text;
using Membership.Core.Extension;

namespace Membership.Service.Security
{
    public static class TokenHelper
    {
        public static string GenerateToken()
        {
            const string valid = "ABCDEFGHJKLMNPRSTUVWXYZ1234567890";

            var result = new StringBuilder();

            using (var rng = new RNGCryptoServiceProvider())
            {
                var uintBuffer = new byte[sizeof(uint)];

                var length = 80;

                while (length-- > 0)
                {
                    rng.GetBytes(uintBuffer);

                    var num = BitConverter.ToUInt32(uintBuffer, 0);

                    result.Append(valid[(int)(num % (uint)valid.Length)]);
                }
            }

            return result.ToString();
        }

        public static string GetApplicationCode(string token)
        {
            try
            {
                if (token.IsNullOrWhitespace())
                    return string.Empty;

                var value = token.Split(':');

                return value[0];
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string Encrypt(string value, string salt)
        {
            var toEncryptArray = Encoding.UTF8.GetBytes(value);

            var hashmd5 = new MD5CryptoServiceProvider();
            var keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(salt));

            hashmd5.Clear();

            var tDes = new TripleDESCryptoServiceProvider
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            var cTransform = tDes.CreateEncryptor();

            var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            tDes.Clear();

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
    }
}