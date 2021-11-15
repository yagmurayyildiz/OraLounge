using Domain.OraLounge.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.OraLounge.Areas.Admin.Models
{
    public class BookingViewModel
    {
        public int Id { get; set; }
        public BookingPlace BookingPlace { get; set; }
        public DateTime BookingTime { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public DateTime SelectedHour { get; set; }
        public decimal TotalAmount { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int PeopleCount { get; set; }
        public string Message { get; set; }
    }

    public class PostBookingViewModel
    {
        public int Id { get; set; }
        public BookingPlace BookingPlace { get; set; }
        public DateTime BookingTime { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public DateTime SelectedHour { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int PeopleCount { get; set; }
        public string Message { get; set; }
    }

    public class BookingFilterViewModel
    {
        public DateTime BookingDate { get; set; }
        public BookingPlace BookingPlace { get; set; }
    }

    public class BookingScheduleViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int PeopleCount { get; set; }
        public DateTime BookingTime { get; set; }
        public BookingPlace BookingPlace { get; set; }
        public ICollection<int> ReservedTables { get; set; }
    }

    public class PendingBookingViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int PeopleCount { get; set; }
        public string Message { get; set; }
    }

    public class SetTableViewModel
    {
        public int Id { get; set; }
        public List<int> TableIds { get; set; }
    }
}