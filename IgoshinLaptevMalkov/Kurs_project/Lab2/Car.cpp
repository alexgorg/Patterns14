#include "StdAfx.h"
#include "Car.h"
#include <iostream>
using namespace std;

Car::Car(void)
{
}

Car::Car(const Car &copy):Auto(copy)
{
}

Car::Car(int a,int b):Auto(a,b)
{	
	cspeed=speed+=2;
	if(length>3) length=3;
}

Car::~Car(void)
{
}

void Car::Setlength(int a) 
	{
			Auto::Setlength(a);
	};


void Car::info()  {

	cout << "�������� ����������:" <<  endl;
	cout << "��������: " << cspeed<< endl;
	cout << "�����: " << length << endl;
	cout << "�������: " << position << endl;	
	cout << "���������� ������������: ";	if(Getprivod()) cout<<"��"<<endl; else cout<<"���"<<endl;
}

int Car::Getprivod() const 
{
	if(length%2==1) return 1;
	else return 0;

}

int Car::Paint(char*r,char*o)
{
position+=cspeed;
	char *t;
	if(ntr) t=o;
	else t=r;
	for(int i=0;i<length;i++)
			if(t[position+i]==' ') {t[position+i]='+'; }
			else {t[position+i]='*'; return 1;}
	return 0;
}

int Car::Drive(char*r,char*o, Auto *predok)			//0 ����, 1- ������
{
	//Auto* predok=0;
	

	if(ntr)					//!!!!!!!!!!! esli na obochine
		{
		int f1=1;
		for(int i=2;i<=length+1;i++)
			if(r[position+i]!=' ') f1=0;
		if(f1) {ntr=0; position+=2; Setcspeed(1); goto ris;}
		if(o[position+length]==' ') Setcspeed(1);
		else Setcspeed(0);
		goto ris;
		}			
	else
	{	

		if(predok==0) {						//���� ������
		torm=0;
		if (position+length+cspeed>99) return -1;
		if(cspeed<speed) cspeed+=1;
		Paint(r,o);
		return 0;
		}



		int f=1;
		if(position+length+cspeed>98)
			{for(int i=position+length;i<99;i++)
				if(r[position+length]!=' ') f=0; 
			if (f) return -1;					//avto ushlo
			}	
		else
		{ if(predok->Gettorm())								///!!!!!!!!! uslovie ��� ������ ��������
			{if(predok->Getcspeed()<cspeed)
				{Setcspeed(Getcspeed()-3);	
				torm=1;
				goto ris;
				}	
			else
				{torm=1;
				goto ris;
				}
			}
		else
			{
			if(predok->Getcspeed()==0)							///!!!!!!!!! uslovie ��� ������ stoit
				{
				if((predok->Getposition()>position+length-1+cspeed-3)&&cspeed>2)		//���� ������
					{Setcspeed(Getcspeed()-3);	torm=1; goto ris;}
				else									//���� �� ������
					if(Getprivod())					//���� ����
						{
						ntr=1;			///obezd
						Setposition(predok->Getposition());
						Setcspeed(1);							goto ris;
						}
					else {Setcspeed(Getcspeed()-3);	torm=1;}		//���� ������			������ �����
				}
			else			//������ ��������
				{
				if(predok->Getposition()>10*length+position+Getcspeed())		//���� ������ >10(6) ����
					{if(cspeed<speed) Setcspeed(cspeed+1);
					torm=0; goto ris;
					}
				else						//���� ������ 10 �
					{
						if(predok->Getposition()<4*length+position+Getcspeed()&&predok->Getcspeed()<cspeed)		//���� ������
							{if(cspeed-predok->Getcspeed()>3)	Setcspeed(cspeed-2); 
							else Setcspeed(cspeed-1);
							torm=1; 
							goto ris;}
						else {torm=0; goto ris;}
					}
					
				}	

			}
		}
	}



	/*position+=speed;
	if (position+length>99) return -1;
	*/
ris: 
	
	return Paint(r,o);
}