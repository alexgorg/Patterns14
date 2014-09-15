#include "StdAfx.h"
#include "Taxi.h"
#include <ctime>
#include <iostream>
using namespace std;

int Random2(int M)
{
static int f=1;	
if(f){srand(time(0)); f=0;}
int a = rand() % M;
return a+1;
}

Taxi::Taxi(void):Car()
{
}

int Taxi::Paint(char*r,char*o)
{
position+=cspeed;
	char *t;
	if(ntr) t=o;
	else t=r;
	for(int i=0;i<length;i++)
			if(t[position+i]==' ') {t[position+i]=177; }
			else {t[position+i]='*'; return 1;}
	return 0;
}

 void Taxi::info()
 {
	 cout << "Такси:" <<  endl;
	cout << "Скорость: " << cspeed<< endl;
	cout << "Длина: " << length << endl;
	cout << "Позиция: " << position << endl;	
	cout << "Повышенная проходимость: ";	if(Getprivod()) cout<<"Да"<<endl; else cout<<"Нет"<<endl;
 
 }
 

Taxi::Taxi(int a,int b):Car(a,b)
{}

Taxi::Taxi(const Taxi &copy):Car(copy){}

Taxi::~Taxi(void)
{
}

int Taxi::Drive(char*r,char*o, Auto *predok)
{	
	if(Random2(12)==1)
	{cspeed=0; return Paint(r,o);}
	else
		return Car::Drive(r,o,predok);

	
}