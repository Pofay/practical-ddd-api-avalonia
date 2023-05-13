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
            // To Implement
            // Need to Add a Id to the Slot's current constructor
            // Before implementing this.
        }
    }
}
