using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Bookings
{
    public abstract class BasicBookingDto
    {
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int TotalGuests { get; set; }
        
    }

    public class BookingDto : BasicBookingDto
    {
        public int ApartmentId { get; set; }
        public int PaymentId { get; set; }

    }

    public class EditBookingDto : BasicBookingDto
    {
        public int BookingId { get; set; }
        public int PaymentId { get; set; }
    }

    public class ConfirmedBookingEmailDto
    {
        public string Email { get; set; }
        public string CheckIn { get; set; }
        public string CheckOut { get; set; }
        public string Adults { get; set; }
        public string Childrens { get; set; }
        public string Address { get; set; }
        public string PricePerNight { get; set; }
        public string TotalPrice {  get; set; }
        public string UserName { get; set; }
        public string UserLastName { get; set; }
        public string UserPhone { get; set; }
        public string OwnerName { get; set; }
        public string OwnerLastName { get; set; }
        public string OwnerPhone { get; set; }
        public string OwnerEmail { get; set; }
    }

    public class NewReservationDto 
    {
        public string EmailToSend { get; set; }
        public string CheckIn { get; set; }
        public string CheckOut { get; set; }
        public string Adults { get; set; }
        public string Childrens { get; set; }
        public string TotalPrice {  get; set; }
        public string Address { get; set; }
        public string UserName { get; set; }
        public string UserLastName { get; set; }
        public string UserPhone { get; set; }
        public string UserEmail { get; set; }
    }

}
