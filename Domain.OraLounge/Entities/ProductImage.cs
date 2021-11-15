using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.OraLounge.Entities
{
    public class ProductImage : BaseEntity
    {
        public string Source { get; set; }
        public int Priority { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
