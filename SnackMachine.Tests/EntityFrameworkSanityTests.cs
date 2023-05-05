using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using SnackMachine.Logic;
using SnackMachine.Logic.Persistence;

namespace SnackMachine.Tests
{
    public class EntityFrameworkSanityTests
    {

        [Fact]
        public async void ShouldReturnSavedSnackMachine()
        {
            var id = Guid.NewGuid();
            var expected = new SnackMachineEntity(id);

            using (var context = new DataContextFactory().CreateDbContext(new string[] { "Server=localhost; Port=5432; User Id=postgres; Password=postgres; Database=practical_ddd_db; CommandTimeout=20;" }))
            {
                context.SnackMachines.Add(expected);
                await context.SaveChangesAsync();

                var actual = await context.SnackMachines.FindAsync(id);
                Assert.NotNull(actual);
                Assert.Equal(actual, expected);

                context.SnackMachines.Remove(actual);
                await context.SaveChangesAsync();
            }
        }
    }
}
