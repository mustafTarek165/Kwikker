using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Contracts.Contracts
{
    public interface IUserRepository
    {
       public Task<User> GetUser(int id,bool trackChanges);
        public Task<int> GetUsersCount();
    }
}
