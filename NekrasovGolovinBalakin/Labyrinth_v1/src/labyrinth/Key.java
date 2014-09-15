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
public class Key extends ObjectOnMap {
    
    private int typeDoor; //тип двери
    
    public Key()
    {
        super();
        typeDoor = 1;
    }
    
    public Key(int _x, int _y, int _weight, int _typeDoor)
    {
        super(_x, _y, _weight);
        typeDoor = _typeDoor;
    }
    
    
    public int get_typeDoor()
    {
        return typeDoor;
    }
    
    public void set_typeDoor(int _typeDoor)
    {
        typeDoor = _typeDoor;
    }
    
    
    public void draw(Graphics g)
    {
        int x = this.get_xOnPaintField();
        int y = this.get_yOnPaintField();
        
        Color newColor = null;

        switch(typeDoor) {
            case 2 : newColor = new Color(255, 0, 0); break;
            case 3 : newColor = new Color(185, 122, 87); break;
            case 4 : newColor = new Color(255, 201, 14); break;
        }
        
        g.setColor(newColor); 
        
        g.fillRect(x + 5, y + 3, 7, 7);
        g.fillRect(x + 7, y + 3, 3, 11);
        g.fillRect(x + 5, y + 12, 7, 2);
        
        Color color = new Color(0, 0, 3);
        g.setColor(color);
        
        g.fillRect(x + 7, y + 5, 3, 3);
    }
    
}
