using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedKernel;

namespace SnackMachine.Logic
{
    public class Snack : AggregateRoot
    {
        public static readonly Snack None = new Snack(Guid.Parse("c0b1e9a0-0d6a-4b9a-9e5a-6e6b6d9e0e1a"), "None");
        public static readonly Snack Chocolate = new Snack(Guid.Parse("08b96bd6-29db-4089-8e0e-7feb9fab8666"), "Chocolate");
        public static readonly Snack Soda = new Snack(Guid.Parse("99454277-68ae-44be-9648-25da7644bb02"), "Soda");
        public static readonly Snack Gum = new Snack(Guid.Parse("e7b52ab8-1552-4187-bdc8-15e63ac3502a"), "Gum");

        public string Name { get; private set; }

        private Snack(Guid id)
        {
            this.Id = id;
        }

        private Snack(Guid id, string name) : this(id)
        {
            Name = name;
        }
    }
}
