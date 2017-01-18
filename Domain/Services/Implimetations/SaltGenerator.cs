namespace Domain.Services.Implimetations
{
    using System.Security.Cryptography;
    using ByndyuSoft.Infrastructure.Common.Extensions;
    using JetBrains.Annotations;

    [UsedImplicitly]
    public class SaltGenerator : ISaltGenerator
    {
        private const int SaltLength = 3;

        public string GetBase64Salt()
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var salt = new byte[SaltLength];
                rngCryptoServiceProvider.GetBytes(salt);
                return salt.ToBase64();
            }
        }
    }
}