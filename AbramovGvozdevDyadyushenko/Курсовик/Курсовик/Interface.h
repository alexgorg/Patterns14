#pragma once
using namespace System::Drawing;
class Interface
{
public:
	virtual void speedUp(void) = 0;
	virtual void speedDown(void) = 0;
	virtual void stop(void) = 0;
	virtual void draw(Graphics^ g, bool cmd) = 0;
};

