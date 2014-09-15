/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package labyrinth;

import java.awt.Graphics;

/**
 *
 * @author Dima
 */
public class ObjectOnMap {
    
    private int x, y; //координаты
    private int weight; //вес
    
    
    public ObjectOnMap()
    {
        x = 0;
        y = 0;
        weight = 0;
    }
    
    public ObjectOnMap(int _x, int _y, int _weight)
    {
        x = _x;
        y = _y;
        weight = _weight;
    }
    
    
    public int get_x()
    {
        return x;
    }
    
    public int get_y()
    {
        return y;
    }
    
    public int get_weight()
    {
        return weight;
    }
    
    public void set_x(int _x)
    {
        x = _x;
    }
    
    public void set_y(int _y)
    {
        y = _y;
    }
    
    public void set_weight(int _weight)
    {
        weight = _weight;
    }
    
    public void draw(Graphics g)
    {  
    }

    
    public int get_xOnPaintField()
    {
        return Global.startX + Global.startSizeWall * get_y();
    }
    
    public int get_yOnPaintField()
    {
        return Global.startY + Global.startSizeWall * this.get_x(); 
    }

}