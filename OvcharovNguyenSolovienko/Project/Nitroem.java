package storage;
public class Nitroem extends Kraski {
    private String b_razbav;
    Nitroem(){
        b_name="Краска";
        b_razbav="Скипидар";
        b_type=1; //Нитроэмаль
    }
    Nitroem(int beiz,int kolvo,int inc_day,int rel_day,String color,int type,String razbav){
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
            type=1;
        }
        this.b_type=type;
        this.b_razbav=razbav;
    }
    @Override
    public String getname(){
        return this.b_name;
    }
    public String getrazbav() {
        return this.b_razbav;
    }
    public void setrazbav(String razbav) {
        this.b_razbav=razbav;
    }
}
