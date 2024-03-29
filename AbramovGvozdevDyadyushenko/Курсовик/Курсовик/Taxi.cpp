#include "StdAfx.h"
#include "Taxi.h"


Taxi::Taxi(void):Car()
{
	xStop = 200;
	yStop = 70;
}
Taxi::Taxi(int s,int l,int x,int y,int ml,int xs,int ys):Car(s,l,x,y,ml)
{
	xStop = xs;
	yStop = ys;
	isStop = false;
	TimeStop = 0;
}
//
//get
//
int Taxi::getSX(void)
{
	return xStop;
}
int Taxi::getSY(void)
{
	return yStop;
}
int Taxi::getTimeStop(void)
{
	return TimeStop;
}
bool Taxi::getIsStop(void)
{
	return isStop;
}
//
//set
//
void Taxi::setTimeStop(int ts)
{
	(ts >= 0) ? TimeStop = ts : TimeStop = 0;
}
void Taxi::setIsStop(bool is)
{
	isStop = is;
}
//
//interface
//
void Taxi::draw(Graphics^ g,bool cmd)
{
	(cmd)? g->FillRectangle(gcnew SolidBrush(Color::Yellow),getX(),getY(),getLength(),30): g->FillRectangle(gcnew SolidBrush(Color::White),getX(),getY(),getLength(),30);
}
Taxi::~Taxi(void)
{
}
