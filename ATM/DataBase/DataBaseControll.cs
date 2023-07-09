using ATM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.DataBase
{
    public static class DataBaseControll
    {
        private static DataBaseContext _dbContext;

        static DataBaseControll()
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
            var cassette = new Cassette(new Stack<Banknote>());

            var t = _dbContext.Cassettes.Local.ToList();

            foreach (var item in t)
            {
                for (int i = 0; i < item.CountOfBanknotes; i++)
                {
                    cassette.AddBanknote(new Banknote(item.Denomination));
                }

                cassettes.Add(cassette);
                cassette = new Cassette(new Stack<Banknote>());
            }

            return cassettes;
        }

        public static void AddCassettes(CassettesInfo info)
        {
            _dbContext.Cassettes.Add(info);
            _dbContext.SaveChanges();
        }


        public static void DeleteCassettes(int denomination)
        {
            var cassettes = _dbContext.Cassettes.Local.ToList();

            foreach (var cassett in cassettes)
            {
                if (cassett.Denomination == denomination)
                {
                    if (cassett.CountOfBanknotes - 1 > 0)
                        cassett.CountOfBanknotes--;
                }
            }
            _dbContext.SaveChanges();
        }

        public static void DeleteCassettes(int denomination, int countOfBanknotes)
        {
            var cassettes = _dbContext.Cassettes.Local.ToList();

            foreach (var cassett in cassettes)
            {
                if (cassett.Denomination == denomination)
                {
                    if (cassett.CountOfBanknotes - countOfBanknotes > 0)
                        cassett.CountOfBanknotes -= countOfBanknotes;
                }
            }
            _dbContext.SaveChanges();
        }
    }
}
