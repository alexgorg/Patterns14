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
public class MedicineChest extends ObjectOnMap {
    
    private int healthLevel; //уровень здоровья
    
    
    public MedicineChest()
    {
        super();
        healthLevel = 0;
    }
    
    public MedicineChest(int _x, int _y, int _healthLevel)
    {
        super(_x, _y, 0); //вес аптечки == 0
        healthLevel = _healthLevel;
    }
    
    
    public int get_healthLevel()
    {
        return healthLevel;
    }
    
    public void set_healthLevel(int _healthLevel)
    {
        healthLevel = _healthLevel;
    }
    
    
    public void draw(Graphics g)
    {
        Color newColor = new Color(255, 0, 0);
        g.setColor(newColor);
        
        int x = this.get_xOnPaintField();
        int y = this.get_yOnPaintField();
        
        g.drawRect(x + 2, y + 2, 11, 11);
        g.fillRect(x + 2 + 4, y + 2, 4, 11);
        g.fillRect(x + 2, y + 2 + 4, 11, 4);
    }
    
}
