using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SnackMachine.Logic.Persistence
{
    public class DataContext : DbContext
    {
        public DbSet<SnackMachineEntity> SnackMachines { get; set; }

        public DataContext(DbContextOptions options) : base(options) { }
    }


}
