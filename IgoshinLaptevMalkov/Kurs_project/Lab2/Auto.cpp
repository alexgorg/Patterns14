#include "StdAfx.h"
#include "Auto.h"
#include <iostream>
#define LEN 100
using namespace std;


Auto::Auto(void)
{
	speed=0;
	length=0;
	position=0;
}

Auto::Auto(int a,int b)
	{
	cspeed=speed=a;
	length=b;
	position=0;
	ntr=0;
	torm=0;
	};

Auto::Auto(const Auto &copy)
{
	speed=copy.Getspeed();
	length=copy.Getlength();
}


Auto::~Auto(void)
{
	
}

void Auto::info() {
	
	cout << "������:" <<  endl;
	cout << "��������: " << speed<< endl;
	cout << "�����: " << length << endl;
	cout << "�������: " << position << endl;	
}

int Auto::Drive(char*r,char*o, Auto *predok)
{
	int i;
	//for(i=position+length;r[i]==' ';i++);		//���� ���������� ����
	//if(i>=LEN) 
	//{


	position+=speed;
	if (position+length>99) return -1;
	for(i=0;i<length;i++)
		if(r[position+i]==' ') {r[position+i]='#'; }
		else {r[position+i]='*'; return 1;}
	return 0;
}
