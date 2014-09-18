package storage;

import java.util.Vector;

public class form4 extends javax.swing.JDialog {
    public static final int START_VALUE=20; //
    public static final int COLUMN_COUNT=6; //Количество колонок в таблице
    private Vector<Tovar> storage;
    public form4(java.awt.Frame parent, boolean modal) {
        super(parent, modal);
        initComponents();        
    }

   
    @SuppressWarnings("unchecked")
    // <editor-fold defaultstate="collapsed" desc="Generated Code">//GEN-BEGIN:initComponents
    private void initComponents() {

        jLabel1 = new javax.swing.JLabel();
        jCheckBox1 = new javax.swing.JCheckBox();
        jLabel2 = new javax.swing.JLabel();
        jLabel3 = new javax.swing.JLabel();
        jCheckBox2 = new javax.swing.JCheckBox();
        jCheckBox3 = new javax.swing.JCheckBox();
        jLabel4 = new javax.swing.JLabel();
        jCheckBox4 = new javax.swing.JCheckBox();
        jLabel5 = new javax.swing.JLabel();
        jCheckBox5 = new javax.swing.JCheckBox();
        jLabel6 = new javax.swing.JLabel();
        jCheckBox6 = new javax.swing.JCheckBox();
        jLabel7 = new javax.swing.JLabel();

        setDefaultCloseOperation(javax.swing.WindowConstants.DISPOSE_ON_CLOSE);

        jLabel1.setFont(new java.awt.Font("Times New Roman", 0, 14)); // NOI18N
        jLabel1.setText("Список цветов красок, имеющихся на складе:");

        jCheckBox1.setText("");
        jCheckBox1.setEnabled(false);
        jCheckBox1.setSelected(false);

        jLabel2.setText("Красный (масляная)");

        jLabel3.setText("Зеленый (масляная)");

        jCheckBox2.setText("");
        jCheckBox2.setEnabled(false);
        jCheckBox2.setSelected(false);

        jCheckBox3.setText("");
        jCheckBox3.setEnabled(false);
        jCheckBox3.setSelected(false);

        jLabel4.setText("Синий (масляная)");

        jCheckBox4.setText("");
        jCheckBox4.setEnabled(false);
        jCheckBox4.setSelected(false);

        jLabel5.setText("Красный (нитроэмаль)");

        jCheckBox5.setText("");
        jCheckBox5.setEnabled(false);
        jCheckBox5.setSelected(false);

        jLabel6.setText("Зеленый (нитроэмаль)");

        jCheckBox6.setText("");
        jCheckBox6.setEnabled(false);
        jCheckBox6.setSelected(false);

        jLabel7.setText("Синий (нитроэмаль)");

        javax.swing.GroupLayout layout = new javax.swing.GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addGap(44, 44, 44)
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addComponent(jLabel5)
                    .addGroup(layout.createSequentialGroup()
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                            .addComponent(jLabel7)
                            .addComponent(jLabel6))
                        .addGap(56, 56, 56)
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                            .addComponent(jCheckBox5)
                            .addComponent(jCheckBox6)))
                    .addComponent(jLabel1, javax.swing.GroupLayout.PREFERRED_SIZE, 295, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addGroup(layout.createSequentialGroup()
                        .addComponent(jLabel2)
                        .addGap(69, 69, 69)
                        .addComponent(jCheckBox1))
                    .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.TRAILING)
                        .addComponent(jCheckBox4)
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.TRAILING, false)
                            .addGroup(layout.createSequentialGroup()
                                .addComponent(jLabel4)
                                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                                .addComponent(jCheckBox3))
                            .addGroup(javax.swing.GroupLayout.Alignment.LEADING, layout.createSequentialGroup()
                                .addComponent(jLabel3)
                                .addGap(69, 69, 69)
                                .addComponent(jCheckBox2)))))
                .addContainerGap(21, Short.MAX_VALUE))
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addComponent(jLabel1, javax.swing.GroupLayout.PREFERRED_SIZE, 32, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(27, 27, 27)
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(jCheckBox1)
                    .addComponent(jLabel2))
                .addGap(18, 18, 18)
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(jCheckBox2)
                    .addComponent(jLabel3))
                .addGap(18, 18, 18)
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(jCheckBox3)
                    .addComponent(jLabel4))
                .addGap(27, 27, 27)
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(jCheckBox4)
                    .addComponent(jLabel5))
                .addGap(18, 18, 18)
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(jCheckBox5)
                    .addComponent(jLabel6))
                .addGap(18, 18, 18)
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(jCheckBox6)
                    .addComponent(jLabel7))
                .addContainerGap(35, Short.MAX_VALUE))
        );

        pack();
    }// </editor-fold>//GEN-END:initComponents
    public void draw(Vector<Tovar> v1){
        int storage_size=v1.size();
        for(int i=0;i<storage_size;i++){
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
            String red,green,blue;
            red="Красный";
            green="Зеленый";
            blue="Синий";
            
            switch (flag_class){
                case 2: {
                    Kraski p2=(Kraski)v1.elementAt(i);
                    if (red.equals(p2.getcolor())==true) {
                        jCheckBox1.setSelected(true);
                    }
                    if (green.equals(p2.getcolor())==true) {
                        jCheckBox2.setSelected(true);
                    }
                    if (blue.equals(p2.getcolor())==true) {
                        jCheckBox3.setSelected(true);
                    }
                } break;
                case 3: {
                    Nitroem p3=(Nitroem)v1.elementAt(i);
                    if (red.equals(p3.getcolor())==true) {
                        jCheckBox4.setSelected(true);
                    }
                    if (green.equals(p3.getcolor())==true) {
                        jCheckBox5.setSelected(true);
                    }
                    if (blue.equals(p3.getcolor())==true) {
                        jCheckBox6.setSelected(true);
                    }
                } break;
            }//end of switch         
        }//end of cycle
    }
    public void set_vector(Vector<Tovar> v1){
        this.storage=v1;
        this.draw(storage);
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
            java.util.logging.Logger.getLogger(form4.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (InstantiationException ex) {
            java.util.logging.Logger.getLogger(form4.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (IllegalAccessException ex) {
            java.util.logging.Logger.getLogger(form4.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (javax.swing.UnsupportedLookAndFeelException ex) {
            java.util.logging.Logger.getLogger(form4.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        }
        //</editor-fold>

        /* Create and display the dialog */
        java.awt.EventQueue.invokeLater(new Runnable() {
            public void run() {
                form4 dialog = new form4(new javax.swing.JFrame(), true);
                dialog.addWindowListener(new java.awt.event.WindowAdapter() {
                    @Override
                    public void windowClosing(java.awt.event.WindowEvent e) {
                        System.exit(0);
                    }
                });
                dialog.setVisible(true);
            }
        });
    }
    // Variables declaration - do not modify//GEN-BEGIN:variables
    private javax.swing.JCheckBox jCheckBox1;
    private javax.swing.JCheckBox jCheckBox2;
    private javax.swing.JCheckBox jCheckBox3;
    private javax.swing.JCheckBox jCheckBox4;
    private javax.swing.JCheckBox jCheckBox5;
    private javax.swing.JCheckBox jCheckBox6;
    private javax.swing.JLabel jLabel1;
    private javax.swing.JLabel jLabel2;
    private javax.swing.JLabel jLabel3;
    private javax.swing.JLabel jLabel4;
    private javax.swing.JLabel jLabel5;
    private javax.swing.JLabel jLabel6;
    private javax.swing.JLabel jLabel7;
    // End of variables declaration//GEN-END:variables
}
