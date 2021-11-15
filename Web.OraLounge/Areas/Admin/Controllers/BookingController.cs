using AutoMapper;
using Domain.OraLounge;
using Domain.OraLounge.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.OraLounge.Areas.Admin.Models;
using Web.OraLounge.Services.Interfaces;

namespace Web.OraLounge.Areas.Admin.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IMapper _mapper;
        public BookingController(IBookingService bookingService, IMapper mapper)
        {
            _bookingService = bookingService;
            _mapper = mapper;
        }

        // GET: Admin/Booking
        public async Task<ActionResult> Index()
        {
            var bookings = await _bookingService.GetAllBookingsAsync();
            return View(_mapper.Map<IEnumerable<Booking>, IEnumerable<BookingViewModel>>(bookings));
        }

        public ActionResult _SaveBooking()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SaveBooking(BookingViewModel model)
        {
            var booking = _mapper.Map<BookingViewModel, Booking>(model);
            await _bookingService.CreateBookingAsync(booking);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> _UpdateBooking(int id)
        {
            var booking = await _bookingService.GetBookingByIdAsync(id);
            return PartialView(_mapper.Map<Booking, BookingViewModel>(booking));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateBooking(BookingViewModel model)
        {
            var booking = _mapper.Map<BookingViewModel, Booking>(model);
            await _bookingService.UpdateBookingAsync(booking);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> DeleteBooking(int id)
        {
            var booking = await _bookingService.GetBookingByIdAsync(id);
            await _bookingService.DeleteBookingAsync(booking);
            return RedirectToAction("Index");
        }

        public async Task<JsonResult> FillTimes(int peopleCount, int bookingPlace, string bookingDate)
        {
            var times = await _bookingService.GetAvailableTimesAsync(peopleCount, Convert.ToDateTime(bookingDate), (BookingPlace)bookingPlace);
            return Json(times.Select(x => x.ToString("HH:mm")), JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> Schedule()
        {
            var tables = await _bookingService.GetTablesAsync();
            TempData["Tables"] = _mapper.Map<IEnumerable<Table>, IEnumerable<TableViewModel>>(tables);
            return View();
        }

        public async Task<ActionResult> _BookingSchedule(string date, BookingPlace? place)
        {
            if (string.IsNullOrEmpty(date))
                date = DateTime.Now.ToString();

            var day = Convert.ToDateTime(date);
            TempData["Hours"] = _bookingService.GetTimePeriods(day);
            var bookings = await _bookingService.GetBookingsOfDayByPlaceAsync(day, place ?? BookingPlace.Terrace);

            return PartialView(_mapper.Map<IEnumerable<Booking>, IEnumerable<BookingScheduleViewModel>>(bookings));
        }

        public async Task<ActionResult> _PendingBookings(string date, BookingPlace? place)
        {
            if (string.IsNullOrEmpty(date))
                date = DateTime.Now.ToString();

            var day = Convert.ToDateTime(date);
            TempData["Hours"] = _bookingService.GetTimePeriods(day);
            var bookings = await _bookingService.GetBookingsOfDayByPlaceAsync(day, place ?? BookingPlace.Terrace);

            return PartialView(_mapper.Map<IEnumerable<Booking>, IEnumerable<PendingBookingViewModel>>(bookings));
        }

        public async Task<ActionResult> _SetTable(int id)
        {
            return PartialView();
        }
    }
}