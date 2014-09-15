using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace ATM
{
    public partial class ATM_Mainform : Form
    {
        public ATM_Mainform()
        {
            InitializeComponent();
            toScreen = richTextBox1;
            toListBox = listBox1;
            ATMObject = new ATM();
            __thread = new Thread(new ThreadStart( StartATM ) );
            __thread.IsBackground = true;
            __thread.Start();
        }

        //asdasdds

        delegate void textDelegate(String text);

        private void ChangeText(String text)
        {
            if ( toScreen.InvokeRequired)
            {
                toScreen.BeginInvoke(new textDelegate(ChangeText), new object[] { text });
                return;
            }
            else
            {
                toScreen.Text = text;
            }
        }
       
        private static void StartATM()
        {
            ATMObject.runATM();
        }

        private static Thread __thread;

        private static System.Windows.Forms.ListBox toListBox;
        private static System.Windows.Forms.RichTextBox toScreen;
        private static ATM ATMObject;
        private static Screen MainScreen = new Screen( toScreen );
        private static String Buffer = "";
        private enum KeyType { NOTHING, BACK, ENTER, BACKSPACE, CARD, CASH, DEPOSIT, CANCEL };
        private static KeyType keySemaphore = KeyType.NOTHING;
        private enum OperationType { NOTHING, PIN, DRAW };
        private static OperationType operationSemaphore = OperationType.NOTHING;


        private static void waiting(KeyType type)
        {
            keySemaphore = KeyType.NOTHING;
            do
            {
                    Thread.Sleep(200);
            } while(keySemaphore != type);
            keySemaphore = KeyType.NOTHING;
        }

        private static void waiting(int milisec)
        {
                Thread.Sleep(milisec);
        }

        private class ATM
        {
            private int selectedIndex;
            private bool userAuthenticated;
            private int currentPIN;
            private Screen screen = new Screen( toScreen );
            private CashDispenser cashDispenser = new CashDispenser();
            private DepositSlot depositSlot = new DepositSlot();
            private BankDatabase bankDatabase = new BankDatabase();
            private Thread __check_canceled_thread;

            private void getListBoxIndex()
            {
                selectedIndex = toListBox.SelectedIndex;
            }

            private void ClearListBox()
            {
                toListBox.Items.Clear();
            }

            private void getListOfAccounts()
            {
                for (int i = 0; i < 30; i++)
                    toListBox.Items.Add( bankDatabase.getAccountAt(i).toString() );
            }

            private void CheckCanceled()
            {
                keySemaphore = KeyType.NOTHING;
                do
                {
                    Thread.Sleep(200);
                } while (keySemaphore != KeyType.CANCEL);
                keySemaphore = KeyType.NOTHING;
                userAuthenticated = false;
                currentPIN = 0;
                __thread.Abort();
                screen.displayMessage("\nВыход из системы...");
                waiting(2000);
                screen.Clear();
                screen.displayMessage("Спасибо! До свидания!");
                waiting(2000);
                __thread = new Thread(new ThreadStart(this.runATM));
                __thread.IsBackground = true;
                __thread.Start();
            }

            public ATM()
            {
                userAuthenticated = false;
                currentPIN = 0;
                bankDatabase.getListOfAccounts( toListBox );
                selectedIndex = -1;
            }

            public void runATM()
            {
                __check_canceled_thread = new Thread(new ThreadStart(CheckCanceled));
                __check_canceled_thread.IsBackground = true;
                __check_canceled_thread.Start();
                while (true)
                {
                    while (!userAuthenticated)
                    {
                        screen.Clear();
                        Buffer = "";
                        screen.displayMessageLine("Добро пожаловать!");
                        screen.displayMessage("Вставьте вашу карту...");
                        authenticateUser();
                    }
                    performTransactions();
                    userAuthenticated = false;
                    currentPIN = 0;
                }
            }

            private void authenticateUser()
            {
                int index = -1;
                int count = 0;
                keySemaphore = KeyType.NOTHING;
                do
                {
                        if (count == 15)
                        {
                            screen.Clear();
                            screen.displayMessageLine("Добро пожаловать!");
                            screen.displayMessage("Вставьте вашу карту...");
                            count = 0;
                        }
                        else
                        {
                            screen.displayMessage(".");
                            count++;
                        }
                        keySemaphore = KeyType.NOTHING;
                        Thread.Sleep(300);
                        toListBox.Invoke(new MethodInvoker( getListBoxIndex ));
                        index = selectedIndex;
                } while (keySemaphore != KeyType.CARD || index < 0);
                screen.Clear();
                screen.displayMessage("Введите ваш PIN: ");
                int pin = getPinInput();
                screen.displayMessageLine("");

                userAuthenticated = bankDatabase.authenticateUser(pin, index);
                if (userAuthenticated)
                    currentPIN = pin;
                else
                {
                    screen.displayMessageLine("Неверный PIN. Попробуйте снова...");
                    waiting(2000);
                }
            }

            private void performTransactions()
            {
                Transaction currentTransactionPtr;
                bool userExited = false;
                while (!userExited)
                {
                    toListBox.Invoke(new MethodInvoker(ClearListBox));
                    toListBox.Invoke(new MethodInvoker(getListOfAccounts));
                    int mainMenuSelection = displayMainMenu();
                    switch (mainMenuSelection)
                    {
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                            currentTransactionPtr = createTransaction(mainMenuSelection);
                            currentTransactionPtr.execute();
                            break;
                        case 5:
                            screen.Clear();
                            screen.displayMessage(bankDatabase.getHistory(bankDatabase.getCardByPIN(currentPIN)));
                            waiting(KeyType.BACK);
                            break;
                    }
                }
            }

            private int displayMainMenu()
            {

                screen.Clear();
                screen.displayMessageLine("Главное меню");
                screen.displayMessageLine("\n 1 - Баланс");
                screen.displayMessageLine(" 2 - Вывод наличных");
                screen.displayMessageLine(" 3 - Пополнение счета");
                screen.displayMessageLine(" 4 - Перевод средств");
                screen.displayMessageLine(" 5 - История счета");

                return getMenuInput();
            }

            private Transaction createTransaction(int type)
            {
                Transaction tempPtr = null;
                switch (type)
                {
                    case 1:
                        tempPtr = new Balancelnquiry(currentPIN, screen, bankDatabase);
                        break;
                    case 2:
                        tempPtr = new Withdrawal(currentPIN, screen, bankDatabase, cashDispenser);
                        break;
                    case 3:
                        tempPtr = new Deposit(currentPIN, screen, bankDatabase, depositSlot, cashDispenser);
                        break;
                    case 4:
                        tempPtr = new Transfer(currentPIN, screen, bankDatabase);
                        break;
                }
                return tempPtr;
            }

            private int getPinInput()
            {
                Buffer = "";
                operationSemaphore = OperationType.PIN;
                do
                {
                        Thread.Sleep(200);
                } while (Buffer.Length < 4);
                operationSemaphore = OperationType.NOTHING;
                return Convert.ToInt32( Buffer );
            }

            private int getMenuInput()
            {
                Buffer = "";
                operationSemaphore = OperationType.NOTHING;
                do
                {
                        Thread.Sleep(200);
                } while (Buffer.Length < 1);
                operationSemaphore = OperationType.NOTHING;
                return Convert.ToInt32( Buffer );
            }
        }

        private class Balancelnquiry : Transaction 
        {
            public Balancelnquiry( int userPIN, Screen atmScreen, BankDatabase atmBankDatabase )
                : base( userPIN, atmScreen, atmBankDatabase )
            {
            }
    
            public override void execute()
            {
                BankDatabase bankDatabase = getBankDatabase(); 
                Screen screen = getScreen(); 
                double totalBalance = bankDatabase.getTotalBalance( getUserPIN() );
                screen.Clear();
                screen.displayMessageLine( "Информация о балансе" ); 
                screen.displayMessage( "\nТекущий баланс: " ); 
                screen.displayDollarAmount( totalBalance );
                Buffer = "";
                waiting( KeyType.BACK );
            }
        }

        private class Withdrawal : Transaction 
        {
            public Withdrawal( int userPIN, Screen atmScreen, BankDatabase atmBankDatabase, CashDispenser atmCashDispenser )
                : base( userPIN, atmScreen, atmBankDatabase )
            {
                cashDispenser = atmCashDispenser;
            }
    
            private int getMenuInput()
            {
                Buffer = "";
                operationSemaphore = OperationType.NOTHING;
                keySemaphore = KeyType.NOTHING;
                do{
                        Thread.Sleep( 200 );
                }while( Buffer.Length < 1 && keySemaphore != KeyType.BACK );
                operationSemaphore = OperationType.NOTHING;
                if( keySemaphore == KeyType.BACK )
                    return -1;
                else
                    return Convert.ToInt32( Buffer );
            }
    
            private int getWithdrawalInput()
            {
                Buffer = "";
                Screen screen = getScreen();
                screen.Clear();
                screen.displayMessageLine( "Другая сумма" );
                screen.displayMessage( "\nСумма: " );
                operationSemaphore = OperationType.DRAW;
                keySemaphore = KeyType.NOTHING;
                do{
                        Thread.Sleep( 200 );
                }while( keySemaphore != KeyType.BACK && keySemaphore != KeyType.ENTER && Buffer.Length < 7  );
                screen.displayMessageLine( "" );
                operationSemaphore = OperationType.NOTHING;
                if (keySemaphore == KeyType.BACK)
                    return -2;
                else
                {
                    screen.Clear();
                    screen.displayMessageLine("Другая сумма");
                    screen.displayMessage("\nСумма: " + Convert.ToInt32(Buffer) + ".00 руб к выдаче...\n");
                    return Convert.ToInt32(Buffer);
                }
            }
    
            public override void execute()
            {
                bool cashDispensed = false;
                bool transactionCanceled = false; 
                BankDatabase bankDatabase = getBankDatabase();
                Screen screen = getScreen();
        
                do{ 
                    int selection = displayMenuOfAmounts();
                    Buffer = "";
                    if( selection == -2 )
                        continue;
                    if ( selection != -1 )
                    {
                        amount = selection; 
                        double totalBalance = bankDatabase.getTotalBalance( getUserPIN() ); 
                        if ( amount <= totalBalance ) 
                        { 
                           if ( cashDispenser.isCashAvailable( amount ) ) 
                           {
                               bankDatabase.debit( getUserPIN(), amount );
                               cashDispenser.dispenseCash( amount ); 
                               cashDispensed = true; 
                               screen.displayMessage( "\nПожалуйста заберите ваши деньги!" ); 
                               bankDatabase.toHistory( getUserCardNumber(), "Вывод наличных: -" + amount + " руб."  );
                               waiting( KeyType.CASH );
                           } 
                           else
                           {
                               screen.displayMessage( "\nВ банкомате недостаточно средств для выполнения операции!" );
                               waiting( 3000 );
                           }
                        } 
                        else 
                        {
                            screen.displayMessage( "\nНа вашем счете недостаточно средств для выполнения операции!" );
                            waiting( 3000 );
                        }
                    }
                    else
                        transactionCanceled = true;
                }while ( !cashDispensed && !transactionCanceled );
            }
    
            private int amount; 
            private CashDispenser cashDispenser; 
    
            private int displayMenuOfAmounts()
            {
                int userChoice = 0;
                Screen screen = getScreen();
                List<int> amounts = new List<int>();
                amounts.Add( 0 );
                amounts.Add( 100 );
                amounts.Add( 500 );
                amounts.Add( 1000 );
                amounts.Add( 5000 );
                while ( userChoice == 0 ) 
                {
                    screen.Clear();
                    screen.displayMessageLine( "Выберите сумму" ); 
                    screen.displayMessageLine( "\n 1 - 100" ); 
                    screen.displayMessageLine( " 2 - 500" );
                    screen.displayMessageLine( " 3 - 1000" );
                    screen.displayMessageLine( " 4 - 5000" );
                    screen.displayMessageLine( " 5 - Другая сумма" ); 
            
                    int input = getMenuInput();
            
                    switch( input )
                    {
                        case 1: 
                        case 2:
                        case 3: 
                        case 4:
                            userChoice = amounts[ input ];
                            break; 
                        case 5: 
                            userChoice = getWithdrawalInput();
                            break;
                        case -1: 
                            userChoice = -1; 
                            break;
                    }
                }
                return userChoice;
            }
        }

        private class Deposit : Transaction 
        {
            public Deposit( int userPIN, Screen atmScreen, BankDatabase atmBankDatabase, DepositSlot atmDepositSlot, CashDispenser atmCashDispenser ) 
                : base( userPIN, atmScreen, atmBankDatabase )
            {
                depositSlot = atmDepositSlot;
                cashDispenser = atmCashDispenser;
            }
    
            private int getDepositInput()
            {
                Buffer = "";
                operationSemaphore = OperationType.DRAW;
                keySemaphore = KeyType.NOTHING;
                do{
                        Thread.Sleep( 200 );
                }while( keySemaphore != KeyType.BACK && keySemaphore != KeyType.ENTER  && Buffer.Length < 7 ); 
                operationSemaphore = OperationType.NOTHING;
                if( keySemaphore == KeyType.BACK )
                    return -1;
                else
                    return Convert.ToInt32( Buffer );
            }
    
            public override void execute()
            {
                BankDatabase bankDatabase = getBankDatabase(); 
                Screen screen = getScreen();
                amount = promptForDepositAmount() ;
                if ( amount != 0 ) // 0 - canceled
                {
                    screen.displayMessage( "\nПожалуйста вставьте " );
                    screen.displayDollarAmount( amount ); 
                    screen.displayMessageLine( " в слот депозита..." );
            
                    keySemaphore = KeyType.NOTHING;
                    
                    for( int i = 0 ; i < 30 ; i++ )
                    {
                        Thread.Sleep( 200 );
                        if( keySemaphore == KeyType.BACK || keySemaphore == KeyType.DEPOSIT )
                            break;
                    }
            
                    bool envelopeReceived = false;
            
                    if( keySemaphore == KeyType.DEPOSIT )
                         envelopeReceived = depositSlot.isEnvelopeReceived();
            
                    if ( envelopeReceived )
                    { 
                        bankDatabase.credit( getUserPIN(), amount ); 
                        cashDispenser.depositCash( amount );
                        screen.displayMessage( "\nОперация выполнена успешно..." );
                        bankDatabase.toHistory( getUserCardNumber(), "Пополнение счета: +" + amount + " руб." );
                        waiting( 3000 );
                    }
                    else 
                        if( keySemaphore == KeyType.BACK )
                        {
                            screen.displayMessage( "\nОтмена операции..." ); 
                            waiting( 3000 );
                        }
                        else
                        {
                            screen.displayMessage( "\nВремя ожидания вышло. Отмена операции..." ); 
                            waiting( 3000 );
                        }                   
                }
                else
                {
                    screen.displayMessage( "\nОтмена операции..." );
                    waiting( 3000 );
                }
            }
    
            private int amount; 
            private DepositSlot depositSlot; 
            private CashDispenser cashDispenser;
            private int promptForDepositAmount()
            {
                Screen screen = getScreen(); 
                screen.Clear();
                screen.displayMessage( "Пожалуйста введите размер депозита \n(в десятках рублей): " ); 
                int input = getDepositInput();
                Buffer = "";
                screen.displayMessageLine( "" );
                if ( input == -1 )
                    return 0; 
                else 
                    return input * 10 ;
            }
        }

        private class Transfer : Transaction
        {
            public Transfer( int userPIN, Screen atmScreen, BankDatabase atmBankDatabase )
                : base( userPIN, atmScreen, atmBankDatabase )
            {
            }
    
            public override void execute()
            {
                BankDatabase bankDatabase = getBankDatabase(); 
                Screen screen = getScreen(); 
                double totalBalance = bankDatabase.getTotalBalance( getUserPIN() );
                screen.Clear();
                screen.displayMessage( "Текущий баланс: " ); 
                screen.displayDollarAmount( totalBalance );
                screen.displayMessageLine( "" ); 
                int cardNumber = getCardNumberInput();
                if( cardNumber == -1 )
                {
                    screen.displayMessage( "\nОтмена операции..." );
                    waiting( 3000 );
                }
                else
                {
                    int amount = getAmountInput();
                    if( amount == - 1)
                    {
                        screen.displayMessage( "\nОтмена операции..." );
                        waiting( 3000 );
                    }
                    else
                        tryToTransfer( cardNumber, amount );
                }  
            }
    
            private void tryToTransfer( int cardNumber, int amount )
            {
                Screen screen = getScreen(); 
                BankDatabase bankDatabase = getBankDatabase();
                if( bankDatabase.findCardByNumber( cardNumber ) )
                {
                    if( bankDatabase.getTotalBalance( getUserPIN() ) < amount )
                    {
                        screen.displayMessageLine( "\nНе достаточно средств для выполнения операции..." );
                        waiting( 3000 );
                    }
                    else
                    {
                        bankDatabase.debit( getUserPIN(), amount );
                        bankDatabase.creditTransfer( cardNumber, amount );
                        screen.displayMessageLine( "\nОперация перевода завершена!" );
                        bankDatabase.toHistory( getUserCardNumber(), "Перевод: -" + amount + " руб." );
                        bankDatabase.toHistory( cardNumber, "Перевод: +" + amount + " руб." );
                        waiting( 3000 );
                    }
                }
                else
                {
                    screen.displayMessageLine( "\nВ базе не найден счет №" + cardNumber );
                    waiting( 3000 );
                }
        
            }
    
            private int getAmountInput()
            {
                Screen screen = getScreen(); 
                Buffer = "";
                screen.displayMessage( "Сумма: " ); 
                operationSemaphore = OperationType.DRAW;
                keySemaphore = KeyType.NOTHING;
                do{
                        Thread.Sleep( 200 );
                }while( keySemaphore != KeyType.BACK && keySemaphore != KeyType.ENTER && Buffer.Length < 7  );
                operationSemaphore = OperationType.NOTHING;
                screen.displayMessageLine( "" );
                if( keySemaphore == KeyType.BACK )
                    return -1;
                else
                    return Convert.ToInt32( Buffer );
            }
    
            private int getCardNumberInput()
            {       
                Screen screen = getScreen(); 
                Buffer = "";
                screen.displayMessage( "\nУкажите номер счета получателя в формате \n\"99хххх\": " ); 
                screen.displayMessage( "99" ); 
                operationSemaphore = OperationType.DRAW;
                keySemaphore = KeyType.NOTHING;
                do{
                        if( Buffer.Length >= 4 )
                        {
                            operationSemaphore = OperationType.NOTHING;
                            Buffer = Buffer.Substring( 0, 4 );
                        }
                        else
                        {
                            operationSemaphore = OperationType.DRAW;
                            keySemaphore = KeyType.NOTHING;
                        }
                        Thread.Sleep( 200 );
                }while( ( Buffer.Length < 4 || keySemaphore != KeyType.ENTER ) && keySemaphore != KeyType.BACK );
                screen.displayMessageLine( "" ); 
                if( keySemaphore == KeyType.BACK )
                    return -1;
                else
                    return Convert.ToInt32( "99" + Buffer );
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Buffer += "1";
            if (operationSemaphore == OperationType.PIN)
                MainScreen.displayMessage("*");
            else if (operationSemaphore == OperationType.DRAW)
                MainScreen.displayMessage("1");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Buffer += "2";
            if (operationSemaphore == OperationType.PIN)
                MainScreen.displayMessage("*");
            else if (operationSemaphore == OperationType.DRAW)
                MainScreen.displayMessage("2");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Buffer += "3";
            if (operationSemaphore == OperationType.PIN)
                MainScreen.displayMessage("*");
            else if (operationSemaphore == OperationType.DRAW)
                MainScreen.displayMessage("3");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Buffer += "4";
            if (operationSemaphore == OperationType.PIN)
                MainScreen.displayMessage("*");
            else if (operationSemaphore == OperationType.DRAW)
                MainScreen.displayMessage("4");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Buffer += "5";
            if (operationSemaphore == OperationType.PIN)
                MainScreen.displayMessage("*");
            else if (operationSemaphore == OperationType.DRAW)
                MainScreen.displayMessage("5");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Buffer += "6";
            if (operationSemaphore == OperationType.PIN)
                MainScreen.displayMessage("*");
            else if (operationSemaphore == OperationType.DRAW)
                MainScreen.displayMessage("6");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Buffer += "7";
            if (operationSemaphore == OperationType.PIN)
                MainScreen.displayMessage("*");
            else if (operationSemaphore == OperationType.DRAW)
                MainScreen.displayMessage("7");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Buffer += "8";
            if (operationSemaphore == OperationType.PIN)
                MainScreen.displayMessage("*");
            else if (operationSemaphore == OperationType.DRAW)
                MainScreen.displayMessage("8");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Buffer += "9";
            if (operationSemaphore == OperationType.PIN)
                MainScreen.displayMessage("*");
            else if (operationSemaphore == OperationType.DRAW)
                MainScreen.displayMessage("9");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Buffer += "0";
            if (operationSemaphore == OperationType.PIN)
                MainScreen.displayMessage("*");
            else if (operationSemaphore == OperationType.DRAW)
                MainScreen.displayMessage("0");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (operationSemaphore == OperationType.DRAW)
            {
                Buffer += "000";
                MainScreen.displayMessage("000");
            }     
        }

        private void button12_Click(object sender, EventArgs e)
        {
            keySemaphore = KeyType.CANCEL;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            keySemaphore = KeyType.BACKSPACE;
            if (Buffer.Length > 0)
            {
                Buffer = Buffer.Substring(0, Buffer.Length - 1);
                MainScreen.Backspace();
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            keySemaphore = KeyType.ENTER;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            keySemaphore = KeyType.BACK;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            keySemaphore = KeyType.CARD;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            keySemaphore = KeyType.CASH;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            keySemaphore = KeyType.DEPOSIT;
        }

    }
}
