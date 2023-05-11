using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnackMachine.Logic
{
    public class Snack : AggregateRoot
    {
        public string Name { get; private set; }
        private Snack() { }

        public Snack(string name) : this()
        {
            Name = name;
        }
    }
}
