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
public class Patrons extends ObjectOnMap {
    
    private int kol; //количество
    private int typeWeapons; //тип оружия
    private String nameTypeWeapons; //название типа оружия
    
    
    public Patrons()
    {
        super();
        kol = 0;
        typeWeapons = 1;
        nameTypeWeapons = "";
    }
    
    public Patrons(int _x, int _y, int _weight, int _kol, int _typeWeapons)
    {
        super(_x, _y, _weight);
        kol = _kol;
        typeWeapons = _typeWeapons;
        nameTypeWeapons = Weapon.set_nameWeapon(_typeWeapons);
    }
    
    
    public int get_kol()
    {
        return kol;
    }
    
    public int get_typeWeapons()
    {
        return typeWeapons;
    }
    
    public String get_nameTypeWeapons()
    {
        return nameTypeWeapons;
    }
    
    public void set_kol(int _kol)
    {
        kol = _kol;
    }
    
    public void set_typeWeapons(int _typeWeapons)
    {
        typeWeapons = _typeWeapons;
    }
         
    
    public void draw(Graphics g)
    {
        int x = this.get_xOnPaintField();
        int y = this.get_yOnPaintField();
        
        Color newColor = new Color(127, 127, 127);
        g.setColor(newColor);
        
        g.fillRect(x + 2, y + 3, 3, 10);
        g.fillRect(x + 6, y + 3, 3, 10);
        g.fillRect(x + 10, y + 3, 3, 10);
        
    }
    
    
}
