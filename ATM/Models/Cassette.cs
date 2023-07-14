using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATM.Models
{
    public class Cassette
    {
        private Stack<Banknote> _banknotes = new Stack<Banknote>();
        private Stack<Banknote> Banknotes
        {
            get { return _banknotes; }
            set
            {
                if (value.Count > _maxCountOfBanknotes)
                    throw new ArgumentOutOfRangeException($"Count of Banknotes cannot be more than {_maxCountOfBanknotes}");
                _banknotes = value; 
            }
        }

        public int CountOfBanknotes { get; private set; }

        public int? DenominationOfBanknotes { get; init; }

        private const int _maxCountOfBanknotes = 2500;


        public Cassette(Stack<Banknote> banknotes, int? denomination = default)
        {
            Banknotes = banknotes;
            DenominationOfBanknotes = denomination;
        }

        public void AddBanknote(Banknote banknote)
        {
            if (Banknotes is null)
                Banknotes = new Stack<Banknote>();

            if (DenominationOfBanknotes != null && DenominationOfBanknotes != banknote.Denomination)
                throw new FormatException();

            if(_maxCountOfBanknotes < CountOfBanknotes)
                throw new ArgumentOutOfRangeException($"countOfBanknotes cannot be more than {_maxCountOfBanknotes}");

            Banknotes.Push(banknote);
            CountOfBanknotes++;
        }

        public void RemoveBanknote(int countOfBanknotes)
        {
            if (Banknotes is null)
                Banknotes = new Stack<Banknote>();

            if (countOfBanknotes < 0)
                throw new ArgumentOutOfRangeException("countOfBanknotes cannot be less than 0");

            if (countOfBanknotes > CountOfBanknotes)
                throw new ArgumentOutOfRangeException("Not enough banknotes to dispense");

            for (int i = 0; i < countOfBanknotes; i++)
            {
                Banknotes.Pop();
                CountOfBanknotes--;
            }
        }

    }
}
