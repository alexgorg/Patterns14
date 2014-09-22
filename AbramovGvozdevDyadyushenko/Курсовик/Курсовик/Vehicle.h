#pragma once
using namespace System::Drawing;

class Vehicle
{
	int x,y;
	int speed;
	int length;
	int stdSpeed;
public:
	Vehicle(void);
	Vehicle(int s,int l,int dx,int dy);
	//
	//set
	//
	virtual void setSpeed(int s);
	virtual void setLength(int l);
	virtual void setX(int dx);
	virtual void setY(int dy);
	//
	//get
	//
	virtual int getSpeed(void);
	virtual int getLength(void);
	virtual int getX(void);
	virtual int getY(void);
	virtual int getStdSpeed(void);

	virtual void draw(Graphics^ g,bool cmd);
	virtual void speedUp(void);
	virtual void speedDown(void);
	virtual void stop(void);
	~Vehicle(void);
};

