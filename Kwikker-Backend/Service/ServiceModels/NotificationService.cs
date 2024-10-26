﻿using AutoMapper;
using Contracts;
using Entities.DomainModels;
using Entities.ExceptionModels;
using Service.Contracts.Contracts;
using Shared.DTOs;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceModels
{
    public class NotificationService: INotificationService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
  
        public NotificationService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
          
        }

        public async Task CreateNotification(int senderId, string type, int receiverId, string message)
        {
      
             _repository.NotificationRepository.CreateNotification(senderId,type,receiverId,message);
           await _repository.SaveAsync();
        }

        public async Task<IEnumerable<NotificationDTO>> GetUserNotificationsAsync(int receiverId,bool trackChanges)
        {
            var notificationEntities=await _repository.NotificationRepository.GetUserNotificationsAsync(receiverId, trackChanges);

            if(notificationEntities is null) return Enumerable.Empty<NotificationDTO>();    

           var notifications= _mapper.Map<IEnumerable<NotificationDTO>>(notificationEntities);
            return notifications;
        }

        public async Task MarkAsReadAsync(int notificationId,bool trackChanges)
        {
            var notification = await  _repository.NotificationRepository.GetNotification(notificationId, trackChanges);

            if (notification is null) throw new NotFoundException("this notification doesn't exist");
            notification.IsRead = true;
            await _repository.SaveAsync();
        }
    }
}
