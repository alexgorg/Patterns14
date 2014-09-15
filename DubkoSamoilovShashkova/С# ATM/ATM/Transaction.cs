using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATM
{
    public abstract class Transaction
    {
        public Transaction(int userPIN, Screen atmScreen, BankDatabase atmBankDatabase)
        {
            pin = userPIN;
            screen = atmScreen;
            bankDatabase = atmBankDatabase;
        }
        public int getUserCardNumber()
        {
            return bankDatabase.getCardByPIN(pin);
        }
        public int getUserPIN()
        {
            return pin;
        }
        public Screen getScreen()
        {
            return screen;
        }
        public BankDatabase getBankDatabase()
        {
            return bankDatabase;
        }
        abstract public void execute();

        private int pin;
        private Screen screen;
        private BankDatabase bankDatabase;
    }
}
