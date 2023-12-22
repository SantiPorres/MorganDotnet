using Application.Interfaces;
using Application.Options;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace Shared.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        private readonly IOptions<PasswordOptions> _passwordOptions;

        public PasswordHasher(IOptions<PasswordOptions> passwordOptions)
        {
            _passwordOptions = passwordOptions;
        }

        private static readonly HashAlgorithmName _hashAlgorithmName = HashAlgorithmName.SHA256;

        public bool Verify(string passwordHash, string inputPassword)
        {
            var elements = passwordHash.Split(_passwordOptions.Value.Delimiter);
            var salt = Convert.FromBase64String(elements[0]);
            var hash = Convert.FromBase64String(elements[1]);
            var hashInput = Rfc2898DeriveBytes.Pbkdf2(
                    inputPassword,
                    salt,
                    _passwordOptions.Value.Iterations,
                    _hashAlgorithmName,
                    _passwordOptions.Value.KeySize
                );
            return CryptographicOperations.FixedTimeEquals( hash, hashInput );
        }

        public string Hash(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(_passwordOptions.Value.SaltSize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                    password, 
                    salt,
                    _passwordOptions.Value.Iterations, 
                    _hashAlgorithmName,
                    _passwordOptions.Value.KeySize
                );
            return string.Join(_passwordOptions.Value.Delimiter, Convert.ToBase64String(salt), Convert.ToBase64String(hash));
        }
    }
}
