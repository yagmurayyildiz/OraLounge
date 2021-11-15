using Domain.OraLounge.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.OraLounge.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<string> CreatePaymentSession(int bookingId);
        Task<Booking> SetPaymentResult(string sessionId, PaymentStatus status);
    }
}
