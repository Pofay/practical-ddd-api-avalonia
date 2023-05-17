using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public T GetById(Guid id)
        {
            using (var context = ContextFactory.CreateDbContext(new string[] { Environment.GetEnvironmentVariable("DATABASE_URL") }))
            {
                return GetByIdCore(context, id);
            }
        }

        public void Save(T aggregateRoot)
        {
            using (var context = ContextFactory.CreateDbContext(new string[] { Environment.GetEnvironmentVariable("DATABASE_URL") }))
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    SaveCore(context, aggregateRoot);
                    transaction.Commit();
                }
            }
        }

        protected abstract T GetByIdCore(DataContext context, Guid id);
        protected abstract void SaveCore(DataContext context, T aggregateRoot);
    }
}
