using Domain.OraLounge.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.OraLounge.Repositories
{
    public interface IBookingRepsitory : IRepository<Booking>
    {
        Task<List<Booking>> GetBookingsByDateAndPlace(DateTime date, BookingPlace place);
        Task<Booking> FindBySessionIdAsync(string sessionId);
    }
}
