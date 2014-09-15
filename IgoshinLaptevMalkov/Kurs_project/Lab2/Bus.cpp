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
cout << "�������:" <<  endl;
	cout << "��������: " << cspeed<< endl;
	cout << "�����: " << length << endl;
	cout << "�������: " << position << endl;	
	cout << "���������� ������������: ";	if(Getprivod()) cout<<"��"<<endl; else cout<<"���"<<endl;
}

int Bus::Drive(char*r,char*o, Auto *predok)
{	static int ost=0;

	if(position==61-length)			//���� �� ���������
		{
			if(ost)
			{
				ost=0;
				if(r[61]==' ') cspeed=1; 
				
			}
			else ost=1;
		return Paint(r,o);
		}

	if(position+speed+length>=60&&position+length<60)			//���� �� �� ���������, ���� �� ������������?
		{
			position=61-length;
			cspeed=0;
			return Paint(r,o);
		}

	return Gruz::Drive(r,o,predok);
	
}