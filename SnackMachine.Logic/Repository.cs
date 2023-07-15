using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using SnackMachine.Logic.Persistence;

namespace SnackMachine.Logic
{
    public abstract class Repository<T> where T : AggregateRoot
    {
        private DbContextFactory ContextFactory { get; }

        public Repository(DbContextFactory dataContextFactory)
        {
            this.ContextFactory = dataContextFactory;
        }

        public virtual T GetById(Guid id)
        {
            using (var context = ContextFactory.CreateDbContext(new string[] { Environment.GetEnvironmentVariable("DATABASE_URL") }))
            {
                return GetByIdCore(context, id);
            }
        }


        public virtual void Save(T aggregateRoot)
        {
            using (var context = ContextFactory.CreateDbContext(new string[] { Environment.GetEnvironmentVariable("DATABASE_URL") }))
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var aggregateRootFromDb = context.Set<T>().FirstOrDefault(x => x.Id == aggregateRoot.Id);
                    if (aggregateRootFromDb != null)
                    {
                        // From https://stackoverflow.com/questions/36856073/the-instance-of-entity-type-cannot-be-tracked-because-another-instance-of-this-t
                        context.Entry(aggregateRootFromDb).State = EntityState.Detached;
                        context.Set<T>().Update(aggregateRoot);
                        SaveCore(context);
                        context.SaveChanges();
                    }
                    else
                    {
                        context.Set<T>().Add(aggregateRoot);
                        SaveCore(context);
                        context.SaveChanges();
                    }
                    transaction.Commit();
                }
            }
        }

        protected abstract void SaveCore(DbContext context);

        public virtual void Delete(T aggregateRoot)
        {
            using (var context = ContextFactory.CreateDbContext(new string[] { Environment.GetEnvironmentVariable("DATABASE_URL") }))
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    context.Set<T>().Remove(aggregateRoot);
                    context.SaveChanges();
                    transaction.Commit();
                }
            }
        }
        protected abstract T GetByIdCore(DbContext context, Guid id);
    }
}
