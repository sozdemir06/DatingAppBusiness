using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.Core.DataAccess.EntityFrameWork;
using DatingApp.Core.Utilities.Helpers.AppHeaderHelper;
using DatingApp.DataAccess.Abstract;
using DatingApp.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.DataAccess.Concrete.EntityFramework
{
    public class EfMessageDal : IEntityRepositoryBase<Message,DataContext>, IMessageDal
    {
        public async Task<Message> GetAddedMessageWithUserInfo(int messageId)
        {
            using(var context=new DataContext())
            {
                var message=await context.Messages.Include(u=>u.Sender).ThenInclude(p=>p.Photos)
                                                  .Include(u=>u.Recipient).ThenInclude(p=>p.Photos)
                                                  .FirstOrDefaultAsync(m=>m.Id==messageId);
                        return message;
            }
        }

        public async Task<PagedList<Message>> GetMessagesForUser(MessageParams messageParams)
        {
            using(var context=new DataContext())
            {
                var messages=context.Messages
                            .Include(u=>u.Sender).ThenInclude(p=>p.Photos)
                            .Include(u=>u.Recipient).ThenInclude(p=>p.Photos)
                            .AsQueryable();

                switch (messageParams.MessageContainer)
                {
                    case "Inbox":
                        messages=messages.Where(u=>u.RecipientId==messageParams.UserId);
                    break;
                    case "Outbox":
                        messages=messages.Where(u=>u.SenderId==messageParams.UserId);
                    break;
                    default:
                        messages=messages.Where(u=>u.RecipientId==messageParams.UserId && u.IsRead==false);
                    break;
  
                }
                messages=messages.OrderByDescending(d=>d.MessageSent);
                return await PagedList<Message>.CreateAsync(messages,messageParams.PageNumber,messageParams.PageSize);

            }
        }

        public async Task<IEnumerable<Message>> GetMessageThread(int userId, int recipientId)
        {
            using(var context=new DataContext())
            {
                var messageThread=await context.Messages.Include(m=>m.Sender).ThenInclude(p=>p.Photos)
                                                        .Include(m=>m.Recipient).ThenInclude(p=>p.Photos)
                                                        .Where(u=>u.RecipientId==userId && u.SenderId==recipientId || u.RecipientId==recipientId && u.SenderId==userId)
                                                        .OrderByDescending(m=>m.MessageSent)
                                                        .ToListAsync();
                return messageThread;   

            }
            
        }
    }
}