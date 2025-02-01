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

    public enum SortProperty
    {
        Price,
        TopRated,
        MostPopular
    }

    public class SortBy
    {
        public SortProperty SortProperty { get; set; }
        public SortDirection Direction { get; set; }
    }
}
