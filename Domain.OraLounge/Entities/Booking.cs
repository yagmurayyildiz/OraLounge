using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.OraLounge.Entities
{
    public class Booking : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int PeopleCount { get; set; }
        public DateTime BookingTime { get; set; }
        public string Message { get; set; }
        public BookingPlace BookingPlace { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public string StripeSessionId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public ICollection<Table> ReservedTables { get; set; }
    }

    public enum BookingPlace
    {
        Terrace,
        Restaurant        
    }

    public enum PaymentStatus
    {
        Pending,
        Paid,
        Canceled
    }
}
