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
        private DataContextFactory ContextFactory { get; }

        public Repository(DataContextFactory dataContextFactory)
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

        protected abstract void SaveCore(DataContext context);

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
        protected abstract T GetByIdCore(DataContext context, Guid id);
    }
}
