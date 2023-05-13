using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SnackMachine.Logic.Persistence.Configuration;

namespace SnackMachine.Logic.Persistence
{
    public class DataContext : DbContext
    {
        public DbSet<SnackMachineEntity> SnackMachines { get; set; }

        public DataContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var persistenceAssembly = typeof(SnackMachineEntityTypeConfiguration).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(persistenceAssembly);
        }
    }

    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var connectionString = args[0];
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            Console.WriteLine("DATABASE_URL: " + connectionString);
            optionsBuilder.UseNpgsql(connectionString);
            return new DataContext(optionsBuilder.Options);
        }
    }
}
