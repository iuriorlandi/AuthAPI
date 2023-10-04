using System.Security.Cryptography;

namespace AuthAPI.Utilities
{
    public class PasswordUtility
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 10000;

        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] salt)
        {
            using var pbkdf2 = new Rfc2898DeriveBytes(password, SaltSize, Iterations, HashAlgorithmName.SHA256);
            salt = pbkdf2.Salt;
            passwordHash = pbkdf2.GetBytes(HashSize);
        }

        public static bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt)
        {
            using var pbkdf2 = new Rfc2898DeriveBytes(password, storedSalt, Iterations, HashAlgorithmName.SHA256);
            byte[] newHash = pbkdf2.GetBytes(HashSize);
            return CompareByteArrays(storedHash, newHash);
        }

        private static bool CompareByteArrays(byte[] storedPasswordHash, byte[] newHash)
        {
            if (storedPasswordHash.Length != newHash.Length)
                return false;

            for (int i = 0; i < storedPasswordHash.Length; i++)
            {
                if (storedPasswordHash[i] != newHash[i])
                    return false;
            }

            return true;
        }
    }
}
