using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Search
{
    public class UserSearch : PagedSearch
    {
        public string Keyword { get; set; }
    }
}
