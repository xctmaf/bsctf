namespace Domain.Services.Implimetations
{
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using ByndyuSoft.Infrastructure.Common.Extensions;
    using JetBrains.Annotations;

    [UsedImplicitly]
    public class PasswordHasher : IPasswordHasher
    {
        private static readonly HashAlgorithm HashAlgorithm = SHA512.Create();
        
        public string GetHashedPassword(string password, string salt)
        {
            var encoding = Encoding.UTF8;
            var passwordBytes = salt.FromBase64().Concat(encoding.GetBytes(password)).ToArray();
            return HashAlgorithm.ComputeHash(passwordBytes).ToBase64();

        }
    }
}