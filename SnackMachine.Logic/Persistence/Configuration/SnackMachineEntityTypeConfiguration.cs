using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SnackMachine.Logic.Persistence.Configuration
{
    public class SnackMachineEntityTypeConfiguration : IEntityTypeConfiguration<SnackMachineEntity>
    {
        public void Configure(EntityTypeBuilder<SnackMachineEntity> builder)
        {
            builder.ToTable("SnackMachine");
            builder.HasKey(x => x.Id);

            builder.OwnsOne(s => s.MoneyInside,
                navigationBuilder =>
                {
                    navigationBuilder.Property(m => m.OneCentCount).HasColumnName("OneCentCount").IsRequired();
                    navigationBuilder.Property(m => m.TenCentCount).HasColumnName("TenCentCount").IsRequired();
                    navigationBuilder.Property(m => m.QuarterCount).HasColumnName("QuarterCount").IsRequired();
                    navigationBuilder.Property(m => m.OneDollarCount).HasColumnName("OneDollarCount").IsRequired();
                    navigationBuilder.Property(m => m.FiveDollarCount).HasColumnName("FiveDollarCount").IsRequired();
                    navigationBuilder.Property(m => m.TwentyDollarCount).HasColumnName("TwentyDollarCount").IsRequired();
                });
            builder.Ignore(s => s.MoneyInTransaction);

            builder.HasMany(s => s.Slots)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
