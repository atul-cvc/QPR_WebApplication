using QPR_Application.Models.DTO.Response;
using QPR_Application.Models.DTO.Utility;
using QPR_Application.Models.Entities;
using System.Security.Cryptography;

namespace QPR_Application.Util.VerifyPassword
{
    public class VerifyPassword
    {
        public Boolean VerifyUserPassword(Login user, registration userDetails)
        {
            PasswordHashingUtil pwHasher = new PasswordHashingUtil();
            string hashedPassword = pwHasher.GetHashedDigest(user.Password, userDetails.PasswordSalt);
            if(hashedPassword == userDetails.password)
                return true;
            return false;
        }
    }
}
