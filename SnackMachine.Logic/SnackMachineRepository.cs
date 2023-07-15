using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using SnackMachine.Logic.Persistence;

namespace SnackMachine.Logic
{
    public class SnackMachineRepository : Repository<SnackMachineEntity>
    {
        private DbContextFactory ContextFactory { get; }

        public SnackMachineRepository(DbContextFactory dataContextFactory) 
        {
            this.ContextFactory= dataContextFactory;
        }

        public override SnackMachineEntity GetById(Guid id)
        {
            using (var context = ContextFactory.CreateDbContext(new string[] { Environment.GetEnvironmentVariable("DATABASE_URL") }))
            {
                return GetByIdCore(context, id);
            }
        }

        public override void Save(SnackMachineEntity aggregateRoot)
        {
            using (var context = ContextFactory.CreateDbContext(new string[] { Environment.GetEnvironmentVariable("DATABASE_URL") }))
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var aggregateRootFromDb = context.Set<SnackMachineEntity>().FirstOrDefault(x => x.Id == aggregateRoot.Id);
                    if (aggregateRootFromDb != null)
                    {
                        // From https://stackoverflow.com/questions/36856073/the-instance-of-entity-type-cannot-be-tracked-because-another-instance-of-this-t
                        context.Entry(aggregateRootFromDb).State = EntityState.Detached;
                        context.Set<SnackMachineEntity>().Update(aggregateRoot);
                        SaveCore(context);
                        context.SaveChanges();
                    }
                    else
                    {
                        context.Set<SnackMachineEntity>().Add(aggregateRoot);
                        SaveCore(context);
                        context.SaveChanges();
                    }
                    transaction.Commit();
                }
            }
        }

        public override void Delete(SnackMachineEntity aggregateRoot)
        {
            using (var context = ContextFactory.CreateDbContext(new string[] { Environment.GetEnvironmentVariable("DATABASE_URL") }))
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    context.Set<SnackMachineEntity>().Remove(aggregateRoot);
                    context.SaveChanges();
                    transaction.Commit();
                }
            }
        }

        protected SnackMachineEntity GetByIdCore(DbContext context, Guid id)
        {
            return context
                .Set<SnackMachineEntity>()
                .Include(s => s.Slots)
                .ThenInclude(s => s.SnackPile.Snack)
                .FirstOrDefault(x => x.Id == id);
        }

        protected void SaveCore(DbContext context)
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
