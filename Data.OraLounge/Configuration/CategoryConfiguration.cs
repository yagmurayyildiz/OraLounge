using Domain.OraLounge.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.OraLounge.Configuration
{
    internal class CategoryConfiguration : EntityTypeConfiguration<Category>
    {
        internal CategoryConfiguration()
        {
            Property(x => x.Name).IsRequired()
                .HasMaxLength(50);

            HasMany(x => x.Products)
                .WithRequired(x => x.Category)
                .HasForeignKey(x => x.CategoryId)
                .WillCascadeOnDelete(true);
        }
    }
}
