#pragma once
class Auto
{
public:
	Auto(void);
	virtual ~Auto(void);
protected:
	int speed;
	int length;
	int position;
	int cspeed;
	int ntr;
	int torm;

public:
	virtual void info() ;
	virtual int Paint(char*r,char*o)=0 ;
	virtual void Setspeed(int a) {speed=a;};
	virtual void Setcspeed(int a) {if(a>0) if(a>speed) cspeed=speed; else cspeed=a; else cspeed=0;};
	virtual void Setlength(int a) {length=a;};
	int Getspeed() const {return speed;};
	int Gettorm() const {return torm;};
	int Getcspeed() const {return cspeed;};
	int Getlength() const {return length;};
	virtual void Setposition(int a) {position=a;};
	int Getposition() const {return position;};
	virtual int Drive(char*r,char*o, Auto *predok);
	Auto(int a,int b);
	Auto(const Auto &copy);
	
};

