using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Admin
{
    public class AdminUsersDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public string Phone { get; set; }
        public int TotalBookings { get; set; }
        public int TotalApartments { get; set; }

    }
}
