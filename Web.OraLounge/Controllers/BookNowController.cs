using AutoMapper;
using Domain.OraLounge.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.OraLounge.Models;
using Web.OraLounge.Services.Interfaces;

namespace Web.OraLounge.Controllers
{
    public class BookNowController : BaseController
    {
        private readonly IBookingService _bookingService;
        private readonly IMapper _mapper;

        public BookNowController(IBookingService bookingService, IMapper mapper)
        {
            _bookingService = bookingService;
            _mapper = mapper;
        }

        // GET: BookNow
        public ActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> FillTimes(int peopleCount, int bookingPlace, string bookingDate)
        {
            var times = await _bookingService.GetAvailableTimesAsync(peopleCount, Convert.ToDateTime(bookingDate), (BookingPlace)bookingPlace);
            return Json(times.Select(x => x.ToString("HH:mm")), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> Index(BookingViewModel model)
        {
            if (ModelState.IsValid)
            {
                var booking = _mapper.Map<BookingViewModel, Booking>(model);
                await _bookingService.CreateBookingAsync(booking);

                return RedirectToAction("Index", "Checkout", new { bookingId = booking.Id });
            }

            return View(model);
        }
    }
}