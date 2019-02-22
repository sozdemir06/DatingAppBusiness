namespace DatingApp.Core.Utilities.Helpers.AuthHelpers
{
    public static class VerifyPasswordHash
    {
        public static bool CheckPassword(string password,byte[] passwordHash,byte[] passwordSalt)
        {
            using(var hmac=new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computehash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i <computehash.Length; i++)
                {
                    if(computehash[i]!=passwordHash[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}