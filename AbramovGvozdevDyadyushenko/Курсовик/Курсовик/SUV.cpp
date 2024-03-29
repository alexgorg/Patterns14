#include "StdAfx.h"
#include "SUV.h"


SUV::SUV(void)
{
	isRoad = true;
}
SUV::SUV(int s,int l,int dx,int dy)
{
	isRoad = true; 
	SUV::setSpeed(s);
	SUV::setLength(l);
	Vehicle::setX(dx);
	Vehicle::setY(dy);
}
//
//get
//
int SUV:: getIsRoad(void)
{
	return isRoad;
}
//
//set
//
void SUV::setIsRoad(bool isr)
{
	isRoad = isr;
}
void SUV::setSpeed(int s)
{
	(isRoad) ? Vehicle::setSpeed(s) : Vehicle::setSpeed(5);
}
void SUV::setLength(int l)
{
	(l > 0 ) ? Vehicle::setLength(l) : Vehicle::setLength(45);
}
void SUV::setY(int dy)
{
	Vehicle::setY(dy);
}
//
//interface
//
void SUV::draw(Graphics^ g , bool cmd)
{
	(cmd)? g->FillRectangle(gcnew SolidBrush(Color::Gray),Vehicle::getX(),Vehicle::getY(),Vehicle::getLength(),30): g->FillRectangle(gcnew SolidBrush(Color::White),Vehicle::getX(),Vehicle::getY(),Vehicle::getLength(),30);
}
SUV::~SUV(void)
{
}
