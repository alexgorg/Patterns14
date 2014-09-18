package storage;
public class Razbav extends Tovar{
    protected String b_marka;
    Razbav(){
        b_name="Разбавитель";
        b_marka="Скипидар"; //По умолчанию
    }
    Razbav(int beiz,int kolvo,int inc_day,int rel_day,String marka){
        b_name="Разбавитель";
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
        this.b_marka=marka;
    }
    @Override
    public String getname(){
        return this.b_name;
    }
    public String getmarka() {
        return this.b_marka;
    }
    public void setmarka(String marka) {
        this.b_marka=marka;
    }
}
