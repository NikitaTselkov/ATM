using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Models
{
    public static class UserAuthorization
    {
        private static CardOfUser _authorizatedUsersCard;

        public static CardOfUser AuthorizationOfUsersCard(long number, short passwords)
        {
            var user = new CardOfUser(number, passwords);
            user.AddMoneyToUsersCard(new Random().Next(10, 1000000));

            _authorizatedUsersCard = user;

            return user;
        }

        public static CardOfUser GetAutorizatedCard()
        {
            return _authorizatedUsersCard;
        }

        public static long GetBalance()
        {
            return _authorizatedUsersCard.Balance;
        }

        public static bool IsUsersCardAuthorized()
        {
            return _authorizatedUsersCard is not null;
        }
    }
}
