using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Models
{
    public static class ATMStateModel
    {
        private static Stack<Cassette> _cassettes;
        public static Stack<Cassette> Cassettes
        {
            get { return _cassettes; }
            private set
            {
                if (value.Count > _maxCountOfCassettes)
                    throw new ArgumentOutOfRangeException($"Count of Cassettes cannot be more than {_maxCountOfCassettes}");
                _cassettes = value;
            }
        }

        public static int CountOfCassettes => Cassettes.Count;

        static ATMStateModel()
        {
            //TODO: Получать данные из DB

            Cassettes = new Stack<Cassette>();

            FillCassettesRandomData();
        }

        private static void FillCassettesRandomData()
        {
            var t = 0;

            for (int i = 0; i < 6; i++)
            {
                var cassette = new Cassette(new Stack<Banknote>());

                if (t == 0)
                {
                    t = 50;
                }
                else if (t == 50)
                {
                    t = 100;
                }
                else if (t == 100)
                {
                    t = 500;
                }
                else if (t == 500)
                {
                    t = 1000;
                }
                else if (t == 1000)
                {
                    t = 2000;
                }
                else if (t == 2000)
                {
                    t = 5000;
                }

                for (int j = 0; j < new Random().Next(1000, 2500); j++)
                {
                    cassette.AddBanknote(new Banknote(t));
                }


                Cassettes.Push(cassette);
            }
        }

        private const int _maxCountOfCassettes = 6;

        public static long GetAllMoney()
        {
            var result = default(long);

            foreach (var cassette in Cassettes)
            {
                result += cassette.Banknotes.Peek().Denomination * cassette.CountOfBanknotes;
            }

            return result;
        }
    }
}
