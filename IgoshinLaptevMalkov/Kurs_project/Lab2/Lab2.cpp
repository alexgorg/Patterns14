// Lab2.cpp: определяет точку входа для консольного приложения.
//

#include "stdafx.h"
#include "Auto.h"
#include "Car.h"
#include "Bus.h"
#include "Gruz.h"
#include "Taxi.h"
#include <iostream>
#include <conio.h>
#include <Windows.h>
#include <vector>
#include <algorithm>
//#include <typeinfo>
#include <ctime>
//#include <string>

#define LEN 100

using namespace std;
char road[LEN],oboch[LEN];
int Kdtp=0, prob=0,step=0;
vector<Auto*> traffic;
vector<Auto*> obochina;
void vivod();
void inform();
void sortm();
void sortmenu();

void sortm()
{
cout<<"1 - Отсортировать машины";
if(getch()=='1') {vivod(); sortmenu();}

}



int Random1(int M)
{
static int f=1;	
if(f){srand(time(0)); f=0;}
int a = rand() % M;
return a+1;
}


bool sor(Auto *a, Auto *b){
	return (a->Getposition() > b->Getposition());
	
}

bool sorsp(Auto *a, Auto *b){
	return (a->Getcspeed() > b->Getcspeed());
	
}

bool sortype(Auto *a, Auto *b){
	int t1=0,t2=0;
	t1 = (dynamic_cast<Car *>(a))? 1 : 0 ;
	if(t1==1) t1 = (dynamic_cast<Taxi *>(a))? 2 : 1;
	else t1 = (dynamic_cast<Bus *>(a))? 4 : 3;


	t2 = (dynamic_cast<Car *>(b))? 1 : 0;
	if(t2==1) t2 = (dynamic_cast<Taxi *>(b))? 2 : 1 ;
	else t2 = (dynamic_cast<Bus *>(b))? 4 : 3;

	if(t1 >= t2) return false;
	else return true;
}

void Dtp(vector<Auto *>::iterator lom)
{	int pos=(*lom)->Getposition();
	int len=(*lom)->Getlength();
	delete (*lom);
	traffic.erase(lom);
	for(vector<Auto *>::iterator p=traffic.begin();(*p)->Getposition()>=pos;p++)
		if((*p)->Getposition()<=pos+len)
			{delete (*p);
			p = traffic.erase(p);
			break;
			}
}

void drive()
{
	if(step%4==0) 
		{
			int sp=7+Random1(3);
			int len=Random1(5);
			switch(Random1(4))
			{
				case 1:	traffic.push_back(new Car(sp,len)); break;
				case 2: traffic.push_back(new Taxi(sp,len));break;
				case 3: traffic.push_back(new Gruz(sp,len)); break;
				case 4: traffic.push_back(new Bus(sp,len)); break;
			}		
	
		}
	step++;


memset(road,' ',sizeof(road));
memset(oboch,' ',sizeof(oboch));

vector<Auto *>::iterator pnt1 = traffic.begin();
for(int i=0;i<traffic.size();i++)
//while(pnt1 != traffic.end())
{	
	int temp;
	if(pnt1+i!=traffic.begin()) 
	temp=(*(pnt1+i))->Drive(road,oboch,*(pnt1+i-1));
	else { temp=(*(pnt1+i))->Drive(road,oboch,0);}
	if(temp==-1) //pnt1=dtp(pnt1,pnt1-1);
		{delete (*pnt1);
			pnt1 = traffic.erase(pnt1);
			i=-1;
		} 
	else
		{
		if(temp==1) 
			{
				int q=traffic.size();
				Kdtp+=temp;
				Dtp(pnt1+i);
				if(q-traffic.size()==2)		i--;
				continue;
			}
		
		}
}

sort(traffic.begin(), traffic.end(), sor);
}

void sortmenu()
{
int f=1;
char fu;

  while(f==1)
{
	vivod();
		do{
		puts("Cортировка:");
		puts("-------------------------------------");
		cout<<"1 - По скорости\n";				
		cout<<"2 - По расстоянию\n";	
		cout<<"3 - По типу автомобиля\n";
		cout<<"4 - Выход\n";								
		fflush(stdin);
		fu=getch();

	}while((fu < '1')||(fu > '4'));

	if(fu=='1') {sort(traffic.begin(), traffic.end(), sorsp);inform(); getch();	}
	if(fu=='2') {sort(traffic.begin(), traffic.end(), sor);inform(); getch();}
	if(fu=='3') {sort(traffic.begin(), traffic.end(), sortype);inform(); getch();}
	if(fu=='4') f = 0;

}
  sort(traffic.begin(), traffic.end(), sor);
}

void vivod()
{
	
system("cls");
cout<<"\n____________________________________________________________________________________________________\n";
for(int i=0;i<100;i++)
	cout<<road[i];
  
cout<<"\n\n____________________________________________________________=_______________________________________\n"; //ocтановка  60 с0
for(int i=0;i<100;i++)
cout<<oboch[i];
  cout<<"\n____________________________________________________________________________________________________\n";
  
}

void inform()
{
	vivod();
	int Kcar=0,Kgruz=0,Kbus=0,Kjeep=0;
	vector<Auto *>::iterator ptr = traffic.begin();
while(ptr != traffic.end())
{	
	(*ptr)->info();
	printf("\n");
	if(dynamic_cast<Car *>(*ptr)) Kcar++;
	else {
		Kgruz++;
		if(dynamic_cast<Bus *>(*ptr)) Kbus++;
		}
	if(dynamic_cast<Jeep *>(*ptr)->Getprivod()) Kjeep++; 

	ptr++;
}
cout<<"Произошло аварий: "<<Kdtp<<endl;
cout<<"Количество пробок: "<<prob<<endl;
cout<<"Количество легковых автомобилей: "<<Kcar<<endl;
cout<<"Количество грузовых автомобилей: "<<Kgruz<<endl;
cout<<"Из них автобусов: "<<Kbus<<endl;
cout<<"Среди всех , машин повышенной проходимости: "<<Kjeep<<endl<<endl;

}


void spravka()
{
vivod();
cout<<"+ - Легковой автомобиль"<<endl;
putchar(177); cout<<" - Такси"<<endl;
cout<<"# - Грузовой автомобиль"<<endl;
cout<<"@ - Автобус"<<endl;
cout<<"* - Авария"<<endl;
getch();

}


int _tmain(int argc, _TCHAR* argv[])
{
	SetConsoleCP(1251);
	SetConsoleOutputCP(1251);
	setlocale(LC_CTYPE,"Russian");


	memset(road,' ',sizeof(road));
	memset(oboch,' ',sizeof(oboch));

  int f=1;
  char fu;

  while(f==1)
{
	vivod();
	

		do{
		puts("-------------------------------------");
		cout<<"1 - Продолжить\n";				
		cout<<"2 - Информация\n";	
		cout<<"3 - Справка\n";
		cout<<"4 - Выход\n";								
		fflush(stdin);
		fu=getch();

	}while((fu < '1')||(fu > '4'));

		if(fu=='1') {drive(); 	}
		if(fu=='2') {inform(); sortm();}
	if(fu=='3') spravka();
	if(fu=='4') f = 0;

}
	return 0;
}

