using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Models
{
    public static class UserAuthorization
    {
        public static CardOfUser AutorizatedUsersCard { get; private set; }

        public static CardOfUser AutorizationOfUsersCard()
        {
            var user = new CardOfUser(1253453633, 3245);
            user.SetMoneyToUsersCard(53245);

            AutorizatedUsersCard = user;

            return user;
        }

        public static bool IsUsersCardAuthorized()
        {
            return true;
        }
    }
}
