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
public class People extends Personage {
    
    private int[][] weapons; //имеющееся оружие
    private int[] patrons; //имеющиеся патроны
    private int[] keys; //имеющиеся ключи
    
    
    public boolean getKeyIndex(int i)
    {
        if (keys[i] >= 1)
            return true;
        
        return false;      
    }
    
    public People()
    {
        super();
    }
    
    public People(int _x, int _y, int _weight, String _name, int _health, int _radius, int _velocity)
    {
        super(_x, _y, _weight + 50, _name, _health, _radius, _velocity);
        
        int kolTypeWeapons = 5;
        int kolTypeKeys = 5;
        
        weapons = new int[kolTypeWeapons][2];
        patrons = new int[kolTypeWeapons];
        keys = new int[kolTypeKeys];
        
        for (int i = 0; i < kolTypeWeapons; i++)
        {
            weapons[i][0] = 0;
            weapons[i][1] = 0;
            patrons[i] = 0;
        }
        
        for (int i = 0; i < kolTypeKeys; i++)
            keys[i] = 0;
    }
    
    
    public void draw(Graphics g)
    {
        int x = this.get_xOnPaintField();
        int y = this.get_yOnPaintField();
        
        Color newColor = new Color(34, 177, 76);
        g.setColor(newColor);
 
        g.fillOval(x + 5, y + 1, 7, 7);
        g.fillRect(x + 7, y + 3, 3, 11);
        g.fillRect(x + 4, y + 8, 9, 2);
        g.fillRect(x + 4, y + 12, 9, 2);
    }
    
    
    public void takeMedicineChest(MedicineChest medicineChest)
    {
        this.set_health( this.get_health() + medicineChest.get_healthLevel() );
        Global.list.remove(medicineChest);
        Global.kolObjOnMap--;
        Global.kolMedicineChest--;
    }
    
    public void takeWeapon(Weapon weapon)
    {
        this.set_weight( this.get_weight() + weapon.get_weight() );
        weapons[ weapon.get_type() ][0] = weapon.get_rate();
        weapons[ weapon.get_type() ][1] = weapon.get_killability();
        Global.list.remove(weapon);
        Global.kolObjOnMap--;
        Global.kolWeapons--;
    }
    
    public void takePatrons(Patrons patron)
    {
        this.set_weight( this.get_weight() + patron.get_weight() );
        patrons[ patron.get_typeWeapons() ] = patron.get_kol();
        Global.list.remove(patron);
        Global.kolObjOnMap--;
        Global.kolPatrons--;
    }
    
    public void takeKey(Key key)
    {
        this.set_weight( this.get_weight() + key.get_weight() );
        keys[ key.get_typeDoor() ] = 1;
        Global.list.remove(key);
        Global.kolObjOnMap--;
        Global.kolKey--;
    }
    
    public boolean shoot(Personage personage)
    {
        for (int i = 0; i < weapons.length; i++)
            if (weapons[i][0] > 0 && patrons[i] > 0)
            {
                int damage = weapons[i][0] * weapons[i][1];
                patrons[i]--;
                
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
                
            }
        
        return false;
    }
    
    public boolean canYouShoot()
    {
        for (int i = 0; i < weapons.length; i++)
            if (weapons[i][0] > 0 && patrons[i] > 0)
                return true;
        
        return false;
    }
    
    public boolean haveYouKey()
    {
        for (int i = 0; i < keys.length; i++)
            if (keys[i] > 0)
                return true;
        
        return false;
    }
  
}
