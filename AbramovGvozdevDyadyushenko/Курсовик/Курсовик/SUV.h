//
//����� "�����������"
//
#pragma once
#include "Truck.h"
#include "Car.h"
#include "Vehicle.h"

class SUV : virtual public Truck , public Car
{
	int isRoad;// 0 - �� �������, 1 - �� ������.
public:
	SUV(void);
	SUV(int s,int l,int dx,int dy);
	//
	//get
	//
	virtual int getIsRoad(void);
	//
	//set
	//
	virtual void setIsRoad(bool isr);
	virtual void setSpeed(int s);
	virtual void setLength(int l);
	virtual void setY(int dy);
	//
	//interface
	//
	virtual void draw(Graphics^ g , bool cmd);

	~SUV(void);
};

