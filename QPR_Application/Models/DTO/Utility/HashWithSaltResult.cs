namespace QPR_Application.Models.DTO.Utility
{
    public class HashWithSaltResult
    {
        public string Salt { get; }
        public string Digest { get; set; }
        public HashWithSaltResult(string salt, string digest)
        {
            Salt = salt;
            Digest = digest;
        }
    }
}
