#pragma once
#include "Auto.h"
#include "Info.h"
#include "Jeep.h"
class Gruz :
	public Auto,public Info,public Jeep
{
public:
	Gruz(void);
	Gruz(int a,int b);
	Gruz(const Gruz &copy):Auto(copy){}
	virtual int Getprivod() const ;
	virtual void info();
	virtual ~Gruz(void);
	virtual int Drive(char*r,char*o, Auto *predok);
	virtual int Paint(char*r,char*o);
};

