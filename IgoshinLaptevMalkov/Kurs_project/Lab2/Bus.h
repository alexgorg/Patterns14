#pragma once
#include "gruz.h"
class Bus :
	public Gruz
{
public:
	Bus(void);
	Bus(int a,int b):Gruz(a,b){}
	virtual ~Bus(void);
	virtual void info() ;
	virtual int Drive(char*r,char*o, Auto *predok);
	virtual int Paint(char*r,char*o);
};

