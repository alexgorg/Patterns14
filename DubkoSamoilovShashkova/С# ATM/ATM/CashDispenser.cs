using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATM
{
    public class CashDispenser
    {
        public CashDispenser()
        {
            count = INITIAL_COUNT;
        }
        public void dispenseCash(int amount)
        {
            count -= amount;
        }
        public void depositCash(int amount)
        {
            count += amount;
        }
        public bool isCashAvailable(int amount)
        {
            if (count >= amount)
                return true;
            else
                return false;
        }
        private static int INITIAL_COUNT = 50000;
        private int count;
    }

}
