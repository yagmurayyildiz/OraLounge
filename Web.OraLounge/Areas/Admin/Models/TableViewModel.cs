using Domain.OraLounge.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.OraLounge.Areas.Admin.Models
{
    public class TableViewModel
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public int Capacity { get; set; }
        public BookingPlace TablePlace { get; set; }
    }
}