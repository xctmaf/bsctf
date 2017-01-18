namespace Domain.Services.Implimetations
{
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;


    public class PasswordHasherService : IPasswordHasherService
    {
        private static readonly int SaltLength = 3;
        private static readonly HashAlgorithm hashAlgorithm = SHA512.Create();

        private string GetSalt()
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var salt = new byte[SaltLength];
                rngCryptoServiceProvider.GetBytes(salt);
                return salt.ToBase64();
            }
        }

        public string GetHashedPassword(string password)
        {
            var salt = GetSalt();

            var encoding = Encoding.UTF8;
            byte[] passwordBytes = encoding.GetBytes(password)
                .Concat(salt.FromBase64())
                .ToArray();
            return hashAlgorithm.ComputeHash(passwordBytes).ToBase64();

        }



        private static string HashPassword(string password, string salt)
        {
        }

       
    }
}