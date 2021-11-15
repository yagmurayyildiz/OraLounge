using Domain.OraLounge.Entities;
using Domain.OraLounge.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.OraLounge.Repositories
{
    internal class BookingRepository : Repository<Booking>, IBookingRepsitory
    {
        internal BookingRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Task<List<Booking>> GetBookingsByDateAndPlace(DateTime date, BookingPlace place)
        {
            return Set.Where(x => x.PaymentStatus == PaymentStatus.Paid && DbFunctions.TruncateTime(x.BookingTime) == date.Date && x.BookingPlace == place).OrderBy(x=> x.BookingTime).ToListAsync();
        }

        public Task<Booking> FindBySessionIdAsync(string sessionId)
        {
            return Set.FirstOrDefaultAsync(x => x.StripeSessionId == sessionId);
        }
    }
}
