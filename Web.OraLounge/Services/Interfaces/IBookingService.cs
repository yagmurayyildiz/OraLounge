using Domain.OraLounge.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.OraLounge.Services.Interfaces
{
    public interface IBookingService
    {
        Task<List<Booking>> GetBookingsOfDayByPlaceAsync(DateTime day, BookingPlace place);
        Task<List<DateTime>> GetAvailableTimesAsync(int peopleCount, DateTime day, BookingPlace place);
        List<DateTime> GetTimePeriods(DateTime day);
        Task<List<Table>> GetTablesAsync();
        Task<List<Booking>> GetAllBookingsAsync();
        Task<Booking> GetBookingByIdAsync(int id);
        Task CreateBookingAsync(Booking booking);
        Task UpdateBookingAsync(Booking booking);
        Task DeleteBookingAsync(Booking booking);
    }
}
