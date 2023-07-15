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
        public abstract T GetById(Guid id);

        public abstract void Save(T aggregateRoot);

        public abstract void Delete(T aggregateRoot);

        protected abstract void SaveCore(DbContext context);

        protected abstract T GetByIdCore(DbContext context, Guid id);
    }
}
