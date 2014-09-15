/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package labyrinth;

import java.awt.Color;
import java.awt.Graphics;

/**
 *
 * @author Dima
 */
public class ArmorPeople extends People {
    
    private int armorLevel; //уровень брони
    
    public ArmorPeople()
    {
        super();
        armorLevel = 0;
    }
    
    public ArmorPeople(int _x, int _y, int _weight, String _name, int _health, int _radius, int _velocity, int _armorLevel)
    {
        super(_x, _y, _weight, _name, _health, _radius, _velocity);
        armorLevel = _armorLevel;
    }
    
    
    public int get_armorLevel()
    {
        return armorLevel;
    }
    
    public void set_armorLevel(int _armorLevel)
    {
        armorLevel = _armorLevel;
    }
    
    
    public void draw(Graphics g)
    {
        int x = this.get_xOnPaintField();
        int y = this.get_yOnPaintField();
        
        Color newColor = new Color(34, 177, 76);
        g.setColor(newColor);
 
        g.fillOval(x + 5, y + 1, 7, 7);
        
        Color color = new Color(0, 128, 255);
        g.setColor(color);
        
        g.fillRect(x + 5, y + 7, 7, 7);
          
    }

    public int defence(int damage)
    {
        set_armorLevel( get_armorLevel() - damage );
        if (get_armorLevel() < 0)
        {
            damage = (-1) * get_armorLevel();
            set_armorLevel(0);
        }
        else
            damage = 0;
        
        return damage;
    }
    
}
