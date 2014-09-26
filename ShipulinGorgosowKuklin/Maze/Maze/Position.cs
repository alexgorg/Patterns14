using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maze
{
    class Position
    {
        protected int x;
        protected int y;

        public void setX(int xx)
        {
            x = xx;
        }

        public void setY(int yy)
        {
            y = yy;
        }

        public int getX()
        {
            return x;
        }

        public int getY()
        {
            return y;
        }

        public void setPosition(int xx, int yy)
        {
            x = xx;
            y = yy;
        }

    }
}
