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
public class Weapon extends ObjectOnMap {
    
    private int type; //тип оружия
    private String nameWeapon; //наименование
    private int rate; //скорострельность
    private int killability; //поражающая способность
    
    
    public Weapon()
    {
        super();
        type = 1;
        nameWeapon = set_nameWeapon(type);
        rate = 0;
        killability = 0;
    }
    
    public Weapon(int _x, int _y, int _weight, int _type, int _rate, int _killability)
    {
        super(_x, _y, _weight);
        type = _type;
        
        nameWeapon = set_nameWeapon(_type);
        rate = _rate;
        killability = _killability;
    }
    
    
    public int get_type()
    {
        return type;
    }
    
    public String get_nameWeapon()
    {
        return nameWeapon;
    }
    
    public int get_rate()
    {
        return rate;
    }
    
    public int get_killability()
    {
        return killability;
    }
    
    protected static String set_nameWeapon(int _type)
    {
        String str = "";
        
        switch (_type) {
            
            case 1 :
                str = "Пистолет";
            break;
                
            case 2 :
                str = "Автомат";
            break;
                
            case 3 :
                str = "Пулемёт";
            break;
                
        }
        
        return str;
    }
    
    public void set_type(int _type)
    {
        type = _type;
        nameWeapon = set_nameWeapon(type);
    }
    
    public void set_rate(int _rate)
    {
        rate = _rate;
    }
    
    public void set_killability(int _killability)
    {
        killability = _killability;
    }
    
    
    public void draw(Graphics g)
    {
        int x = this.get_xOnPaintField();
        int y = this.get_yOnPaintField();
        
        Color newColor = new Color(0, 0, 5);
        g.setColor(newColor);
        
        g.fillRect(x + 1, y + 2, 13, 3);
        g.fillRect(x + 5, y + 2, 3, 8);
        g.fillRect(x + 11, y + 3, 3, 9);
        
    }
            
    
}
