using Entities.DomainModels;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RepositoryModels
{
    public class NotificationRepository : RepositoryBase<Notification>, INotificationRepository
    {
        public NotificationRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

       

        public void CreateNotification(int senderId, string type, int receiverId, string message)
        {
            Notification notification = new Notification()
            {
                SenderId=senderId,
                ReceiverId=receiverId,
                Message=message,
                Type=type
            };
            Create(notification);
        }
           
        
        public async Task<Notification> GetNotification(int id, bool trackChanges)
        {
           var notification=await FindByCondition(x => x.Id.Equals(id), trackChanges).SingleOrDefaultAsync();
            return notification!;
        }

        public async Task<IEnumerable<Notification>> GetUserNotificationsAsync(int receiverId,bool trackChanges)
        {

            var notifications = await FindByCondition(x => x.ReceiverId.Equals(receiverId), trackChanges).ToListAsync();
            return notifications!;
        }

        
    }
}
