using Domain.OraLounge.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.OraLounge.Models
{
    public class BookingViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        public int PeopleCount { get; set; }
        public DateTime BookingTime { get; set; }
        public List<DateTime> BookingHours { get; set; }
        public DateTime SelectedTime { get; set; }
        public string Message { get; set; }
        public BookingPlace BookingPlace { get; set; }
    }
}