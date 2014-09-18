package storage;
public class Gvozdi extends Tovar{
    protected int b_razmer;
    Gvozdi(){
        b_name="Гвозди";
        b_razmer=40; //По умолчанию 40 и 60
    }
    Gvozdi(int beiz,int kolvo,int inc_day,int rel_day,int razmer){
        b_name="Гвозди";
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
        if (razmer<=40){
            razmer=40;
        } else { razmer=60; }
        this.b_razmer=razmer;
    }
    @Override
    public String getname(){
        return this.b_name;
    }
    public int getrazmer() {
        return this.b_razmer;
    }
    public void setrazmer(int razmer) {
        if (razmer!=40||razmer!=60){
            razmer=40;
        }
        this.b_razmer=razmer;
    }
}
