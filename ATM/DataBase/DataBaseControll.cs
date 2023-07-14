using ATM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.DataBase
{
    public static class DataBaseControl
    {
        private static DataBaseContext _dbContext;

        static DataBaseControl()
        {
            _dbContext = new DataBaseContext();
            _dbContext.Database.EnsureCreated();
            _dbContext.Cassettes.Load();
        }

        public static IEnumerable<CassettesInfo> GetCountAndDenominationOfBanknotes()
        {
            return _dbContext.Cassettes;
        }

        public static IEnumerable<Cassette> GetCassettes()
        {
            var cassettes = new List<Cassette>();

            foreach (var item in _dbContext.Cassettes.Local.ToList())
            {
                var cassette = new Cassette(new Stack<Banknote>(), item.Denomination);

                for (int i = 0; i < item.CountOfBanknotes; i++)
                {
                    cassette.AddBanknote(new Banknote(item.Denomination));
                }

                cassettes.Add(cassette);
            }

            return cassettes;
        }

        public static void EditCassettes(CassettesInfo info)
        {
            var cassettes = _dbContext.Cassettes.Local.ToList();

            foreach (var item in cassettes)
            {
                if (item.Denomination == info.Denomination)
                {
                    item.CountOfBanknotes += info.CountOfBanknotes;
                    _dbContext.SaveChanges();
                    break;
                }
            }
        }

        public static void AddCassettes(CassettesInfo info)
        {
            _dbContext.Cassettes.Add(info);
            _dbContext.SaveChanges();
        }


        public static void DeleteCassettes(int denomination)
        {
            var cassettes = _dbContext.Cassettes.Local.ToList();

            foreach (var cassette in cassettes)
            {
                if (cassette.Denomination == denomination)
                {
                    if (cassette.CountOfBanknotes - 1 >= 0)
                        cassette.CountOfBanknotes--;
                }
            }
            _dbContext.SaveChanges();
        }

        public static void DeleteCassettes(int denomination, int countOfBanknotes)
        {
            var cassettes = _dbContext.Cassettes.Local.ToList();

            foreach (var cassette in cassettes)
            {
                if (cassette.Denomination == denomination)
                {
                    if (cassette.CountOfBanknotes - countOfBanknotes >= 0)
                        cassette.CountOfBanknotes -= countOfBanknotes;
                }
            }
            _dbContext.SaveChanges();
        }
    }
}
