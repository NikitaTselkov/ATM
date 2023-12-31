﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Models
{
    public static class UserAuthorization
    {
        private static CardOfUser _autorizatedUsersCard;

        public static CardOfUser AutorizationOfUsersCard(long number, short passwprd)
        {
            var user = new CardOfUser(number, passwprd);
            user.AddMoneyToUsersCard(new Random().Next(10, 1000000));

            _autorizatedUsersCard = user;

            return user;
        }

        public static CardOfUser GetAutorizatedCard()
        {
            return _autorizatedUsersCard;
        }

        public static long GetBalance()
        {
            return _autorizatedUsersCard.Balance;
        }

        public static bool IsUsersCardAuthorized()
        {
            return _autorizatedUsersCard is not null;
        }
    }
}
