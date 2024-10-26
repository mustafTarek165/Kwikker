using Entities.DomainModels;
using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Contracts.Contracts
{
    public interface INotificationRepository
    {
        void  CreateNotification(int senderId, string type, int receiverId, string message);
        Task<IEnumerable<Notification>> GetUserNotificationsAsync(int receiverId,bool trackChanges);
      

        Task<Notification> GetNotification(int id,bool trackChanges); 
    }
}
