package storage;
public class Tovar {
    protected String b_name;
    protected int b_beiz;
    protected int b_inc_day;
    protected int b_rel_day;
    protected int b_kolvo;
    
    Tovar() {
        b_name = "";
        b_beiz=1; //1-кг; 2-литры
        b_kolvo=1;
        b_inc_day=1;
        b_rel_day=0;
    }
    Tovar(int beiz,int kolvo,int inc_day,int rel_day) {
        if (beiz<1||beiz>2) {
            beiz=1;
        }
        this.b_beiz=beiz;
        if (inc_day<1) {
            inc_day=1;
        }
        this.b_inc_day=inc_day;
        if (rel_day<1) {
            rel_day=1;
        }
        this.b_rel_day=rel_day;
        if (kolvo<1) {
            kolvo=1;
        }
        this.b_kolvo=kolvo;
    }
    public String getname() {
        return b_name;
    }
    public int getbeiz() {
        return b_beiz;
    }
    public void setbeiz(int beiz) {
        if (beiz<1||beiz>2) {
            beiz=1;
        }
        this.b_beiz=beiz; 
    }
    public int getkolvo() {
        return b_kolvo;
    }
    public void setkolvo(int kolvo) {
        if (kolvo<1) {
            kolvo=1;
        }
        this.b_kolvo=kolvo;
    }
    public int getinc_day() {
        return b_inc_day;
    }
    public void setinc_day(int inc_day) {
        if (inc_day<1) {
            inc_day=1;
        }
        this.b_inc_day=inc_day;
    }
    public int getrel_day() {
        return b_rel_day;
    }
    public void setrel_day(int rel_day) {
         if (rel_day<1) {
            rel_day=1;
        }
        this.b_rel_day=rel_day;
    }
}
