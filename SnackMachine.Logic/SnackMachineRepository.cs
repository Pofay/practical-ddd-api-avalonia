using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            return context.SnackMachines.FirstOrDefault(x => x.Id == id);

        }

        protected override void SaveCore(DataContext context, SnackMachineEntity aggregateRoot)
        {
            var snackMachine = context.SnackMachines.FirstOrDefault(x => x.Id == aggregateRoot.Id);
            if (snackMachine == null)
            {
                context.SnackMachines.Add(aggregateRoot);
            }
            else
            {
                context.SnackMachines.Update(aggregateRoot);
            }
            context.SaveChanges();
        }
    }
}
