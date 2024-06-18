using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Users
{
    public class UpdateUserDto : UserDto
    {
        public string Password { get; set; }
 
    }
}
