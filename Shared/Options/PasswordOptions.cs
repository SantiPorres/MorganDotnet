using System.Security.Cryptography;

namespace Application.Options
{
    public class PasswordOptions
    {
        public int SaltSize { get; set; }

        public int KeySize { get; set; }

        public int Iterations { get; set; }

        public char Delimiter { get; set; }
    }
}
