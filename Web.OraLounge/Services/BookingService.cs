using Domain.OraLounge;
using Domain.OraLounge.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Web.OraLounge.Services.Interfaces;

namespace Web.OraLounge.Services
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<List<Booking>> GetBookingsOfDayByPlaceAsync(DateTime day, BookingPlace place)
        {
            return _unitOfWork.BookingRepository.GetBookingsByDateAndPlace(day, place);
        }

        public async Task<List<DateTime>> GetAvailableTimesAsync(int peopleCount, DateTime day, BookingPlace place)
        {
            if (day.Date < DateTime.Now.Date || (day.Date == DateTime.Now.Date && DateTime.Now.Hour >= BookingConstants.LastTimeToBookForSameDayAsHour))
                return new List<DateTime>();

            var availableTimes = GetTimePeriods(day);
            var bookingsOfDay = await _unitOfWork.BookingRepository.GetBookingsByDateAndPlace(day, place);

            if (bookingsOfDay == null || bookingsOfDay.Count() == 0)
                return availableTimes;

            int cntPeopleIn = peopleCount;
            var leavingTimes = new List<Tuple<int, DateTime>>();
            foreach (var item in bookingsOfDay)
            {
                cntPeopleIn += item.PeopleCount;
                leavingTimes.Add(new Tuple<int, DateTime>(item.PeopleCount, item.BookingTime.AddHours(BookingConstants.MaxStayingTimeAsHours)));

                if (leavingTimes.Any(x => x.Item2 == item.BookingTime))
                    cntPeopleIn -= leavingTimes.Where(x => x.Item2 == item.BookingTime).Sum(x => x.Item1);

                if (cntPeopleIn > BookingConstants.MaxCapacity)
                {
                    var fullTimes = availableTimes.Where(x => x > item.BookingTime.AddHours(-1*BookingConstants.MaxStayingTimeAsHours) && x < item.BookingTime.AddHours(BookingConstants.MaxStayingTimeAsHours)).ToList();
                    foreach (var time in fullTimes)
                    {
                        availableTimes.Remove(time);
                    }
                }
            }

            return availableTimes;
        }

        public List<DateTime> GetTimePeriods(DateTime day)
        {
            var periodsOfDay = new List<DateTime>();
            var startTime = new DateTime(day.Year, day.Month, day.Day, BookingConstants.StartTimeToBookAsHour, 0, 0);
            var endTime = GetLastBookingTime(day);
            for (DateTime time = startTime; time <= endTime; time = time.AddMinutes(BookingConstants.TimePeriodRangeAsMin))
            {
                periodsOfDay.Add(time);
            }
            return periodsOfDay;
        }

        private DateTime GetLastBookingTime(DateTime day)
        {
            if (day.DayOfWeek == DayOfWeek.Friday || day.DayOfWeek == DayOfWeek.Saturday)
            {
                day = day.AddDays(1);
                return new DateTime(day.Year, day.Month, day.Day, BookingConstants.LastBookableTimeAtWeekendAsHour, 0, 0);
            }
            else
            {
                return new DateTime(day.Year, day.Month, day.Day, BookingConstants.LastBookableTimeOnWeekdaysAsHour, 0, 0);
            }
        }

        public Task<List<Table>> GetTablesAsync()
        {
            return _unitOfWork.TableRepository.GetAllAsync();
        }

        public Task<List<Booking>> GetAllBookingsAsync()
        {
            return _unitOfWork.BookingRepository.GetAllAsync();
        }

        public Task<Booking> GetBookingByIdAsync(int id)
        {
            return _unitOfWork.BookingRepository.FindByIdAsync(id);
        }

        public Task CreateBookingAsync(Booking booking)
        {
            booking.TotalAmount = booking.PeopleCount * BookingConstants.PricePerPerson;

            _unitOfWork.BookingRepository.Add(booking);
            return _unitOfWork.SaveChangesAsync();
        }
        public Task UpdateBookingAsync(Booking booking)
        {
            _unitOfWork.BookingRepository.Update(booking);
            return _unitOfWork.SaveChangesAsync();
        }

        public Task DeleteBookingAsync(Booking booking)
        {
            _unitOfWork.BookingRepository.Remove(booking);
            return _unitOfWork.SaveChangesAsync();
        }
    }

    public static class BookingConstants
    {
        public const int StartTimeToBookAsHour = 14;              //14:00
        public const int LastBookableTimeAtWeekendAsHour = 1;     //01:00
        public const int LastBookableTimeOnWeekdaysAsHour = 23;   //23:00
        public const int TimePeriodRangeAsMin = 15;              //minutes
        public const int LastTimeToBookForSameDayAsHour = 20;     //20:00
        public const int MaxStayingTimeAsHours = 2;                //hours
        public const int MaxCapacity = 60;
        public const int MaxPeopleCount = 15;
        public const decimal PricePerPerson = 10;           //GBP
    }
}