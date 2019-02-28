using System.Collections.Generic;
using System.Linq;
using DatingApp.Core.Utilities.Helpers.AuthHelpers;
using DatingApp.DataAccess.Concrete.EntityFramework;
using DatingApp.Entities.Concrete;
using Newtonsoft.Json;

namespace DatingApp.WebAPI.SeedData
{
    public class Seed
    {
        
        private readonly DataContext context;
        private readonly IAuthHelper authHelper;
        public Seed(DataContext context, IAuthHelper authHelper)
        {
            this.authHelper = authHelper;
            this.context = context;

        }

        public void SeedUsers()
        {
            if(!context.Users.Any())
            {
                var userData=System.IO.File.ReadAllText("SeedData/UserSeedData.json");
                var users=JsonConvert.DeserializeObject<List<User>>(userData);
                foreach (var user in users)
                {
                    byte[] passwordHash,passwordSalt;
                    authHelper.CreatePasswordHash("password",out passwordHash,out passwordSalt);
                    user.PasswordHash=passwordHash;
                    user.PasswordSalt=passwordSalt;
                    user.UserName=user.UserName.ToLower();
                    user.Email=user.Email;

                    context.Users.Add(user);
                }

                context.SaveChanges();
            }
           
        }
    }
}