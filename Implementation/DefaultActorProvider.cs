using Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation
{
    //NE KORISTI SE KLASA
    public class DefaultActorProvider : IApplicationActorProvider
    {
        public IApplicationActor GetActor()
        {
            return new Actor
            {
                Id = 0,
                Email = "test",
                FirstName = "Test",
                LastName = "Test"
            };
        }
    }
}
