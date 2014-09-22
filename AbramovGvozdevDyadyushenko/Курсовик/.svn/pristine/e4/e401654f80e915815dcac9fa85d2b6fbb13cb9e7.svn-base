#pragma once
#include "Truck.h"
class Bus : public Truck
{
	int xStop;
	int yStop;
	int TimeStop;
	bool isStop;
public:
	Bus(void);
	Bus(int s,int l,int x,int y,int ml,int xs,int ys);
	//
	//get
	//
	virtual int getSY(void);
	virtual int getSX(void);
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
	~Bus(void);
};

