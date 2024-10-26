using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public record UserDTO(int ID,string UserName,string PasswordHash,string Email,string ProfilePicture,string Bio);
}
