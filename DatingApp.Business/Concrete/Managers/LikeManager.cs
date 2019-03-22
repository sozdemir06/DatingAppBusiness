using System;
using System.Security.Claims;
using System.Threading.Tasks;
using DatingApp.Business.Abstract;
using DatingApp.DataAccess.Abstract;
using DatingApp.Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace DatingApp.Business.Concrete.Managers
{
    public class LikeManager : ILikeService
    {
        private readonly ILikeDal likeDal;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IUserDal userDal;
        public LikeManager(ILikeDal likeDal, IHttpContextAccessor httpContextAccessor, IUserDal userDal)
        {
            this.userDal = userDal;
            this.httpContextAccessor = httpContextAccessor;
            this.likeDal = likeDal;

        }
        public async Task<Like> AddLike(int userId, int recipientId)
        {
            if (userId != int.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                throw new Exception("UnAuthorized.!!");
            }
            var getLike = await likeDal.Get(u => u.LikerId == userId && u.LikeeId == recipientId);
            if (getLike != null)
            {
                throw new Exception("You already like this profiles.!!");
            }

            var getLikeesUserFromRepo = await userDal.Get(u=>u.Id==recipientId);
            if(getLikeesUserFromRepo==null)
            {
                throw new Exception("Not found that user you want to like.!!");
            }

            getLike=new Like
            {
                LikerId=userId,
                LikeeId=recipientId
            };

            var addLikeToDb=await likeDal.Add(getLike);
            if(addLikeToDb==null)
            {
                throw new Exception("Failed to like user.!!");
            }

            return addLikeToDb;
        }
    }
}