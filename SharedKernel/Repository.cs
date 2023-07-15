using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel
{
    public abstract class Repository<T> where T : AggregateRoot
    {
        public abstract T GetById(Guid id);

        public abstract void Save(T aggregateRoot);

        public abstract void Delete(T aggregateRoot);
    }
}
