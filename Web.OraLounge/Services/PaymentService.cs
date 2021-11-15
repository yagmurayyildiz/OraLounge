using Domain.OraLounge;
using Domain.OraLounge.Entities;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Web.OraLounge.Services.Interfaces;

namespace Web.OraLounge.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        private string stripeApiKey = "sk_test_";
        private string stripeSuccessUrl = "https://localhost:44322/Checkout/Success";
        private string stripeFailUrl = "https://localhost:44322/Checkout/Cancel";
        public async Task<string> CreatePaymentSession(int bookingId)
        {
            var booking = await _unitOfWork.BookingRepository.FindByIdAsync(bookingId);
            if (booking == null)
                return null;

            StripeConfiguration.ApiKey = stripeApiKey;
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                  "card",
                },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                          UnitAmountDecimal = booking.TotalAmount*100,
                          Currency = "gbp"
                        },
                        Quantity = 1,
                    },
                },
                Mode = "payment",
                SuccessUrl = stripeSuccessUrl + "?session_id={CHECKOUT_SESSION_ID}",
                CancelUrl = stripeFailUrl + "?session_id={CHECKOUT_SESSION_ID}",
            };

            var service = new SessionService();
            Session session = await service.CreateAsync(options);
            if (string.IsNullOrWhiteSpace(session.Id))
                return null;

            booking.StripeSessionId = session.Id;
            _unitOfWork.BookingRepository.Update(booking);
            await _unitOfWork.SaveChangesAsync();

            return session.Url;
        }

        public async Task<Booking> SetPaymentResult(string sessionId, PaymentStatus status)
        {
            var booking = await _unitOfWork.BookingRepository.FindBySessionIdAsync(sessionId);
            if (booking != null)
            {
                booking.PaymentStatus = status;
                _unitOfWork.BookingRepository.Update(booking);
                await _unitOfWork.SaveChangesAsync();
            }
            return booking;
        }
    }
}