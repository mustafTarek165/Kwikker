using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public record NotificationDTO( int id,int senderId,int receiverId,string message,bool isRead,DateTime createdAt);
}
