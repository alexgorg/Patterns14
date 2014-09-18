package storage;
public class Kraski extends Tovar {
    protected String b_color;
    protected int b_type; //0-масляная; 1-нитроэмаль
    Kraski() {
        b_name="Краска";
        b_color="";
        b_type=0;//по умолчанию масляная краска
    }
    Kraski(int beiz,int kolvo,int inc_day,int rel_day,String color,int type){
        b_name="Краска";
        if (beiz<1||beiz>2) {
            beiz=1;
        }
        this.b_beiz=beiz;
        if (inc_day<1) {
            inc_day=1;
        }
        this.b_inc_day=inc_day;
        if (rel_day<0) {
            rel_day=1;
        }
        this.b_rel_day=rel_day;
        if (kolvo<1) {
            kolvo=1;
        }
        this.b_kolvo=kolvo;
        this.b_color=color;
        if (type!=0||type!=1){
            type=0;
        }
        this.b_type=type;    
    }
    public String getcolor() {
        return b_color;
    }
    public void setcolor(String color) {
        this.b_color=color;
    }
    public int gettype() {
        return b_type;
    }
    public void settype(int type) {
        if (type!=0||type!=1) {
            type=0;
        }
        this.b_type=type;
    }    
}
