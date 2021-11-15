using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.OraLounge.Entities
{
    public class Table : BaseEntity
    {
        public string Number { get; set; }
        public int Capacity { get; set; }
        public BookingPlace TablePlace { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
