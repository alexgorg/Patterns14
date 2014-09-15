#include "StdAfx.h"
#include "Bus.h"
#include <iostream>
using namespace std;

Bus::Bus(void)
{
}


Bus::~Bus(void)
{
}


int Bus::Paint(char*r,char*o)
{
position+=cspeed;
	char *t;
	if(ntr) t=o;
	else t=r;
	for(int i=0;i<length;i++)
			if(t[position+i]==' ') {t[position+i]='@'; }
			else {t[position+i]='*'; return 1;}
	return 0;
}


void Bus::info() 
{
cout << "Автобус:" <<  endl;
	cout << "Скорость: " << cspeed<< endl;
	cout << "Длина: " << length << endl;
	cout << "Позиция: " << position << endl;	
	cout << "Повышенная проходимость: ";	if(Getprivod()) cout<<"Да"<<endl; else cout<<"Нет"<<endl;
}

int Bus::Drive(char*r,char*o, Auto *predok)
{	static int ost=0;

	if(position==61-length)			//если на остановке
		{
			if(ost)
			{
				ost=0;
				if(r[61]==' ') cspeed=1; 
				
			}
			else ost=1;
		return Paint(r,o);
		}

	if(position+speed+length>=60&&position+length<60)			//если не на остановке, могу ли остановиться?
		{
			position=61-length;
			cspeed=0;
			return Paint(r,o);
		}

	return Gruz::Drive(r,o,predok);
	
}