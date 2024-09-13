using QPR_Application.Models.DTO.Utility;
using QPR_Application.Models.Entities;
using System.Security.Cryptography;
using System.Text;

namespace QPR_Application.Util
{
    public class PasswordHashingUtil
    {
        public HashWithSaltResult CreateHashWithSalt(string password, int saltLength, HashAlgorithm hashAlgo)
        {
            RNG rng = new RNG();
            byte[] saltBytes = rng.GenerateRandomCryptographicBytes(saltLength);
            byte[] passwordAsBytes = Encoding.UTF8.GetBytes(password);
            List<byte> passwordWithSaltBytes = new List<byte>();
            passwordWithSaltBytes.AddRange(passwordAsBytes);
            passwordWithSaltBytes.AddRange(saltBytes);
            byte[] digestBytes = hashAlgo.ComputeHash(passwordWithSaltBytes.ToArray());
            return new HashWithSaltResult(Convert.ToBase64String(saltBytes), Convert.ToBase64String(digestBytes));
        }

        public string GetHashedDigest(string password, string salt)
        {
            byte[] passBytes = Encoding.UTF8.GetBytes(password);
            byte[] saltBytes = Convert.FromBase64String(salt);
            /*byte[] saltBytes = Encoding.UTF8.GetBytes(salt);*/
            List<byte> passwordWithSaltBytes = new List<byte>();
            passwordWithSaltBytes.AddRange(passBytes);
            passwordWithSaltBytes.AddRange(saltBytes);
            byte[] digestBytes = SHA512.Create().ComputeHash(passwordWithSaltBytes.ToArray());
            return Convert.ToBase64String(digestBytes);
        }
        public Boolean VerifyUserPassword(Login user, registration userDetails)
        {
            //PasswordHashingUtil pwHasher = new PasswordHashingUtil();
            string hashedPassword = GetHashedDigest(user.Password, userDetails.PasswordSalt);
            if (hashedPassword == userDetails.password)
                return true;
            return false;
        }
    }
    public class RNG
    {
        public string GenerateRandomCryptographicKey(int keyLength = 10)
        {
            return Convert.ToBase64String(GenerateRandomCryptographicBytes(keyLength));
        }

        public byte[] GenerateRandomCryptographicBytes(int keyLength)
        {
            var rngCryptoServiceProvider = RandomNumberGenerator.Create();
            byte[] randomBytes = new byte[keyLength];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            return randomBytes;
        }
    }
}
