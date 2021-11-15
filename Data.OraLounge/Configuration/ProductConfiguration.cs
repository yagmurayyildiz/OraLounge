using Domain.OraLounge.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.OraLounge.Configuration
{
    internal class ProductConfiguration : EntityTypeConfiguration<Product>
    {
        internal ProductConfiguration()
        {
            Property(x => x.Name).IsRequired()
                .HasMaxLength(50);

            Property(x => x.Price).HasPrecision(18, 2);

            HasMany(x => x.Images)
                .WithRequired(x => x.Product)
                .HasForeignKey(x => x.ProductId)
                .WillCascadeOnDelete(true);
        }
    }
}
