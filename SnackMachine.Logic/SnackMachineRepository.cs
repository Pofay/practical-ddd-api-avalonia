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
    }
}
