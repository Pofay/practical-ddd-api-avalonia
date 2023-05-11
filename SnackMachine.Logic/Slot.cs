using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnackMachine.Logic
{
    public class Slot : Entity
    {
        public SnackPile SnackPile { get; set; }
        public SnackMachineEntity SnackMachine { get; private set; }
        public int Position { get; private set; }

        private Slot() { }

        public Slot(SnackMachineEntity snackMachine, int position)
        {
            SnackMachine = snackMachine;
            Position = position;
            SnackPile = new SnackPile(null, 0, 0m);
        }

        public Slot(SnackMachineEntity snackMachine, int position, SnackPile snackPile) : this()
        {
            SnackMachine = snackMachine;
            Position = position;
            SnackPile = snackPile;
        }
    }
}
