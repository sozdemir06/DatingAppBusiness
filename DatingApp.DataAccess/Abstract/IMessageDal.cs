using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.Core.DataAccess;
using DatingApp.Core.Utilities.Helpers.AppHeaderHelper;
using DatingApp.Entities.Concrete;

namespace DatingApp.DataAccess.Abstract
{
    public interface IMessageDal:IEntityRepository<Message>
    {
         Task<PagedList<Message>> GetMessagesForUser(MessageParams messageParams);
         Task<IEnumerable<Message>> GetMessageThread(int userId,int recipientId);
         Task<Message> GetAddedMessageWithUserInfo(int messageId);
    }
}