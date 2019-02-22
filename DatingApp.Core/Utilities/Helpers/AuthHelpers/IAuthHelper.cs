namespace DatingApp.Core.Utilities.Helpers.AuthHelpers
{
    public interface IAuthHelper
    {
         void CreatePasswordHash(string password,out byte[] PasswordHash,out byte[] passwordSalt);
         bool VerifyPasswordHash(string password,byte[] passwordHash,byte[] passwordSalt);
         string GenerateJwtToken(int userId,string username,string[] userroles);
    }
}