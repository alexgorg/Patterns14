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
public class OtherPeople extends People {
    
    public OtherPeople()
    {
        super();
    }
    
    public OtherPeople(int _x, int _y, int _weight, String _name, int _health, int _radius, int _velocity)
    {
        super(_x, _y, _weight, _name, _health, _radius, _velocity);
    }
    
    
    public void draw(Graphics g)
    {
        int x = this.get_xOnPaintField();
        int y = this.get_yOnPaintField();

        Color newColor = new Color(34, 177, 76);
        g.setColor(newColor);
        
        g.fillRect(x + 7, y + 3, 3, 11);
        g.fillRect(x + 4, y + 8, 9, 2);
        g.fillRect(x + 4, y + 12, 9, 2);
        
        Color color = new Color(163, 73, 164);
        g.setColor(color);
 
        g.fillOval(x + 5, y + 1, 7, 7);
    }
    
    public void recoveryHealth()
    {
        if (this.get_health() < 100)
            this.set_health( this.get_health() + 1 );
    }
    
}
