using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class UseCaseLogsDto
    {
        public int Id { get; set; }
        public string UseCaseName { get; set; }
        public string Email { get; set; }
        public object UseCaseData { get; set; }
        public DateTime ExecutedAt { get; set; }
    }
}
