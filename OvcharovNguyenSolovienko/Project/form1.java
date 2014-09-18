package storage;

import java.util.Random;
import java.util.Vector;
import javax.swing.table.JTableHeader;
import javax.swing.table.TableColumn;
import javax.swing.table.TableColumnModel;

public class form1 extends javax.swing.JFrame {
   //public static final int MAX_ARRAY=6; //Пример константы
    public static final int START_VALUE=20; //
    public static final int COLUMN_COUNT=6; //Количество колонок в таблице
    public int cur_day;
    model m1;
    Vector<Tovar> storage; //Объявление вектора "Склад"
    public form1() {
        initComponents();
    }
  
    @SuppressWarnings("unchecked")
    // <editor-fold defaultstate="collapsed" desc="Generated Code">//GEN-BEGIN:initComponents
    private void initComponents() {

        jMenu4 = new javax.swing.JMenu();
        jScrollPane1 = new javax.swing.JScrollPane();
        jTable1 = new javax.swing.JTable();
        jLabel1 = new javax.swing.JLabel();
        jLabel2 = new javax.swing.JLabel();
        jMenuBar1 = new javax.swing.JMenuBar();
        jMenu1 = new javax.swing.JMenu();
        jMenuItem1 = new javax.swing.JMenuItem();
        jMenuItem3 = new javax.swing.JMenuItem();
        jMenuItem2 = new javax.swing.JMenuItem();
        jMenuItem4 = new javax.swing.JMenuItem();
        jMenu2 = new javax.swing.JMenu();
        jMenuItem5 = new javax.swing.JMenuItem();
        jMenuItem6 = new javax.swing.JMenuItem();
        jMenuItem7 = new javax.swing.JMenuItem();

        jMenu4.setText("jMenu4");

        setDefaultCloseOperation(javax.swing.WindowConstants.EXIT_ON_CLOSE);

        jTable1.setModel(new javax.swing.table.DefaultTableModel(
            new Object [][] {
                {null, null, null, null},
                {null, null, null, null},
                {null, null, null, null},
                {null, null, null, null}
            },
            new String [] {
                "Title 1", "Title 2", "Title 3", "Title 4"
            }
        ));
        jTable1.setEnabled(true);
        jTable1.setVisible(false);
        jScrollPane1.setViewportView(jTable1);

        jLabel1.setText("День");
        jLabel1.setEnabled(false);

        jLabel2.setText("*");
        jLabel2.setEnabled(false);

        jMenu1.setLabel("Файл");
        jMenu1.setPreferredSize(new java.awt.Dimension(50, 19));

        jMenuItem1.setAccelerator(javax.swing.KeyStroke.getKeyStroke(java.awt.event.KeyEvent.VK_F4, 0));
        jMenuItem1.setText("Начать работу");
        jMenuItem1.setToolTipText("");
        jMenuItem1.setCursor(new java.awt.Cursor(java.awt.Cursor.DEFAULT_CURSOR));
        jMenuItem1.setDoubleBuffered(true);
        jMenuItem1.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jMenuItem1ActionPerformed(evt);
            }
        });
        jMenu1.add(jMenuItem1);

        jMenuItem3.setAccelerator(javax.swing.KeyStroke.getKeyStroke(java.awt.event.KeyEvent.VK_F12, 0));
        jMenuItem3.setText("Очистить склад");
        jMenuItem3.setEnabled(false);
        jMenuItem3.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jMenuItem3ActionPerformed(evt);
            }
        });
        jMenu1.add(jMenuItem3);

        jMenuItem2.setAccelerator(javax.swing.KeyStroke.getKeyStroke(java.awt.event.KeyEvent.VK_F5, 0));
        jMenuItem2.setText("Следующий день");
        jMenuItem2.setEnabled(false);
        jMenuItem2.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jMenuItem2ActionPerformed(evt);
            }
        });
        jMenu1.add(jMenuItem2);

        jMenuItem4.setAccelerator(javax.swing.KeyStroke.getKeyStroke(java.awt.event.KeyEvent.VK_ESCAPE, 0));
        jMenuItem4.setText("Выход");
        jMenuItem4.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jMenuItem4ActionPerformed(evt);
            }
        });
        jMenu1.add(jMenuItem4);

        jMenuBar1.add(jMenu1);

        jMenu2.setText("Просмотреть");
        jMenu2.setEnabled(false);
        jMenu2.addMouseListener(new java.awt.event.MouseAdapter() {
            public void mouseClicked(java.awt.event.MouseEvent evt) {
                jMenu2MouseClicked(evt);
            }
        });

        jMenuItem5.setText("Количество краски заданного цвета");
        jMenuItem5.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jMenuItem5ActionPerformed(evt);
            }
        });
        jMenu2.add(jMenuItem5);

        jMenuItem6.setText("Количество гвоздей нужного размера");
        jMenuItem6.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jMenuItem6ActionPerformed(evt);
            }
        });
        jMenu2.add(jMenuItem6);

        jMenuItem7.setText("Список цветов красок");
        jMenuItem7.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jMenuItem7ActionPerformed(evt);
            }
        });
        jMenu2.add(jMenuItem7);

        jMenuBar1.add(jMenu2);

        setJMenuBar(jMenuBar1);

        javax.swing.GroupLayout layout = new javax.swing.GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addComponent(jScrollPane1, javax.swing.GroupLayout.DEFAULT_SIZE, 614, Short.MAX_VALUE)
                    .addGroup(layout.createSequentialGroup()
                        .addComponent(jLabel1, javax.swing.GroupLayout.PREFERRED_SIZE, 61, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addGap(27, 27, 27)
                        .addComponent(jLabel2, javax.swing.GroupLayout.PREFERRED_SIZE, 347, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addGap(0, 0, Short.MAX_VALUE)))
                .addContainerGap())
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(jLabel1, javax.swing.GroupLayout.PREFERRED_SIZE, 23, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(jLabel2))
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(jScrollPane1, javax.swing.GroupLayout.DEFAULT_SIZE, 346, Short.MAX_VALUE)
                .addContainerGap())
        );

        pack();
    }// </editor-fold>//GEN-END:initComponents
/*
 * Методы:
 * void release_storage(int number2);+
 * void fill_storage(int number);+
 * int random();+
 * 
 */
    private void jMenuItem1ActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jMenuItem1ActionPerformed
        //Кнопка: Начать работу
        jMenuItem1.setEnabled(false); //Начать работу disable
        jMenuItem2.setEnabled(true); //След. день enable
        jMenuItem3.setEnabled(true); //Очистить склад enable
        jMenu2.setEnabled(true); //Просмотры enable
        jLabel1.setEnabled(true);
        jLabel2.setEnabled(true);
        cur_day=1;       
        jLabel2.setText("Начало работы: загружено "+String.valueOf(START_VALUE)+" единиц товара.");
        jLabel1.setText("День "+String.valueOf(cur_day));
        storage=new Vector<Tovar>();
        this.fill_storage(START_VALUE);
        jTable1.setEnabled(true);
        jTable1.setVisible(true);
        this.draw(storage);               
    }//GEN-LAST:event_jMenuItem1ActionPerformed
//Меню просмотр
    private void jMenu2MouseClicked(java.awt.event.MouseEvent evt) {//GEN-FIRST:event_jMenu2MouseClicked
        //
        /*
        form2 frm=new form2(this,true);
        frm.setLocation(300, 100);
        frm.draw(v1);
        frm.setVisible(true);
        */
    }//GEN-LAST:event_jMenu2MouseClicked
//Кнопка: Очистить склад
    private void jMenuItem3ActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jMenuItem3ActionPerformed
        //Кнопка: Очистить склад
        jMenuItem1.setEnabled(true); //Начать работу enable
        jMenuItem2.setEnabled(false); //След. день disable
        jMenuItem3.setEnabled(false); //Очистить склад disable
        jMenu2.setEnabled(false); //Просмотры disable
        jLabel1.setEnabled(false);
        jLabel2.setEnabled(false);
        jTable1.setEnabled(false);
    }//GEN-LAST:event_jMenuItem3ActionPerformed
//Кнопка: Выход
    private void jMenuItem4ActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jMenuItem4ActionPerformed
        //Кнопка: Выход
        this.dispose();
        System.exit(0);
    }//GEN-LAST:event_jMenuItem4ActionPerformed
//Кнопка: Следующий день
    private void jMenuItem2ActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jMenuItem2ActionPerformed
        //Кнопка: Следующий день
        cur_day++;
        jLabel1.setText("День "+String.valueOf(cur_day));
        int flag=random(); 
        /*0-ничего не произошло
         * 1-наполняем и не удаляем
         * 2-наполняем и не можем удалить
         * 3-наполняем и удаляем
         * 4-ненаполняем и удаляем
         * 5-ненаполняем и не можем удалить
         */
        switch (flag){
            case 0: {
                jLabel2.setText("*");
            } break;
            case 1: {
                jLabel2.setText("На склад поступил новый товар!");
            } break;
            case 2: {
                jLabel2.setText("На склад поступил новый товар; Реализовать прежний товар не удалось!");
            } break;
            case 3: {
                jLabel2.setText("На склад поступил новый товар; Реализован прежний товар!");
            } break;
            case 4: {
                jLabel2.setText("Реализован прежний товар!");
            } break;
            case 5: {
                jLabel2.setText("Реализовать прежний товар не удалось!");
            } break;
        }
        this.draw(storage);    
    }//GEN-LAST:event_jMenuItem2ActionPerformed
//Количество краски заданного цвета
    private void jMenuItem5ActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jMenuItem5ActionPerformed
        form2 frm2=new form2(this,true);
        frm2.setLocation(300, 100); //Начальное положение окна
        frm2.set_vector(storage);
        frm2.setVisible(true);
        
    }//GEN-LAST:event_jMenuItem5ActionPerformed
//Количество гвоздей нужного размера
    private void jMenuItem6ActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jMenuItem6ActionPerformed
        form3 frm3=new form3(this,true);
        frm3.setLocation(300, 100); //Начальное положение окна
        frm3.set_vector(storage);
        frm3.setVisible(true);
    }//GEN-LAST:event_jMenuItem6ActionPerformed
//Список цветов красок
    private void jMenuItem7ActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jMenuItem7ActionPerformed
        form4 frm4=new form4(this,true);
        frm4.setLocation(300, 100); //Начальное положение окна
        frm4.set_vector(storage);
        frm4.setVisible(true);
    }//GEN-LAST:event_jMenuItem7ActionPerformed
//Методы
    public void fill_storage(int number1){
        Random r1=new Random();
        for(int i=0;i<number1;i++){
            int rnd1=r1.nextInt(4);
            switch (rnd1){
                case 0: {
                    int rnd2=10+r1.nextInt(100);
                    storage.add(new Gvozdi(1,rnd2/2,cur_day,0,rnd2-10));
                } break;
                case 1: {
                    int rnd2=10+r1.nextInt(100);
                    String tmp;
                    if (rnd2%2==0) { tmp="Скипидар"; }
                    else { tmp="Ацетон"; }
                    storage.add(new Razbav(2,rnd2/2,cur_day,0,tmp));
                } break;
                case 2: {
                    int rnd2=10+r1.nextInt(100);
                    int rnd3=r1.nextInt(3);
                    String tmp = null;
                    switch (rnd3){
                        case 0: {tmp="Красный";} break;
                        case 1: {tmp="Зеленый";} break;
                        case 2: {tmp="Синий";} break;
                    }
                    storage.add(new Kraski(2,rnd2/2,cur_day,0,tmp,0));
                } break;
                case 3: {
                    int rnd2=10+r1.nextInt(100);
                    int rnd3=r1.nextInt(3);
                    String tmp = null;
                    String tmp2;
                    switch (rnd3){
                        case 0: {tmp="Красный";} break;
                        case 1: {tmp="Зеленый";} break;
                        case 2: {tmp="Синий";} break;
                    }
                    if (rnd2%2==0) { tmp2="Скипидар"; }
                    else { tmp2="Ацетон"; }
                    storage.add(new Nitroem(2,rnd2/2,cur_day,0,tmp,1,tmp2));
                    } break;
                    
            }
        }
    }
    public int check_release(int number2){
        int storage_size=storage.size();
        int temp_count=0; //Счётчик свободных элементов
        for(int i=0;i<storage_size;i++){
            if (storage.elementAt(i).getrel_day()==0) {temp_count++;}
        }
        if (temp_count>=number2){
            return number2;
        }
        else {return 0;}
    }
    public int release_storage(int number2){
        if (number2>0){
            //Начало
            for(int k=0;k<number2;k++){ 
                int storage_size=storage.size();
                int temp_count=0; //Счётчик
                //Заполняем массив на наличие свободных объектов
                for(int i=0;i<storage_size;i++){
                    if (storage.elementAt(i).getrel_day()==0) {temp_count++;}
                }               
                //Создаём массив из идексов вектора storage
                int temp_array[]=new int[temp_count];
                int j=0; //Индекс для массива temp_array
                for(int i=0;i<storage_size;i++){
                    if (storage.elementAt(i).getrel_day()==0){
                        temp_array[j]=i;
                        j++;
                    }
                }
                Random r2=new Random();
                int q1=r2.nextInt(temp_count);
                storage.elementAt(temp_array[q1]).setrel_day(cur_day);
            }
            //Конец
            return 1;
        }
        else {return -1;}
    }
    public int random(){
        int flag=0; 
        /*0-ничего не произошло
         * 1-наполняем и не удаляем
         * 2-наполняем и не можем удалить
         * 3-наполняем и удаляем
         * 4-ненаполняем и удаляем
         * 5-ненаполняем и не можем удалить
         */
        int flag2=0;
        int flag3=0;
        Random r3=new Random();
        int rnd_kolvo=5+r3.nextInt(20);
        int random=r3.nextInt(50);
        if (random%2==0) { this.fill_storage(rnd_kolvo); flag2=2;}
        if (random%3==0) {flag3=this.release_storage(check_release(rnd_kolvo));}
        if (flag2==0||flag3==0) { flag=0; }
        if (flag2==2||flag3==0) { flag=1; }
        if (flag2==2||flag3==-1) { flag=2; }
        if (flag2==2||flag3==1) { flag=3; }
        if (flag2==0||flag3==1) { flag=4; }
        if (flag2==0||flag3==-1) { flag=5; }
        return flag;
    }
    public void draw(Vector<Tovar> v1){
        int storage_size=v1.size();
        m1=new model(storage_size,COLUMN_COUNT);
        for(int i=0;i<storage_size;i++){
            String tmp="";
            if (v1.elementAt(i).getbeiz()==1) 
            { 
                tmp="кг"; 
            }
            else {tmp="л";}
            m1.set_data(i,0,v1.elementAt(i).getname());
            m1.set_data(i,1,tmp);
            m1.set_data(i,2,String.valueOf(v1.elementAt(i).getkolvo()));
            m1.set_data(i,3,String.valueOf(v1.elementAt(i).getinc_day()));
            m1.set_data(i,4,String.valueOf(v1.elementAt(i).getrel_day()));
            int flag_class=0;//Флаг на определение класса
            /*
             * 0-гвозди
             * 1-разбавитель
             * 2-краска
             * 3-нитроэмаль
             */
            if (v1.elementAt(i) instanceof Nitroem==true) {flag_class=3;}
            else if (v1.elementAt(i) instanceof Kraski==true) {flag_class=2; }
            else if (v1.elementAt(i) instanceof Razbav==true) {flag_class=1; }
            else { if (v1.elementAt(i) instanceof Gvozdi==true) {flag_class=0; }                        
            }
            switch (flag_class){
                case 0: {
                    Gvozdi p1=(Gvozdi)v1.elementAt(i);
                    m1.set_data(i,5,String.valueOf("Размер: "+p1.getrazmer()));
                    p1=null;
                } break;
                case 1: {
                    Razbav p2=(Razbav)v1.elementAt(i);
                    m1.set_data(i,5,String.valueOf(p2.getmarka()));
                    p2=null;
                } break;
                case 2: {
                    Kraski p3=(Kraski)v1.elementAt(i);
                    String type;
                    if (p3.gettype()==0) 
                    {type="Масляная"; }
                    else { type="Нитроэмаль"; }
                    m1.set_data(i,5,String.valueOf(p3.getcolor()+" "+type));
                    p3=null;
                    type=null;
                } break;
                case 3: {
                    Nitroem p4=(Nitroem)v1.elementAt(i);
                    String type;
                    if (p4.gettype()==0) {type="Масляная"; }
                    else { type="Нитроэмаль"; }
                    m1.set_data(i,5,String.valueOf(p4.getcolor()+" "+type+" ("+p4.getrazbav()+")"));
                    p4=null;
                    type=null;
                }
            }                         
        } //end of double cycle
        this.jTable1.setModel(m1);
        JTableHeader jth=this.jTable1.getTableHeader();
        TableColumnModel jtm=jth.getColumnModel();
        TableColumn tc[]=new TableColumn[COLUMN_COUNT];
        tc[0]=jtm.getColumn(0);
        tc[0].setHeaderValue("Название");
        tc[1]=jtm.getColumn(1);
        tc[1].setHeaderValue("Ед.изм.");
        tc[2]=jtm.getColumn(2);
        tc[2].setHeaderValue("Кол-во");
        tc[3]=jtm.getColumn(3);
        tc[3].setHeaderValue("День поступления");
        tc[4]=jtm.getColumn(4);
        tc[4].setHeaderValue("День выдачи");
        tc[5]=jtm.getColumn(5);
        tc[5].setHeaderValue("Дополнительно");
        tc[0].setPreferredWidth(70);
        tc[1].setPreferredWidth(40);
        tc[2].setPreferredWidth(40);
        tc[3].setPreferredWidth(60);
        tc[4].setPreferredWidth(50);
        tc[5].setPreferredWidth(180);
        this.jTable1.setTableHeader(jth);
        jth.setVisible(true);
    }
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
            java.util.logging.Logger.getLogger(form1.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (InstantiationException ex) {
            java.util.logging.Logger.getLogger(form1.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (IllegalAccessException ex) {
            java.util.logging.Logger.getLogger(form1.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (javax.swing.UnsupportedLookAndFeelException ex) {
            java.util.logging.Logger.getLogger(form1.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        }
        //</editor-fold>

        /* Create and display the form */
        java.awt.EventQueue.invokeLater(new Runnable() {
           public void run() {
                form1 frm1 = new form1();
                frm1.setLocation(300, 100); //Начальное положение окна
                frm1.setVisible(true);
            }
        });        
    }
    // Variables declaration - do not modify//GEN-BEGIN:variables
    private javax.swing.JLabel jLabel1;
    private javax.swing.JLabel jLabel2;
    private javax.swing.JMenu jMenu1;
    private javax.swing.JMenu jMenu2;
    private javax.swing.JMenu jMenu4;
    private javax.swing.JMenuBar jMenuBar1;
    private javax.swing.JMenuItem jMenuItem1;
    private javax.swing.JMenuItem jMenuItem2;
    private javax.swing.JMenuItem jMenuItem3;
    private javax.swing.JMenuItem jMenuItem4;
    private javax.swing.JMenuItem jMenuItem5;
    private javax.swing.JMenuItem jMenuItem6;
    private javax.swing.JMenuItem jMenuItem7;
    private javax.swing.JScrollPane jScrollPane1;
    private javax.swing.JTable jTable1;
    // End of variables declaration//GEN-END:variables
}
