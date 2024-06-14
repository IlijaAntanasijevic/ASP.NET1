using System;
using System.Collections.Generic;
using System.Text;
using Domain.Core;



namespace App.Domain
{
    public class User : Entity
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Avatar { get; set; }

        public virtual ICollection<Apartment> Apartments { get; set; } = new HashSet<Apartment>();
        public virtual ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();
        public virtual ICollection<UserUseCase> UseCases { get; set; } = new HashSet<UserUseCase>();


    }
}
