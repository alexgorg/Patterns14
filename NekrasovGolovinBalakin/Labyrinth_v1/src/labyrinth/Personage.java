/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package labyrinth;

import java.util.ArrayList;
import javax.swing.JPanel;

/**
 *
 * @author Dima
 */
public class Personage extends ObjectOnMap {
    
    private String name; //имя
    private int health; //здоровье
    private int radius; //радиус видимости
    private int velocity; //скорость перемещения v == f(weight)
    private int kostyl;
    private int lastX, lastY;
    
    
    public int get_lastX()
    {
        return lastX;
    }
    
    public int get_lastY()
    {
        return lastY;
    }
    
    public void set_lastX(int _lastX)
    {
        lastX = _lastX;
    }
    
    public void set_lastY(int _lastY)
    {
        lastY = _lastY;
    }
    
    
    public Personage()
    {
        super();
        name = "";
        health = 0;
        radius = 0;
        velocity = 0;
    }
    
    public Personage(int _x, int _y, int _weight, String _name, int _health, int _radius, int _velocity)
    {
        super(_x, _y, _weight);
        name = _name;
        health = _health;
        radius = _radius;
        velocity = _velocity;
        kostyl = _velocity;
        lastX = lastY = 0;
    }
    
    
    public int get_kostyl()
    {
        return kostyl;
    }
    
    public void set_kostyl(int _kostyl)
    {
        kostyl = _kostyl;
    }
    
    
    public String get_name()
    {
        return name;
    }
    
    public int get_health()
    {
        return health;
    }
    
    public int get_radius()
    {
        return radius;
    }
    
    public int get_velocity()
    {
        return velocity;
    }
    
    public void set_name(String _name)
    {
        name = _name;
    }
    
    public void set_health(int _health)
    {
        health = _health;
    }
    
    public void set_radius(int _radius)
    {
        radius = _radius;
    }
    
    public void set_weight(double _weight)
    {
        this.set_weight(_weight);
        velocity = 100 / (int)this.get_weight();
    }
    
    public void set_velocity(int _velocity)
    {
        velocity = _velocity;
    }
    
    public void step(JPanel jPanel)
    {
        int xDraw = this.get_xOnPaintField();
        int yDraw = this.get_yOnPaintField();
        
        int x = this.get_x();
        int y = this.get_y();
        
        ArrayList<String> side = new ArrayList<>();
        int kolSide = 0;
        if (Global.lab[x][y - 1] <= 0 && (x != lastX || y - 1 != lastY))
        {
            side.add("left");
            kolSide++;
        }
        if (Global.lab[x][y + 1] <= 0 && (x != lastX || y + 1 != lastY))
        {
            side.add("right");
            kolSide++;
        }
        if (Global.lab[x - 1][y] <= 0 && (x - 1 != lastX || y != lastY))
        {
            side.add("top");
            kolSide++;
        }
        if (Global.lab[x + 1][y] <= 0 && (x + 1 != lastX || y != lastY))
        {
            side.add("bottom");
            kolSide++;
        }
        
        //проверка на двери
        if (this instanceof People)
        {
            People people = (People)this;
            
            for (int i = 2; i <= 4; i++)
            {
                if (Global.lab[x][y - 1] == i && people.getKeyIndex(i) && (x != lastX || y - 1 != lastY)) 
                    { side.add("left"); kolSide++; }
                if (Global.lab[x][y + 1] == i && people.getKeyIndex(i) && (x != lastX || y + 1 != lastY)) 
                    { side.add("right"); kolSide++; }
                if (Global.lab[x - 1][y] == i && people.getKeyIndex(i) && (x - 1 != lastX || y != lastY)) 
                    { side.add("top"); kolSide++; }
                if (Global.lab[x + 1][y] == i && people.getKeyIndex(i) && (x + 1 != lastX || y != lastY)) 
                    { side.add("bottom"); kolSide++; }
            }
              
        }
        
        lastX = x;
        lastY = y;
        
        if (kolSide != 0)
        {
            int rand = Global.generateRandom(kolSide);

            if (side.get(rand).equals("left"))
                y--;
            if (side.get(rand).equals("right"))
                y++;
            if (side.get(rand).equals("top"))
                x--;
            if (side.get(rand).equals("bottom"))
                x++; 
        }

        
        set_x(x);
        set_y(y);
        
        jPanel.paintImmediately(xDraw, yDraw, 15, 15);
        
        if (this instanceof OtherPeople)
        {
            OtherPeople otherPeople = (OtherPeople)this;
            otherPeople.recoveryHealth();
        }
        
    }
    
}