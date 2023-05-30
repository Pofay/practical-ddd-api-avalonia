using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SnackMachine.Logic.Persistence;

namespace SnackMachine.Logic
{
    public class SnackMachineRepository : Repository<SnackMachineEntity>
    {
        public SnackMachineRepository(DataContextFactory dataContextFactory) : base(dataContextFactory)
        {
        }

        protected override SnackMachineEntity GetByIdCore(DataContext context, Guid id)
        {
            return context
                .SnackMachines
                .Include(s => s.Slots)
                .ThenInclude(s => s.SnackPile.Snack)
                .FirstOrDefault(x => x.Id == id);
        }

        protected override void SaveCore(DataContext context)
        {
            // Answer from https://stackoverflow.com/questions/48630029/how-should-i-model-static-reference-data-with-entity-framework
            // This is a workaround to avoid EF Core to try to insert Snack entities 
            // of which are supposed to be static reference data.
            foreach (var e in context.ChangeTracker.Entries<Snack>())
            {
                e.State = EntityState.Unchanged;
            }
        }
    }
}
