﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedKernel;

namespace SnackMachine.Logic
{
    public class Slot : Entity
    {
        public SnackPile SnackPile { get; set; }
        public SnackMachineEntity SnackMachine { get; private set; }
        public int Position { get; private set; }

        private Slot(Guid id)
        {
            this.Id = id;
        }

        public Slot(SnackMachineEntity snackMachine, int position) : this(Guid.NewGuid())
        {
            SnackMachine = snackMachine;
            Position = position;
            SnackPile = SnackPile.Empty;
        }

        public Slot(SnackMachineEntity snackMachine, int position, SnackPile snackPile) : this(Guid.NewGuid())
        {
            SnackMachine = snackMachine;
            Position = position;
            SnackPile = snackPile;
        }
    }
}
