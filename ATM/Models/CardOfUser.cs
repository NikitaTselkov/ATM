using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Models
{
    public class CardOfUser
    {
        public long Number { get; init; }
        public short Pincode { get; init; }
        public decimal Balance { get; private set; }

        public CardOfUser(long number, short pincode)
        {
            var length = number.ToString().Length;

            if (length > 16 || length < 16)
                throw new FormatException();

            Number = number;
            Pincode = pincode;
        }

        public void SetMoneyToUsersCard(decimal money)
        {
            Balance += money;
        }
    }
}
