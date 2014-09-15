using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ATM
{
    public class BankDatabase
    {
        public BankDatabase()
        {
            Random rand = new Random();
            for (int i = 0; i < 30; i++)
                accounts.Add( new Account(990000 + rand.Next() % 10000, 1000 + rand.Next() % 9000, 100.0 + rand.Next() % 49900));
        }

        public int getPINAt(int index)
        {
            return accounts[index].getUserPIN();
                
        }

        public void toHistory(int cardNumber, String message)
        {
            Account userAccountPtr = getAccountByCard(cardNumber);
            userAccountPtr.toHistory(message);
        }

        public String getHistory(int cardNumber)
        {
            Account userAccountPtr = getAccountByCard(cardNumber);
            return userAccountPtr.getHistory();
        }

        public bool authenticateUser(int userPIN, int index)
        {
            if (userPIN == getPINAt(index))
                return true;
            else
                return false;
        }

        public int getCardByPIN(int userPIN)
        {
            Account userAccountPtr = getAccount(userPIN);
            return userAccountPtr.getCardNumber();
        }

        public bool findCardByNumber(int cardNumber)
        {
            Account userAccountPtr = getAccountByCard(cardNumber);
            if (userAccountPtr != null)
                return true;
            else
                return false;
        }

        public void creditTransfer(int cardNumber, double amount)
        {
            Account userAccountPtr = getAccountByCard(cardNumber);
            userAccountPtr.credit(amount);
        }

        public double getTotalBalance(int userPIN)
        {
            Account userAccountPtr = getAccount(userPIN);
            return userAccountPtr.getTotalBalance();
        }

        public void credit(int userPIN, double amount)
        {
            Account userAccountPtr = getAccount(userPIN);
            userAccountPtr.credit(amount);
        }
        public void debit(int userPIN, double amount)
        {
            Account userAccountPtr = getAccount(userPIN);
            userAccountPtr.debit(amount);
        }

        private List<Account> accounts = new List<Account>();

        private Account getAccount(int userPIN)
        {
            for (int i = 0; i < accounts.Count; i++)
            {
                if ( accounts[i].getUserPIN() == userPIN)
                    return accounts[i];
            }
            return null;
        }

        public void getListOfAccounts( System.Windows.Forms.ListBox listbox )
        {
            for (int i = 0; i < 30; i++)
                listbox.Items.Add(accounts[i].toString());
        }

        public Account getAccountAt(int index)
        {
            for (int i = 0; i < accounts.Count; i++)
            {
                if( i == index )
                    return accounts[i];
            }
            return null;
        }

        private Account getAccountByCard(int cardNumber)
        {
            for (int i = 0; i < accounts.Count ; i++)
            {
                if (accounts[i].getCardNumber() == cardNumber)
                    return accounts[i];
            }
            return null;
        }
    }
}
