#pragma once
#include "Vehicle.h"
class Car : public Vehicle
{
	int MaxLength;
public:
	Car(void);
	Car(int s,int l,int dx,int dy,int ml);
	//
	//set
	//
	virtual void setMaxLength(int ml);
	virtual void setLength(int l);
	//
	//get
	//
	virtual int getMaxLength(void);
	//
	//interface
	//
	virtual void draw(Graphics^ g, bool cmd);
	~Car(void);
};

