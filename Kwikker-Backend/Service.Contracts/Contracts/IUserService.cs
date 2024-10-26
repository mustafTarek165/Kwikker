using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts.Contracts
{
    public interface IUserService
    {
        Task<UserDTO> GetUser(int id,bool trackChanges);
        Task<int> GetUserCount();   
    }
}
