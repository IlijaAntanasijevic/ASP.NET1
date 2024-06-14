using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class ErrorLog
    {
        public Guid ErrorId { get; set; }
        public string Message { get; set; }
        public string StrackTrace { get; set; }
        public DateTime Time { get; set; }
    }
}
