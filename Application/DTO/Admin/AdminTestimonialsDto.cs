using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Admin
{
    public class AdminTestimonialsDto : HomeTestimonials
    {
        public bool IsVisibleOnHome { get; set; }
    }
}
