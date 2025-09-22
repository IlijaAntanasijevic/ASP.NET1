using Application.DTO.Bookings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public interface IEmailSender
    {
        Task ConfirmRegistrationAsync(string email, string code);
        Task ForgotPasswordAsync(string email, string code);
        Task BookingConfirmed(ConfirmedBookingEmailDto data);
        Task NewReservationAsync(NewReservationDto data);
    }
}
