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
public class Beasts extends Personage {
    
    public Beasts()
    {
        super();
    }
    
    public Beasts(int _x, int _y, int _weight, String _name, int _health, int _radius, int _velocity)
    {
        
        super(_x, _y, _weight + 100, _name, _health, _radius, _velocity);
        int tmp = this.get_velocity();
        this.set_velocity(tmp / 2);
    }
    
    
    public void draw(Graphics g)
    {
        int x = this.get_xOnPaintField();
        int y = this.get_yOnPaintField();
        
        Color newColor = new Color(150, 0, 0);
        g.setColor(newColor);
 
        g.fillOval(x + 1, y + 5, 13, 8);
        g.fillRect(x + 2, y + 7, 3, 7);
        g.fillRect(x + 10, y + 7, 3, 7);
        g.fillRect(x + 5, y + 2, 5, 5);
        
    }
    
    public boolean damage(Personage personage)
    {
        int damage = 10;
        if (personage instanceof ArmorPeople)
        {
            ArmorPeople armorPeople = (ArmorPeople)personage;
            damage = armorPeople.defence(damage);
        }
        
        personage.set_health( personage.get_health() - damage );
        
        if (personage.get_health() <= 0)
        {
            Global.kolObjOnMap--;
            if (personage instanceof ArmorPeople) Global.kolArmorPeople--;
            if (personage instanceof OtherPeople) Global.kolOtherPeople--;
            if (personage instanceof Beasts) Global.kolBeasts--;
            
            Global.list.remove(personage);
            
            return true;
        }
        
        return false;
    }
    
}
