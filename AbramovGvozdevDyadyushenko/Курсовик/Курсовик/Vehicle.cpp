#include "StdAfx.h"
#include "Vehicle.h"


Vehicle::Vehicle(void)
{
	speed = 60;
	length = 40;
	x = 0;
	y = 0;
}
Vehicle::Vehicle(int s,int l,int dx,int dy)
{
	setSpeed(s);
	setLength(l);
	setX(dx);
	setY(dy);
	(s>=0)? stdSpeed = s : stdSpeed = 60;
}

//
//set
//
void Vehicle::setSpeed(int s)
{
	(s>=0)? speed = s : speed = 0;
}
void Vehicle::setLength(int l)
{
	(l>0)? length = l : length = 40;
}
void Vehicle::setX(int dx)
{
	x = dx;
}
void Vehicle::setY(int dy)
{
	y = dy;
}

//
//get
//
int Vehicle::getSpeed(void)
{
	return speed;
}
int Vehicle::getLength(void)
{
	return length;
}
int Vehicle::getX(void)
{
	return x;
}
int Vehicle::getY(void)
{
	return y;
}
int Vehicle::getStdSpeed(void)
{
	return stdSpeed;
}
//
//interface
//
void Vehicle::draw(Graphics^ g,bool cmd)
{
	
}
void Vehicle::speedUp(void)
{
	setSpeed(getSpeed()+5);
}
void Vehicle::speedDown(void)
{
	setSpeed(getSpeed()-15);
}
void Vehicle::stop(void)
{
	setSpeed(0);
}
Vehicle::~Vehicle(void)
{
}
