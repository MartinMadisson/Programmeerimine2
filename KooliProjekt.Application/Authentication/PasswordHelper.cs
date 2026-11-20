using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace KooliProjekt.Application.Authentication
{
    public static class PasswordHelper
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 100_000;

        public static string Hash(string password)
        {
            var salt = new byte[SaltSize];
            RandomNumberGenerator.Fill(salt);

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                Iterations,
                HashAlgorithmName.SHA256,
                HashSize);

            var combined = new byte[SaltSize + HashSize];
            Buffer.BlockCopy(salt, 0, combined, 0, SaltSize);
            Buffer.BlockCopy(hash, 0, combined, SaltSize, HashSize);

            return Convert.ToBase64String(combined);
        }

        // Tagastab true, kui väärtus on PasswordHelper.Hash'i tehtud räsi (mitte tavaline tekst parool)
        public static bool IsHash(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            try
            {
                return Convert.FromBase64String(value).Length == SaltSize + HashSize;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static bool Verify(string password, string hashedPassword)
        {
            // Kui andmebaasis on veel vana (räsimata) parool, siis ei lase sisse ega viska viga
            if (!IsHash(hashedPassword))
            {
                return false;
            }

            var combined = Convert.FromBase64String(hashedPassword);

            var salt = new byte[SaltSize];
            Buffer.BlockCopy(combined, 0, salt, 0, SaltSize);

            var storedHash = new byte[HashSize];
            Buffer.BlockCopy(combined, SaltSize, storedHash, 0, HashSize);

            var computedHash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                Iterations,
                HashAlgorithmName.SHA256,
                HashSize);

            return CryptographicOperations.FixedTimeEquals(storedHash, computedHash);
        }
    }
}
