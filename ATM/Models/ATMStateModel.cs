using ATM.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ATM.Models
{
    public static class ATMStateModel
    {
        private static Stack<Cassette> _cassettes;
        private static Stack<Cassette> Cassettes
        {
            get { return _cassettes; }
            set
            {
                if (value.Count > _maxCountOfCassettes)
                    throw new ArgumentOutOfRangeException($"Count of Cassettes cannot be more than {_maxCountOfCassettes}");
                _cassettes = value;
            }
        }

        public static Cassette CassetteForUsers { get; private set; }

        private static List<CassettesInfo> _countAndDenominationOfBanknotes { get; set; }

        public static int CountOfCassettes => Cassettes.Count;

        public static readonly HashSet<int> Denominations = new() { 50, 100, 500, 1000, 2000, 5000 };

        private const int _maxCountOfCassettes = 6;

        static ATMStateModel()
        {
            Cassettes = new Stack<Cassette>(DataBaseControl.GetCassettes());

            if (Cassettes.Count == 0)
                InitCassettes();

            _countAndDenominationOfBanknotes = DataBaseControl.GetCountAndDenominationOfBanknotes().ToList();

            if (_countAndDenominationOfBanknotes.Count == 0)
                InitCountAndDenominationOfBanknotes();
        }

        public static List<CassettesInfo> GetCountAndDenominationOfBanknotes()
        {         
            return new List<CassettesInfo>(_countAndDenominationOfBanknotes);
        }

        public static long GetAllMoney()
        {
            var result = default(long);

            foreach (var cassette in Cassettes)
            {
                if (cassette.CountOfBanknotes > 0)
                    result +=  (long)cassette.DenominationOfBanknotes * cassette.CountOfBanknotes;
            }

            return result;
        }

        public static void AddBanknote(Banknote banknote)
        {
            foreach (var cassette in Cassettes)
            {
                if (cassette.DenominationOfBanknotes == banknote.Denomination)
                {
                    cassette.AddBanknote(banknote);
                }
            }

            _countAndDenominationOfBanknotes = DataBaseControl.GetCountAndDenominationOfBanknotes().ToList();
        }

        public static void AddBanknoteForUsers(Banknote banknote)
        {
            if (CassetteForUsers is null)
                CassetteForUsers = new Cassette(new Stack<Banknote>());

            CassetteForUsers.AddBanknote(banknote);

            var userCard = UserAuthorization.GetAutorizatedCard();
            userCard.AddMoneyToUsersCard(banknote.Denomination);
        }

        /// <summary>
        /// Get banknotes from user (For Debugging)
        /// </summary>
        /// <returns></returns>
        public static List<CassettesInfo> GetBanknotesFromUser()
        {
            var banknotes = new List<CassettesInfo>();

            foreach (var item in Denominations)
            {
                banknotes.Add(new CassettesInfo() { Denomination = item, CountOfBanknotes = new Random().Next(1, 10) });
            }

            return banknotes;
        }

        public static void RemoveBanknoteByDenomination(int denomination)
        {
            if (Cassettes is null)
                throw new ArgumentNullException();

            var cassette = Cassettes.SingleOrDefault(f => f.DenominationOfBanknotes == denomination);

            if (cassette != null)
            {
                cassette.RemoveBanknote(1);
                var userCard = UserAuthorization.GetAutorizatedCard();
                userCard.RemoveMoneyFromUsersCard(denomination);
                _countAndDenominationOfBanknotes = DataBaseControl.GetCountAndDenominationOfBanknotes().ToList();
                DataBaseControl.DeleteCassettes(denomination);
            }
            
        }

        public static bool IsDenominationsEnough(int denomination, long sum)
        {
            var tmp = _countAndDenominationOfBanknotes.First(f => f.Denomination == denomination);

            return tmp.Denomination * tmp.CountOfBanknotes >= sum;
        }

        public static bool IsDenominationsExist(int denomination)
        {
            return _countAndDenominationOfBanknotes.First(f => f.Denomination == denomination).CountOfBanknotes > 0;
        }

        public static List<CassettesInfo> ConvertSumToBanknotes(long sum)
        {
            if (_countAndDenominationOfBanknotes.Count == 0)
                _countAndDenominationOfBanknotes = DataBaseControl.GetCountAndDenominationOfBanknotes().ToList();

            var banknotes = Denominations.Select(item => new CassettesInfo() { Denomination = item, CountOfBanknotes = _countAndDenominationOfBanknotes.Single(s => s.Denomination == item).CountOfBanknotes }).ToList();
            CassettesInfo tmp;
            long previousSum = 0;

            while (sum >= Denominations.Min() && previousSum != sum)
            {
                if (banknotes.All(a => a.CountOfBanknotes == 0))
                    throw new ArgumentOutOfRangeException("Not enough banknotes");

                previousSum = sum;

                foreach (var denomination in Denominations.OrderByDescending(o => o))
                {
                    tmp = banknotes.Single(s => s.Denomination == denomination);

                    if (sum - denomination >= 0 && tmp.CountOfBanknotes > 0)
                    {
                        sum -= denomination;
                        banknotes.First(s => s.Denomination == denomination).CountOfBanknotes--;
                        break;
                    }
                }
            }

            if (sum != 0) return new List<CassettesInfo>();

            CassettesInfo currentItem;
            foreach (var item in _countAndDenominationOfBanknotes)
            {
                currentItem = banknotes.Find(f => f.Denomination == item.Denomination);
                currentItem.CountOfBanknotes = Math.Abs(item.CountOfBanknotes - currentItem.CountOfBanknotes);
            }

            return banknotes.Where(s => s.CountOfBanknotes != 0).ToList();
        }

        private static void InitCassettes()
        {
            foreach (var item in Denominations)
            {
                var cassette = new Cassette(new Stack<Banknote>(), item)
                {
                    DenominationOfBanknotes = item
                };
                Cassettes.Push(cassette);
            }
        }

        private static void InitCountAndDenominationOfBanknotes()
        {
            foreach (var item in Denominations)
            {
                var info = new CassettesInfo() { Denomination = item, CountOfBanknotes = 0 };
                _countAndDenominationOfBanknotes.Add(info);
                DataBaseControl.AddCassettes(info);
            }
        }
    }
}
