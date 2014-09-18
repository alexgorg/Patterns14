package storage;
import javax.swing.table.AbstractTableModel;
public class model extends AbstractTableModel {
    private int row,column;
    private String data[][];
    model(int row,int column){
        this.row=row;
        this.column=column;
        data=new String[this.row][this.column];
    }

    @Override
    public int getRowCount() {
        return this.row;
    }

    @Override
    public int getColumnCount() {
        return this.column;
    }

    @Override
    public Object getValueAt(int rowIndex, int columnIndex) {
        return data[rowIndex][columnIndex];
    }
    
    public void set_data(int r,int c,String el){
        this.data[r][c]=el;
    }
}
