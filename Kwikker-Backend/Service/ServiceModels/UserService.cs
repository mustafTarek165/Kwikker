using AutoMapper;
using Contracts;
using Entities.ExceptionModels;
using Entities.Models;
using Service.Contracts.Contracts;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceModels
{
    internal sealed class UserService:IUserService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public UserService(IRepositoryManager repository, ILoggerManager
        logger,IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task< UserDTO> GetUser(int id, bool trackChanges)
        {
          
                var user =await _repository.UserRepository.GetUser(id, trackChanges);
                
                if (user is null)
                    throw new NotFoundException(id,"User");

                var UserDTO = _mapper.Map<UserDTO>(user);
                return UserDTO;
            
        }

        public async Task<int> GetUserCount()
        =>await _repository.UserRepository.GetUsersCount();
    }
}
