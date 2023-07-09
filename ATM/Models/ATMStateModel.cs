using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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

        private static Cassette _cassettForUsers;
        public static Cassette CassettForUsers
        {
            get { return _cassettForUsers; }
            private set{ _cassettForUsers = value; }
        }

        private static List<CassettesInfo> _countAndDenominationOfBanknotes;
        public static List<CassettesInfo> CountAndDenominationOfBanknotes
        {
            get { return _countAndDenominationOfBanknotes; }
            private set { _countAndDenominationOfBanknotes = value; }
        }

        public static int CountOfCassettes => Cassettes.Count;


        static ATMStateModel()
        {
            //TODO: Получать данные из DB

            Cassettes = new Stack<Cassette>();

            FillCassettesRandomData();

            _countAndDenominationOfBanknotes = GetCountAndDenominationOfBanknotes(); 
        }

        #region Удалить

        public static List<CassettesInfo> GetCountAndDenominationOfBanknotes()
        {
            var cassettes = new List<CassettesInfo>();

            foreach (var cassette in Cassettes)
            {
                cassettes.Add(new CassettesInfo { Denomination = cassette.DenominationOfBanknotes, CountOfBanknotes = cassette.CountOfBanknotes });
            }

            return cassettes;
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

#endregion

        private const int _maxCountOfCassettes = 6;

        public static long GetAllMoney()
        {
            var result = default(long);

            foreach (var cassette in Cassettes)
            {
                if(cassette.CountOfBanknotes > 0)
                    result += cassette.Banknotes.Peek().Denomination * cassette.CountOfBanknotes;
            }

            return result;
        }

        public static void AddBanknoteForUsers(Banknote banknote)
        {
            if (CassettForUsers is null)
                CassettForUsers = new Cassette(new Stack<Banknote>());

            CassettForUsers.AddBanknote(banknote);

            var userCard = UserAuthorization.GetAutorizatedCard();
            userCard.AddMoneyToUsersCard(banknote.Denomination);
        }

        /// <summary>
        /// Get banknotes from user (For Debuging)
        /// </summary>
        /// <returns></returns>
        public static List<CassettesInfo> GetBanknotesFromUser()
        {
            var denominations = new HashSet<int>();
            denominations.Add(5000);
            denominations.Add(2000);
            denominations.Add(1000);
            denominations.Add(500);
            denominations.Add(100);
            denominations.Add(50);

            var banknotes = new List<CassettesInfo>();

            foreach (var item in denominations)
            {
                banknotes.Add(new CassettesInfo() { Denomination = item, CountOfBanknotes = new Random().Next(1, 10) });
            }

            return banknotes;
        }

        public static void RemoveBanknoteByDenomination(int denomination)
        {
            if (Cassettes is null)
                throw new ArgumentNullException();

            foreach (var cassett in Cassettes)
            {
               var tmp = cassett.Banknotes.FirstOrDefault(f => f.Denomination == denomination);

                if (tmp.Denomination != 0)
                {
                    cassett.RemoveBanknote(1);
                    var userCard = UserAuthorization.GetAutorizatedCard();
                    userCard.RemoveMoneyFromUsersCard(denomination);
                    _countAndDenominationOfBanknotes = GetCountAndDenominationOfBanknotes();
                    break;
                }
            }
        }

        public static bool IsDenominationsExist(int denomination)
        {
           return _countAndDenominationOfBanknotes.First(f => f.Denomination == denomination).CountOfBanknotes > 0;
        }

        public static List<CassettesInfo> ConvertSumToBanknotes(long sum)
        {
            if(_countAndDenominationOfBanknotes.Count == 0)
                _countAndDenominationOfBanknotes = GetCountAndDenominationOfBanknotes();

            var denominations = new HashSet<int>();
            denominations.Add(5000);
            denominations.Add(2000);
            denominations.Add(1000);
            denominations.Add(500);
            denominations.Add(100);
            denominations.Add(50);

            var banknotes = new List<CassettesInfo>();

            foreach (var item in denominations)
            {
                banknotes.Add(new CassettesInfo() { Denomination = item, CountOfBanknotes = _countAndDenominationOfBanknotes.First(s => s.Denomination == item).CountOfBanknotes });
            }

            CassettesInfo tmp;

            while (sum > denominations.Last())
            {
                if (banknotes.Last().CountOfBanknotes == 0)
                    throw new ArgumentOutOfRangeException("Not enough banknots");

                foreach (var denomination in denominations)
                {
                    tmp = banknotes.First(s => s.Denomination == denomination);

                    if (sum - denomination >= 0 && tmp.CountOfBanknotes > 0)
                    {
                        sum -= denomination;
                        banknotes.First(s => s.Denomination == denomination).CountOfBanknotes--;
                        break;
                    }
                }
            }

            CassettesInfo currentItem;
            foreach (var item in _countAndDenominationOfBanknotes)
            {
                currentItem = banknotes.Find(f => f.Denomination == item.Denomination);
                currentItem.CountOfBanknotes = Math.Abs(item.CountOfBanknotes - currentItem.CountOfBanknotes);
            }

            return banknotes.Where(s => s.CountOfBanknotes != 0).ToList();
        }
    }
}
