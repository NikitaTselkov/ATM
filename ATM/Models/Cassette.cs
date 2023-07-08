using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATM.Models
{
    public class Cassette
    // 4 - 6 в 1 кассете 2500 банкнот в каждую касету можно добавить касету только своего номинала
    // Банкомат сохраняет информацию по каждой операции
    // Дополнительно банкомат пересчитывает все купюры перед выдачей
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


        private const int _maxCountOfBanknotes = 2500;


        public Cassette(Stack<Banknote> banknotes)
        {
            Banknotes = banknotes;
        }


        public void AddBanknote(Banknote banknote)
        {
            if (Banknotes is null)
                Banknotes = new Stack<Banknote>();

            if (Banknotes.Count > 0 && Banknotes.First().Denomination != banknote.Denomination)
                throw new FormatException("Banknotes must be of the same denomination");

            Banknotes.Push(banknote);
            CountOfBanknotes++;
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
