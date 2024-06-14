using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain
{
    public class Image
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public int ApartmentId { get; set; }

        public virtual Apartment Apartment { get; set; }
    }
}
