using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maze
{
    class NPC:Position
    {
        String name;
        int hp;
        int damage;
        int radius;
        int ammo;
        int status;//0-life 1-death

        public NPC(String nam, int hp1, int dmg, int x1, int y1, int amm)
        {
            damage = dmg;
            hp = hp1;
            x = x1;
            y = y1;
            name = nam;
            radius = 3;
            status = 0;
            ammo = amm;
            //        throw new UnsupportedOperationException("Not yet implemented");

        }

        public NPC() { }

        public String getName()
        {
            return name;
        }

        public int getDamage()
        {
            return damage;
        }

        public int getHp()
        {
            return hp;
        }

        public int getAmmo()
        {
            return ammo;
        }

        public int shoot()
        {
            int t;
            if (ammo != 0)
            {
                ammo--;
                t = damage;
            }
            else t = 1;
            return t;
        }

        public int Radius()
        {
            return radius;
        }

        public void heal(int h)
        {
            hp += h;
            if (hp > 100) hp = 100;
        }

        public void upAmmo(int am)
        {
            ammo += am;
        }

        public void wound(int dd)
        {
            if (hp > dd) hp -= dd;
            else
            {
                hp = 0;
                name = "DEAD";
                status = 1;
            }
        }

        public int Status()
        {
            return status;
        }

        public void puhpuh()
        {
            ammo--;
        }
        public void setStatus(int i)
        {
            status = i;
        }
    }
}
