#pragma once
#include "Vehicle.h"
class Truck : public Vehicle
{
	int MinLen;
public:
	Truck(void);
	Truck(int s,int l,int dx,int dy,int ml);
	//
	//set
	//
	void setMinLen(int l);
	void setLength(int l);
	//
	//get
	//
	int getMinLen(void);
	//
	//interface
	//
	void draw(Graphics^ g,bool cmd);
	~Truck(void);
};

