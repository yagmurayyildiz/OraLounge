using Domain.OraLounge.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.OraLounge.Helpers.MailHelper;
using Web.OraLounge.Models;
using Web.OraLounge.Services.Interfaces;

namespace Web.OraLounge.Controllers
{
    public class CheckoutController : BaseController
    {
        private readonly IPaymentService _paymentService;

        public CheckoutController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        // GET: Checkout
        public ActionResult Index(string bookingId)
        {
            return View(new CheckoutViewModel { BookingId = Convert.ToInt32(bookingId) });
        }

        [HttpPost]
        public async Task<ActionResult> CreateCheckoutSession(string bookingId)
        {
            try
            {
                var sessionUrl = await _paymentService.CreatePaymentSession(Convert.ToInt32(bookingId));
                if (string.IsNullOrWhiteSpace(sessionUrl))
                    return Json(new { success = false });

                return Json(new { success = true, url = sessionUrl });
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }
        //[HttpPost]
        //public async Task<ActionResult> CreateCheckoutSession(string packageId)
        //{
        //    try
        //    {
        //        if (string.IsNullOrWhiteSpace(packageId))
        //            return Json(new { success = false });

        //        var sessionId = await _paymentService.CreatePaymentSession(Guid.Parse(User.Identity.GetUserId()), Guid.Parse(packageId));
        //        if (string.IsNullOrWhiteSpace(sessionId))
        //            return Json(new { success = false });

        //        return Json(new { success = true, id = sessionId });
        //    }
        //    catch (Exception)
        //    {
        //        return Json(new { success = false });
        //    }
        //}

        public async Task<ActionResult> Success(string session_id)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(session_id))
                {
                    var booking = await _paymentService.SetPaymentResult(session_id, PaymentStatus.Paid);
                    if (booking != null)
                        await SendSuccessMail(booking.Email, booking.Name, booking.Surname);
                }
            }
            catch (Exception)
            {

            }
            return View();
        }

        public async Task<ActionResult> Cancel(string session_id)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(session_id))
                    await _paymentService.SetPaymentResult(session_id, PaymentStatus.Canceled);
            }
            catch (Exception)
            {

            }
            return View();
        }

        private async Task SendSuccessMail(string email, string name, string surname)
        {
            if (!string.IsNullOrEmpty(email))
            {
                var mailSender = new SendFormattedMail("no-reply@oralounge.co.uk", email, "Booking Confirmation");
                mailSender.Header = "Thank you for booking";
                mailSender.Message = "";
                await mailSender.SendHtmlFormattedMailAsync();
            }

        }
    }
}