using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATM.Models
{
    public class Cassette
    {
        private Stack<Banknote> _banknotes;
        public Stack<Banknote> Banknotes
        {
            get { return _banknotes; }
            private set
            {
                if (value.Count > _maxCountOfBanknotes)
                    throw new ArgumentOutOfRangeException($"Count of Banknotes cannot be more than {_maxCountOfBanknotes}");
                _banknotes = value; 
            }
        }

        private int _countOfBanknotes;
        public int CountOfBanknotes
        {
            get { return _countOfBanknotes; }
            private set { _countOfBanknotes = value; }
        }

        private int _denominationOfBanknotes;
        public int DenominationOfBanknotes
        {
            get { return _denominationOfBanknotes; }
            private set { _denominationOfBanknotes = value; }
        }

        private const int _maxCountOfBanknotes = 2500;


        public Cassette(Stack<Banknote> banknotes)
        {
            Banknotes = banknotes;
            DenominationOfBanknotes = banknotes.FirstOrDefault().Denomination;
        }

        public void AddBanknote(Banknote banknote)
        {
            if (Banknotes is null)
                Banknotes = new Stack<Banknote>();

            if(_maxCountOfBanknotes < CountOfBanknotes)
                throw new ArgumentOutOfRangeException($"countOfBanknotes cannot be more than {_maxCountOfBanknotes}");

            Banknotes.Push(banknote);
            CountOfBanknotes++;

            if (DenominationOfBanknotes == default)
                DenominationOfBanknotes = banknote.Denomination;
        }

        public void RemoveBanknote(int countOfBanknotes)
        {
            if (Banknotes is null)
                Banknotes = new Stack<Banknote>();

            if (countOfBanknotes < 0)
                throw new ArgumentOutOfRangeException("countOfBanknotes cannot be less than 0");

            if (countOfBanknotes > Banknotes.Count)
                throw new ArgumentOutOfRangeException("Not enough banknotes to dispense");

            for (int i = 0; i < countOfBanknotes; i++)
            {
                Banknotes.Pop();
                CountOfBanknotes--;
            }
        }

    }
}
