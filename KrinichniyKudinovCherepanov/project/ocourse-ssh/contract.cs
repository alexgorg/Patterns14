using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ocourse_ssh
{
    class contract
    {
        protected string name;
        protected string provider;
        protected int realnumber;
        protected string receiver;
        protected int year;
        protected int month;
        protected int day;
        protected int number;
        protected int money;
        public contract()
        {
            name = "";
            provider = "";
            realnumber = 0;
            receiver = "";
            year = 2010;
            month = 1;
            day = 1;
            number = 0;
            money = 0;
        }
        public contract(string sname, string sprovider, string sreceiver, int iyear, int imonth, int iday, int inumber, int irealnumber, int imoney)
        {
            name = "";
            provider = "";
            realnumber = 0;
            setName(sname);
            setProvider(sprovider);
            setRealNumber(irealnumber);
            receiver = "";
            year = 2010;
            month = 1;
            day = 1;
            number = 0;
            money = 0;
            setReceiver(sreceiver);
            setYear(iyear);
            setMonth(imonth);
            setDay(iday);
            setNumber(inumber);
            setMoney(imoney);
        }
        // возвращает значение наименования
        public virtual string getName()
        {
            return name;
        }
        // возвращает значение поставщика
        public virtual string getProvider()
        {
            return provider;
        }
        // реальное число поставок
        public virtual int getRealNumber()
        {
            return realnumber;
        }
        // значение наименования
        public virtual void setName(string sName)
        {
            name = sName;
        }
        // значение поставщика
        public virtual void setProvider(string sProvider)
        {
            provider = sProvider;
        }
        // задать реальное число поставок
        public virtual void setRealNumber(int irealnumber)
        {
            if (irealnumber >= 0)
                realnumber = irealnumber;
        }
        // возвращает значение получателя
        public virtual string getReceiver() 
        {
            return receiver;
        }
	    // значение получателя
        public virtual void setReceiver(string sReceiver)
        {
            receiver = sReceiver;
        }
	    // год договора
        public virtual int getYear()
        {
            return year;
        }
	    // год договора
        public virtual int getMonth()
        {
            return month;
        }
	    // год договора
        public virtual int getDay()
        {
            return day;
        }
	    // планируемое число поставок
        public virtual int getNumber()
        {
            return number;
        }
	    // планируемый бюджет договора
        public virtual int getMoney()
        {
            return money;
        }
	    // задать год договора
        public virtual void setYear(int iyear)
        {
            if (iyear >= 1980 && iyear < 2080)
                year = iyear;
        }
	    // задать месяц договора
        public virtual void setMonth(int imonth)
        {
            if (imonth >= 1 && imonth <= 12)
                month = imonth;
        }
	    // задать день договора
        public virtual void setDay(int iday)
        {
            switch (month)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    if (iday >= 1 && iday <= 31) day = iday;
                    break;
                case 4:
                case 6:
                case 9:
                case 11:
                    if (iday >= 1 && iday <= 30) day = iday;
                    break;
                case 2:
                    if (year % 4 == 0)
                    {
                        if (iday >= 1 && iday <= 28) day = iday;
                    }
                    else if (iday >= 1 && iday <= 29) day = iday;
                    break;
                default:
                    break;
            }
        }
	    // задать планируемое число поставок
        public virtual void setNumber(int inumber)
        {
            if (inumber >= 0)
                number = inumber;
        }
	    // задать планируемый бюджет договора
        public virtual void setMoney(int imoney)
        {
            if (imoney >= 0)
                money = imoney;
        }
    }
}
