/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package labyrinth;

import java.awt.Graphics;
import java.awt.Point;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import javax.swing.Timer;

/**
 *
 * @author Dima
 */
public class Redactor extends javax.swing.JFrame {

    public Redactor() {
        initComponents();
        
        Global.OpenFile();
        
        Global.generateListName();
        
        Global.CreateList();
        
        jLabel1.setText("Кол-во объектов на карте: " + Global.kolObjOnMap);
        
        for (int i = 0; i < Global.kolObjOnMap; i++)
        {
            if (Global.list.get(i) instanceof ArmorPeople)
                Global.kolArmorPeople++;
            if (Global.list.get(i) instanceof OtherPeople)
                Global.kolOtherPeople++;
            if (Global.list.get(i) instanceof Beasts)
                Global.kolBeasts++;
            if (Global.list.get(i) instanceof Weapon)
                Global.kolWeapons++;
            if (Global.list.get(i) instanceof Patrons)
                Global.kolPatrons++;
            if (Global.list.get(i) instanceof MedicineChest)
                Global.kolMedicineChest++;
            if (Global.list.get(i) instanceof Key)
                Global.kolKey++;
        }
      
        jTextPane1.setText(Global.aboutKolStr());
        
        
        Global.timer = new Timer(50, new ActionListener (
                ) {

            @Override
            public void actionPerformed(ActionEvent e) {
                
                for (int i = 0; i < Global.list.size(); i++)
                { 
                    Personage personage;
                    
                    if (Global.list.get(i) instanceof Personage)
                    {
                        personage = (Personage)Global.list.get(i);
                        
                        int tmp = personage.get_kostyl();
                        if (tmp > 1)
                            personage.set_kostyl(tmp - 1);
                        else
                        {
                            personage.step(jPanel1); //шаг на одну клетку
                            personage.set_kostyl( personage.get_velocity() );  
                        }     
                        
                        if (personage instanceof People)
                            {
                                People people = (People)personage;
                                
                                for (int j = 0; j < Global.list.size(); j++)
                                    if (people.get_x() == Global.list.get(j).get_x() && people.get_y() == Global.list.get(j).get_y())
                                    {
                                        if (Global.list.get(j) instanceof MedicineChest)
                                        {
                                            MedicineChest medicineChest = (MedicineChest)Global.list.get(j);
                                            people.takeMedicineChest(medicineChest);
                                            i--;
                                            break;
                                        }
                                        if (Global.list.get(j) instanceof Weapon)
                                        {
                                            Weapon weapon = (Weapon)Global.list.get(j);
                                            people.takeWeapon(weapon);
                                            i--;
                                            break;
                                        }
                                        if (Global.list.get(j) instanceof Patrons)
                                        {
                                            Patrons patrons = (Patrons)Global.list.get(j);
                                            people.takePatrons(patrons);
                                            i--;
                                            break;
                                        }
                                        if (Global.list.get(j) instanceof Key)
                                        {
                                            Key key = (Key)Global.list.get(j);
                                            people.takeKey(key);
                                            i--;
                                            break;
                                        }
                                        if (Global.list.get(j) instanceof Personage && Global.list.get(j).equals(people) == false)
                                        {
                                            Personage personage1 = (Personage)Global.list.get(j);
                                            if (people.shoot(personage1));
                                                i--;
                                            break;
                                        }
                                    }
                            }
                            
                            if (personage instanceof Beasts)
                            {
                                Beasts beast = (Beasts)personage;
                                
                                for (int j = 0; j < Global.list.size(); j++)
                                    if (beast.get_x() == Global.list.get(j).get_x() && beast.get_y() == Global.list.get(j).get_y())
                                    {
                                        if (Global.list.get(j) instanceof Personage && Global.list.get(j).equals(beast) == false)
                                        {
                                            Personage personage1 = (Personage)Global.list.get(j);
                                            if (beast.damage(personage1));
                                                i--;
                                            break;
                                        }
                                    }
                            }
  
                    }
                }
        
                
                
        
                Graphics g1 = jPanel1.getGraphics();
                
                //перерисовка дверей
                for (int i = 0, y = 0; i < Global.N; i++, y += Global.startSizeWall)
                    for (int j = 0, x = 0; j < Global.M; j++, x += Global.startSizeWall)
                    {
                        if (Global.lab[i][j] == 2)
                            Global.PaintWall(g1, Global.startSizeWall, 255, 0, 0, x, y);
                        if (Global.lab[i][j] == 3)
                            Global.PaintWall(g1, Global.startSizeWall, 185, 122, 87, x, y);
                        if (Global.lab[i][j] == 4)
                            Global.PaintWall(g1, Global.startSizeWall, 255, 201, 14, x, y);
                    }
                
                //перерисовка объектов
                for (int i = 0; i < Global.list.size(); i++)
                    Global.list.get(i).draw(g1);
                
                jLabel1.setText("Кол-во объектов на карте: " + Global.kolObjOnMap);
                jTextPane1.setText(Global.aboutKolStr());

                
            }
        });

    }
    
    
    public void paint(Graphics g) {

        System.out.print("1 ");
        
        super.paint(g);
        //рисуем лабиринт
        Graphics g1 = jPanel1.getGraphics();
        Global.PainLabyrinth(g1);
        
        //рисуем объекты на карте
        for (int i = 0; i < Global.kolObjOnMap; i++)
            Global.list.get(i).draw(g1);
    }  
    
   

    /**
     * This method is called from within the constructor to initialize the form.
     * WARNING: Do NOT modify this code. The content of this method is always
     * regenerated by the Form Editor.
     */
    @SuppressWarnings("unchecked")
    // <editor-fold defaultstate="collapsed" desc="Generated Code">//GEN-BEGIN:initComponents
    private void initComponents() {

        jButton1 = new javax.swing.JButton();
        jPanel1 = new javax.swing.JPanel();
        jLabel1 = new javax.swing.JLabel();
        jScrollPane1 = new javax.swing.JScrollPane();
        jTextPane1 = new javax.swing.JTextPane();
        jScrollPane2 = new javax.swing.JScrollPane();
        jTextPane2 = new javax.swing.JTextPane();

        setDefaultCloseOperation(javax.swing.WindowConstants.EXIT_ON_CLOSE);
        setTitle("Лабиринт");
        setBackground(new java.awt.Color(255, 255, 255));
        setCursor(new java.awt.Cursor(java.awt.Cursor.DEFAULT_CURSOR));
        setIconImages(null);
        setLocationByPlatform(true);
        setResizable(false);

        jButton1.setText("Старт");
        jButton1.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jButton1ActionPerformed(evt);
            }
        });

        jPanel1.setBackground(new java.awt.Color(235, 255, 235));
        jPanel1.setPreferredSize(new java.awt.Dimension(660, 360));
        jPanel1.addMouseListener(new java.awt.event.MouseAdapter() {
            public void mouseClicked(java.awt.event.MouseEvent evt) {
                jPanel1MouseClicked(evt);
            }
        });

        javax.swing.GroupLayout jPanel1Layout = new javax.swing.GroupLayout(jPanel1);
        jPanel1.setLayout(jPanel1Layout);
        jPanel1Layout.setHorizontalGroup(
            jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGap(0, 660, Short.MAX_VALUE)
        );
        jPanel1Layout.setVerticalGroup(
            jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGap(0, 360, Short.MAX_VALUE)
        );

        jLabel1.setFont(new java.awt.Font("Tahoma", 1, 14)); // NOI18N
        jLabel1.setText("jLabel1");

        jTextPane1.setEditable(false);
        jScrollPane1.setViewportView(jTextPane1);

        jTextPane2.setEditable(false);
        jTextPane2.setText("Кликните на объект, чтобы посмотреть информацию о нём.");
        jScrollPane2.setViewportView(jTextPane2);

        javax.swing.GroupLayout layout = new javax.swing.GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addComponent(jPanel1, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addComponent(jLabel1)
                    .addGroup(layout.createSequentialGroup()
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.TRAILING, false)
                            .addComponent(jButton1, javax.swing.GroupLayout.Alignment.LEADING, javax.swing.GroupLayout.DEFAULT_SIZE, 117, Short.MAX_VALUE)
                            .addComponent(jScrollPane1, javax.swing.GroupLayout.Alignment.LEADING))
                        .addGap(10, 10, 10)
                        .addComponent(jScrollPane2, javax.swing.GroupLayout.PREFERRED_SIZE, 177, javax.swing.GroupLayout.PREFERRED_SIZE)))
                .addGap(0, 12, Short.MAX_VALUE))
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addComponent(jPanel1, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(0, 0, Short.MAX_VALUE))
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addComponent(jLabel1)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING, false)
                    .addGroup(layout.createSequentialGroup()
                        .addComponent(jScrollPane1, javax.swing.GroupLayout.PREFERRED_SIZE, 125, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(jButton1, javax.swing.GroupLayout.DEFAULT_SIZE, 45, Short.MAX_VALUE))
                    .addComponent(jScrollPane2))
                .addContainerGap(javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );

        getAccessibleContext().setAccessibleDescription("");

        pack();
    }// </editor-fold>//GEN-END:initComponents

    private void jButton1ActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jButton1ActionPerformed
        // TODO add your handling code here:
        String buttonName = jButton1.getText();
        if (buttonName.equals("Старт"))
        {
            jButton1.setText("Пауза");
            Global.timer.start();
        }
        else
        {
            jButton1.setText("Старт");
            Global.timer.stop();
        }
                
    }//GEN-LAST:event_jButton1ActionPerformed

    private void jPanel1MouseClicked(java.awt.event.MouseEvent evt) {//GEN-FIRST:event_jPanel1MouseClicked
        
        Point point = jPanel1.getMousePosition();
        int x0 = point.y / Global.startSizeWall;
        int y0 = point.x / Global.startSizeWall;
        
        for (int i = 0; i < Global.kolObjOnMap; i++)
            if (Global.list.get(i).get_x() == x0 && Global.list.get(i).get_y() == y0)
            {   
                int weight = Global.list.get(i).get_weight(); 
                
                String str =  "Координаты: (" + x0 + ", " + y0 + ")";
                str += "\nВес: " + weight;
                
                if (Global.list.get(i) instanceof ArmorPeople)
                {
                    
                    ArmorPeople armorPeople = (ArmorPeople)Global.list.get(i);
                    
                    str = "Класс: Бронилюди\n" + str;
                    
                    String name = armorPeople.get_name();
                    int health = armorPeople.get_health();
                    int radius = armorPeople.get_radius();
                    int v = armorPeople.get_velocity();
                    int armor = armorPeople.get_armorLevel();
                    
                    str += "\nИмя: " + name;
                    str += "\nЗдоровье: " + health;
                    str += "\nРадиус: " + radius;
                    str += "\nСкорость: " + v;
                    str += "\nУровень брони: " + armor;
                    
                    if (armorPeople.canYouShoot())
                        str += "\nМожет стрелять!!!";
                    
                    if (armorPeople.haveYouKey())
                        str += "\nЕсть ключ.";
  
                }
                
                if (Global.list.get(i) instanceof OtherPeople)
                {
                    OtherPeople otherPeople = (OtherPeople)Global.list.get(i);
                    
                    str = "Класс: Другие люди\n" + str;
                    
                    String name = otherPeople.get_name();
                    int health = otherPeople.get_health();
                    int radius = otherPeople.get_radius();
                    int v = otherPeople.get_velocity();
                    
                    str += "\nИмя: " + name;
                    str += "\nЗдоровье: " + health;
                    str += "\nРадиус: " + radius;
                    str += "\nСкорость: " + v;
                    
                    if (otherPeople.canYouShoot())
                        str += "\nМожет стрелять!!!";
                    
                    if (otherPeople.haveYouKey())
                        str += "\nЕсть ключ.";
                }
                
                if (Global.list.get(i) instanceof Beasts)
                {
                    Beasts beasts = (Beasts)Global.list.get(i);
                    
                    str = "Класс: Животные\n" + str;
                    
                    String name = beasts.get_name();
                    int health = beasts.get_health();
                    int radius = beasts.get_radius();
                    int v = beasts.get_velocity();
                    
                    str += "\nИмя: " + name;
                    str += "\nЗдоровье: " + health;
                    str += "\nРадиус: " + radius;
                    str += "\nСкорость: " + v;
                }
                
                if (Global.list.get(i) instanceof Weapon)
                {
                    Weapon weapon = (Weapon)Global.list.get(i);
                    
                    str = "Класс: Оружие\n" + str;
                    
                    String name = weapon.get_nameWeapon();
                    int rate = weapon.get_rate();
                    int killability = weapon.get_killability();
                    
                    str += "\nНаименование: " + name;
                    str += "\nСкорострельность: " + rate;
                    str += "\nПоражающая способность: " + killability;
                }
                
                if (Global.list.get(i) instanceof Patrons)
                {
                    Patrons patrons = (Patrons)Global.list.get(i);
                    
                    str = "Класс: Патроны\n" + str;
                    
                    int kol = patrons.get_kol();
                    String typeWeapons = patrons.get_nameTypeWeapons();
                    
                    str += "\nКоличество: " + kol;
                    str += "\nТип оружия: " + typeWeapons;

                }
                
                if (Global.list.get(i) instanceof MedicineChest)
                {
                    MedicineChest medicineChest = (MedicineChest)Global.list.get(i);
                    
                    str = "Класс: Аптечка\n" + str;
                    
                    int healthLevel = medicineChest.get_healthLevel();
                    
                    str += "\nУровень здоровья: " + healthLevel;
                }
                
                if (Global.list.get(i) instanceof Key)
                {
                    Key key = (Key)Global.list.get(i);
                    
                    str = "Класс: Ключ\n" + str;
                    
                    int typeDoor = key.get_typeDoor();
                    
                    str += "\nТип двери: " + typeDoor;
                }

                
                jTextPane2.setText(str);
                
                break;
            }
          
    }//GEN-LAST:event_jPanel1MouseClicked

    /**
     * @param args the command line arguments
     */
    public static void main(String args[]) {
        /* Set the Nimbus look and feel */
        //<editor-fold defaultstate="collapsed" desc=" Look and feel setting code (optional) ">
        /* If Nimbus (introduced in Java SE 6) is not available, stay with the default look and feel.
         * For details see http://download.oracle.com/javase/tutorial/uiswing/lookandfeel/plaf.html 
         */
        try {
            for (javax.swing.UIManager.LookAndFeelInfo info : javax.swing.UIManager.getInstalledLookAndFeels()) {
                if ("Nimbus".equals(info.getName())) {
                    javax.swing.UIManager.setLookAndFeel(info.getClassName());
                    break;
                }
            }
        } catch (ClassNotFoundException ex) {
            java.util.logging.Logger.getLogger(Redactor.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (InstantiationException ex) {
            java.util.logging.Logger.getLogger(Redactor.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (IllegalAccessException ex) {
            java.util.logging.Logger.getLogger(Redactor.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (javax.swing.UnsupportedLookAndFeelException ex) {
            java.util.logging.Logger.getLogger(Redactor.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        }
        //</editor-fold>

        /* Create and display the form */
        java.awt.EventQueue.invokeLater(new Runnable() {
            public void run() {
                new Redactor().setVisible(true);
                
            }
        });
    }
    // Variables declaration - do not modify//GEN-BEGIN:variables
    private javax.swing.JButton jButton1;
    private javax.swing.JLabel jLabel1;
    private javax.swing.JPanel jPanel1;
    private javax.swing.JScrollPane jScrollPane1;
    private javax.swing.JScrollPane jScrollPane2;
    private javax.swing.JTextPane jTextPane1;
    private javax.swing.JTextPane jTextPane2;
    // End of variables declaration//GEN-END:variables
}
