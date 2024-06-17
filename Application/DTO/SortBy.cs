using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public enum SortDirection
    {
        Asc,
        Desc
    }

    public class SortBy
    {
        public string SortProperty { get; set; }
        public SortDirection Direction { get; set; }
    }
}
