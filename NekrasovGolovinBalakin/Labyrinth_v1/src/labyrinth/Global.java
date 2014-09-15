/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package labyrinth;

import java.awt.Color;
import java.awt.Graphics;
import java.io.BufferedReader;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Random;
import java.util.Scanner;
import java.util.logging.Level;
import java.util.logging.Logger;
import javax.swing.Timer;

/**
 *
 * @author Dima
 */
public class Global {
    
    public static int startSizeWall; //размер одного блока стены
    public static int startX, startY; //начальные координаты
    public static int[][] lab; //сама карта
    public static int N, M; //размеры массива lab
    public static ArrayList<ObjectOnMap> list; //список объектов на карте
    
    public static int kolObjOnMap; //количество объектов на карте
    public static int kolPeople, kolArmorPeople, kolOtherPeople, kolBeasts, kolWeapons, kolPatrons, kolMedicineChest, kolKey;
    
    public static ArrayList<String> listName; //список имён
    public static int kolName; //количество имён
    
    public static Timer timer;
    
    
    public static void generateListName()
    {
        listName = new ArrayList<>();
        kolName = 0;
        
        try {
            FileReader fileReader = new FileReader("input_name.txt");
            
            BufferedReader reader = new BufferedReader(fileReader);
            
            String line;
            while ( (line = reader.readLine()) != null)
            {
                listName.add(line);
                kolName++;
            }       
            
        } catch (FileNotFoundException ex) {
            Logger.getLogger(Global.class.getName()).log(Level.SEVERE, null, ex);
        } catch (IOException ex) {
            Logger.getLogger(Global.class.getName()).log(Level.SEVERE, null, ex);
        }
        
    }
    
    
    public static void CreateList()
    {
        list = new ArrayList<>();
        
        kolObjOnMap = generateRandom(50) + 100;
        System.out.println("Кол-во объектов на карте: " + kolObjOnMap);
        
        for (int i = 0; i < kolObjOnMap; i++)
        {
            int x, y; //координаты
            
            while (true)
            {
                x = generateRandom(N);
                y = generateRandom(M);
                
                if (lab[x][y] == 0)
                {
                    lab[x][y] = -1;  //чтобы объекты на карте изначально не наслаивались друг на друга
                    break;
                }
            }
            
            int weight = generateRandom(25) + 1;
            
            
            ObjectOnMap obj = new ObjectOnMap();
            int rand = generateRandom(7);
            
            switch (rand) {
                
                case 0 :
                {
                    int type = generateRandom(3) + 1;
                    int rate = generateRandom(70) + 5000;
                    int killability = generateRandom(25) + 1000;
                    
                    obj = new Weapon(x, y, weight, type, rate, killability);
                }
                break;
                    
                case 1 :
                {
                    int kol = generateRandom(70) + 5000;
                    int typeWeapons = generateRandom(3) + 1;
                    
                    obj = new Patrons(x, y, weight, kol, typeWeapons);
                }
                break;
                    
                case 2 :
                {
                    int healthLevel = generateRandom(25) + 30;
                    
                    obj = new MedicineChest(x, y, healthLevel);
                }
                break;
                    
                case 3 :
                {
                    int typeDoor = generateRandom(3) + 2;
                    
                    obj = new Key(x, y, weight, typeDoor);
                }
                break;
                    
                default :
                {   
                    int nameIndex = generateRandom(kolName);
                    String name = listName.get(nameIndex);
                    listName.remove(nameIndex);
                    kolName--;
                    
                    int health = generateRandom(100) + 100;
                    int radius = generateRandom(5) + 1;
                    int velocity = generateRandom(10) + 2;
                    
                    switch (rand) {
                            
                        case 4 :
                        {
                            int armorLevel = generateRandom(26) + 30;
                            
                            obj = new ArmorPeople(x, y, weight, name, health, radius, velocity, armorLevel);
                        }
                        break;
                            
                        case 5 :
                        {
                            obj = new OtherPeople(x, y, weight, name, health, radius, velocity);
                        }
                        break;
                            
                        case 6 :
                        {
                            obj = new Beasts(x, y, weight, name, health, radius, velocity);
                        }
                        break;
                            
                    }
                       
                } //end default
                    
            } //end switch
            
            
            list.add(obj);
            
            
             
        }
             
    }
    
    
    public static void PainLabyrinth(Graphics g)
    {        
        
        startSizeWall = 15; //размер одного блока стены
        
        for (int i = 0, y = 0; i < N; i++, y += startSizeWall)
            for (int j = 0, x = 0; j < M; j++, x += startSizeWall)
            {
                if (lab[i][j] == 1)
                    PaintWall(g, startSizeWall, 0, 0, 91, x, y); //стена
                if (lab[i][j] == 2)
                    PaintWall(g, startSizeWall, 255, 0, 0, x, y); //дверь 1
                if (lab[i][j] == 3)
                    PaintWall(g, startSizeWall, 185, 122, 87, x, y); //дверь 2
                if (lab[i][j] == 4)
                    PaintWall(g, startSizeWall, 255, 201, 14, x, y); //дверь 3
            }
          
        
    }
    
    
    public static void PaintWall(Graphics g, int size, int R, int G, int B, int x, int y)
    {
        startX = 0;
        startY = 0;
        
        Color wall = new Color(R, G, B);
        g.setColor(wall);
        g.fillRect(startX + x, startY + y, size, size);
    }
    
    
    public static void OpenFile()
    {
        int rand = generateRandom(3);
        System.out.println("rand == " + rand);
        String fileName = "";
        
        switch (rand) {          
            case 0 : fileName = "input1.txt"; break;
            case 1 : fileName = "input2.txt"; break;
            case 2 : fileName = "input3.txt"; break;
        }
        
        
        
        File file = new File(fileName);
        if (file.exists())
            System.out.println("Good! Labyrinth in file \"" + fileName + "\"");
        else
            System.out.println("No good");

        try {
            
            Scanner scanner = new Scanner(file);
            
            N = scanner.nextInt();
            M = scanner.nextInt();
            
            lab = new int[N][M];
            
            for (int i = 0; i < N; i++)
                for (int j = 0; j < M; j++)
                    lab[i][j] = scanner.nextInt();
            
        } catch (FileNotFoundException ex) {
            Logger.getLogger(Global.class.getName()).log(Level.SEVERE, null, ex);
        }
        
    }
    
    
    public static int generateRandom(int n)
    {
        Random random = new Random();
        return Math.abs(random.nextInt()) % n;
    }

    
    public static String aboutKolStr()
    {
        String str = "";
        str += "Бронилюди: " + String.valueOf(kolArmorPeople);
        str += "\nДругие люди: " + String.valueOf(kolOtherPeople);
        str += "\nЖивотные: " + String.valueOf(kolBeasts);
        str += "\nОружие: " + String.valueOf(kolWeapons);
        str += "\nПатроны: " + String.valueOf(kolPatrons);
        str += "\nАптечки: " + String.valueOf(kolMedicineChest);
        str += "\nКлючи: " + String.valueOf(kolKey);
        return str;   
    }
        
    
    
}
