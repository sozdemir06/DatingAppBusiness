using DatingApp.Core.Entities;

namespace DatingApp.Entities.Concrete
{
    public class Like:IEntity
    {  
        public int LikerId { get; set; }
        public int LikeeId { get; set; }
        public User Liker { get; set; }
        public User Likee { get; set; }
    }
}