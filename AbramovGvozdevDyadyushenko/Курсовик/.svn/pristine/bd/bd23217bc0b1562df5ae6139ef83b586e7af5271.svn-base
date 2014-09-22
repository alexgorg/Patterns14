#include "StdAfx.h"
#include "Truck.h"


Truck::Truck(void):Vehicle()
{
	MinLen = 30;
}
Truck::Truck(int s,int l,int dx,int dy,int ml):Vehicle(s,l,dx,dy)
{
	setMinLen(ml);
}
//
//set
//
void Truck::setMinLen(int l)
{
	(l>0)? MinLen = l : MinLen = 20;
}
void Truck::setLength(int l)
{
	(l<=MinLen)? Vehicle::setLength(l) : Vehicle::setLength(MinLen);
}
//
//get
//
int Truck::getMinLen(void)
{
	return MinLen;
}
void Truck::draw(Graphics^ g,bool cmd)
{
	(cmd)? g->FillRectangle(gcnew SolidBrush(Color::Blue),getX(),getY(),getLength(),30): g->FillRectangle(gcnew SolidBrush(Color::White),getX(),getY(),getLength(),30);
}
Truck::~Truck(void)
{
}
