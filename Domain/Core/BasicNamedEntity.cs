using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Core
{
    public abstract class BasicNamedEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
