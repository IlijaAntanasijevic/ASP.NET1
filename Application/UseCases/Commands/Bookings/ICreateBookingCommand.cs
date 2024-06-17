using Application.DTO.Bookings;


namespace Application.UseCases.Commands.Bookings
{
    public interface ICreateBookingCommand : ICommand<BookingDto>
    {
    }
}
