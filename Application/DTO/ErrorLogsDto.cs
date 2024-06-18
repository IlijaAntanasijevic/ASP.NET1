using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class ErrorLogsDto
    {
        public Guid ErrorId { get; set; }
        public string Message { get; set; }
        public string StrackTrace { get; set; }
        public DateTime Time { get; set; }
        public string Email { get; set; }
    }
}
