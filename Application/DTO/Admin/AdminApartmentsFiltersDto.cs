using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Admin
{
    public class AdminApartmentsFiltersDto
    {
        public List<BasicDto> Users {  get; set; }
        public List<BasicDto> TotalBookings {  get; set; }
        public List<BasicDto> Cities {  get; set; }
        public List<BasicDto> Statuses {  get; set; }
        public List<BasicDto> BookingStatuses {  get; set; }
    }
}
