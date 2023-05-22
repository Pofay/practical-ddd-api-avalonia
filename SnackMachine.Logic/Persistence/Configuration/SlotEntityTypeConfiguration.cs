using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SnackMachine.Logic.Persistence.Configuration
{
    public class SlotEntityTypeConfiguration : IEntityTypeConfiguration<Slot>
    {
        public void Configure(EntityTypeBuilder<Slot> builder)
        {
            builder.ToTable(nameof(Slot));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Position);
            builder.OwnsOne(x => x.SnackPile, navigationBuilder =>
            {
                navigationBuilder.Property(x => x.Quantity);
                navigationBuilder.Property(x => x.Price);
                navigationBuilder.HasOne(x => x.Snack).WithOne();
            });
        }
    }
}
