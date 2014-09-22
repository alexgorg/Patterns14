using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ocourse_ssh
{
    class wheat : contract
    {
	    private int hardness;
	    private int grade;
        public wheat()
        {
            hardness = 1;
            grade = 0;
        }
        public wheat(int ihardness, int igrade, string sname, string sprovider, string sreceiver, int iyear, int imonth, int iday, int inumber, int irealnumber, int imoney) : 
            base(sname, sprovider, sreceiver, iyear, imonth, iday, inumber, irealnumber, imoney)
        {
            hardness = 1;
            grade = 0;
            setHardness(ihardness);
            setGrade(igrade);
        }
	    // значение класса пшеницы
        public virtual int getGrade()
        {
            return grade;
        }
	    // значение твёрдости пшеницы
        public virtual int getHardness()
        {
            return hardness;
        }
	    // задать класс пшеницы
        public virtual void setGrade(int igrade)
        {
            switch (igrade)
            {
                case 0: //нестандартная
                    grade = 0;
                    break;
                case 3: //3-й класс
                    grade = 3;
                    break;
                case 4: //4-й класс
                    grade = 4;
                    break;
                default:
                    break;
            }
        }
	    // задать твёрдость пшеницы
        public virtual void setHardness(int ihardness)
        {
            switch (ihardness)
            {
                case 1: //твёрдая
                    hardness = 1;
                    break;
                case 2: //мягкая
                    hardness = 2;
                    break;
                default:
                    break;
            }
        }
    }
}
