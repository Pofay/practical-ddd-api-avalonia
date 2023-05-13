﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SnackMachine.Logic.Persistence.Configuration
{
    public class SnackEntityTypeConfiguration : IEntityTypeConfiguration<Snack>
    {
        public void Configure(EntityTypeBuilder<Snack> builder)
        {
            builder.ToTable(nameof(Snack));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name);
        }
    }
}
