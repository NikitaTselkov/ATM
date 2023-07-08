using System;
using System.Collections.Generic;
using System.Text;

namespace ATM.Models
{
    public struct Banknote
    {
        /// <summary>
        /// Номинал.
        /// </summary>
        public int Denomination { get; init; }

        public Banknote(int denomination)
        {
            Denomination = denomination;
        }
    }
}
