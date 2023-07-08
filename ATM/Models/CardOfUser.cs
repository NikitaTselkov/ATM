using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Models
{
    public class CardOfUser
    {
        public int Number { get; init; }
        public int Pincode { get; init; }
        public int Balance { get; private set; }

        public CardOfUser(int number, int pincode)
        {
            Number = number;
            Pincode = pincode; //UserAuthorization.AutorizationOfUsersCard();
        }

        public void SetMoneyToUsersCard(int money)
        {
            Balance += money;
        }
    }
}
