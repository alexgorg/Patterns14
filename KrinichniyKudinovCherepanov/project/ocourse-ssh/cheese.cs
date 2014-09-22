using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ocourse_ssh
{
    class cheese : contract
    {
        private int type;
        private int fat;
        private int pack;
        public cheese()
        {
            type = 1;
            fat = 30;
            pack = 1;
        }
        public cheese(int itype, int ifat, int ipack, string sname, string sprovider, string sreceiver, int iyear, int imonth, int iday, int inumber, int irealnumber, int imoney) : 
            base(sname, sprovider, sreceiver, iyear, imonth, iday, inumber, irealnumber, imoney)
        {
            type = 1;
            fat = 30;
            pack = 1;
            setType(itype);
            setFat(ifat);
            setPack(ipack);
        }
        // значение сорта сыра
        public virtual int getType()
        {
            return type;
        }
        // значение жирности сыра
        public virtual int getFat()
        {
            return fat;
        }
        // значение упаковки сыра
        public virtual int getPack()
        {
            return pack;
        }
        // задать жирность сыра
        public virtual void setFat(int ifat)
        {
            switch (ifat)
            {
                case 30: //30%
                    fat = 30;
                    break;
                case 40: //40%
                    fat = 40;
                    break;
                case 50:
                    fat = 50; //50%
                    break;
                default:
                    break;
            }
        }
        // задать упаковку сыра
        public virtual void setPack(int ipack)
        {
            switch (ipack)
            {
                case 1: //плёнка
                    pack = 1;
                    break;
                case 2: //парафин
                    pack = 2;
                    break;
                default:
                    break;
            }
        }
        // задать сорт сыра
        public virtual void setType(int itype)
        {
            switch (itype)
            {
                case 1: //алтайский
                    type = 1;
                    break;
                case 2: //радонежский
                    type = 2;
                    break;
                case 3: //голландский
                    type = 3;
                    break;
                default:
                    break;
            }
        }
    }
}
