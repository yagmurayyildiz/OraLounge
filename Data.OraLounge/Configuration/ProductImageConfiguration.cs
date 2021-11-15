using Domain.OraLounge.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.OraLounge.Configuration
{
    internal class ProductImageConfiguration : EntityTypeConfiguration<ProductImage>
    {
        internal ProductImageConfiguration()
        {
            Property(x => x.Source).IsRequired();
        }
    }
}
