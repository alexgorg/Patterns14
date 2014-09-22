#pragma once
#include "Car.h"

class Taxi : public Car
{
	int xStop;
	int yStop;
	int TimeStop;
	bool isStop;
public:
	Taxi(void);
	Taxi(int s,int l,int x,int y,int ml,int xs,int ys);
	//
	//get
	//
	virtual int getSX(void);
	virtual int getSY(void);
	virtual int getTimeStop(void);
	virtual bool getIsStop(void);
	//
	//set
	//
	virtual void setTimeStop(int ts);
	virtual void setIsStop(bool is);
	//
	//interface
	//
	virtual void draw(Graphics^ g,bool cmd);
	~Taxi(void);
};

