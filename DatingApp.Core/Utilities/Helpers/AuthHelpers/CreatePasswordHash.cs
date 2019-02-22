namespace DatingApp.Core.Utilities.Helpers.AuthHelpers
{
    public static class CreatePasswordHash
    {
        public static void CreateHash(string password,out byte[] PasswordHash,out byte[] passwordSalt)
        {
            using(var hmac=new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt=hmac.Key;
                PasswordHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}