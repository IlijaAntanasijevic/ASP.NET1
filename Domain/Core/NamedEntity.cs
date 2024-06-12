using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Core
{
    public abstract class NamedEntity : Entity
    {
        public string Name { get; set; }
    }
}
