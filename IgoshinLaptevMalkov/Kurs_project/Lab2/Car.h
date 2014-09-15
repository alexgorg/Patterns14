#pragma once
#include "auto.h"
#include "Info.h"
#include "Jeep.h"
using namespace std;
class Car :
	public Auto,public Info,public Jeep
{
public:
	Car(void);
	virtual ~Car(void);
	Car(int a,int b);
	Car(const Car &copy);
	virtual void info();
	virtual void Setlength(int a); 
	virtual int Getprivod() const;
	virtual int Drive(char*r,char*o, Auto *predok);
	virtual int Paint(char*r,char*o);
};

