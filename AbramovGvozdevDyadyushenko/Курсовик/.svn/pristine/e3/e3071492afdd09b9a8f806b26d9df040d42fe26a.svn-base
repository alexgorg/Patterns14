#include "StdAfx.h"
#include "Car.h"


Car::Car(void):Vehicle()
{
	MaxLength = 60;
}
Car::Car(int s,int l,int dx,int dy,int ml):Vehicle(s,l,dx,dy)
{
	setMaxLength(ml);
}
//
//set
//
void Car::setMaxLength(int ml)
{
	(ml>0)? MaxLength = ml : MaxLength = 60;
	
}
void Car::setLength(int l)
{
	(l<=MaxLength)? Vehicle::setLength(l) : Vehicle::setLength(MaxLength);
}
//
//get
//
int Car::getMaxLength(void)
{
	return MaxLength;
}
//
//interface
//
void Car::draw(Graphics^ g, bool cmd)
{
	(cmd)? g->FillRectangle(gcnew SolidBrush(Color::Red),getX(),getY(),getLength(),30): g->FillRectangle(gcnew SolidBrush(Color::White),getX(),getY(),getLength(),30);
}
Car::~Car(void)
{
}
