using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATM
{
    public class Account
    {
        public Account(int cardNumber, int thePIN, double theTotalBalance)
        {
            card = cardNumber;
            pin = thePIN;
            totalBalance = theTotalBalance;
        }

        public int getUserPIN()
        {
            return pin;
        }

        public int getCardNumber()
        {
            return card;
        }

        public double getTotalBalance()
        {
            return totalBalance;
        }
        public void credit(double amount)
        {
            totalBalance += amount;
        }
        public void debit(double amount)
        {
            totalBalance -= amount;
        }

        public void toHistory(String message)
        {
            history += message + "\n";
        }

        public String getHistory()
        {
            return history;
        }

        public String toString()
        {
            String tmp = "#" + card + "  PIN:" + pin + "  Balance:" + totalBalance + ".00";
            return tmp;
        }

        private String history = "";
        private int card;
        private int pin;
        private double totalBalance;
    }
}
