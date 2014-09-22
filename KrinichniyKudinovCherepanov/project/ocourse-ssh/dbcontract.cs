using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ocourse_ssh
{
    static class dbcontract
    {
        private static List<contract> db = new List<contract>();
        private static string reSpace(string str, bool direction)
        {
            string tstr = str;
            bool flag = true;
            if (direction)
            {
                if (tstr == "")
                {
                    tstr = "NULL";
                    flag = false;
                }
            }
            else if (tstr == "NULL")
            {
                tstr = "";
                flag = false;
            }
            if (flag)
            {
                if (direction)
                    tstr = tstr.Replace(' ', '_');
                else
                    tstr = tstr.Replace('_', ' ');
            }
            return tstr;
        }
        private static bool checkStr(string tstr)
        {
            if (tstr == "")
                return false;
            for (int i=0; i<tstr.Length; i++)
		        if (tstr[i]<'0' || tstr[i]>'9')
			        return false;
	        return true;
        }
        private static string readBeforeSpace(StreamReader fin)
        {
            char c;
            do
            {
                c = (char)fin.Read();
            }
            while ((c == ' ' || c == '\n' || c == '\r') && fin.EndOfStream == false);
            string buffer = "";
            while (c != ' ' && c != '\n' && c != '\r' && fin.EndOfStream == false)
            {
                buffer += c;
                c = (char)fin.Read();
            }
            return buffer;
        }
        public static bool loadFromFile(string filename)
        {
            string type = "";
            string cmd, t;
            int code = 0, ti;
            contract tcontract = new cheese();
            StreamReader fin;
            try
            {
                fin = new StreamReader(filename);
            }
            catch
            {
                return false;
            }
            {
                t = readBeforeSpace(fin);
                while (fin.EndOfStream == false)
                {
                    cmd = readBeforeSpace(fin);
                    if (fin.EndOfStream == true || cmd[0] == '[')
                    {
                        if (type == "cheese")
                        {
                            cheese tcheese = (cheese)tcontract;
                            db.Add(tcheese);
                        }
                        else if (type == "wheat")
                        {
                            wheat twheat = (wheat)tcontract;
                            db.Add(twheat);
                        }
                        type = "";
                    }
                    else
                    {
                        t = readBeforeSpace(fin);
                        if (t == "=")
                        {
                            if (cmd == "contract") code = 1;
                            else if (cmd == "name") code = 2;
                            else if (cmd == "provider") code = 3;
                            else if (cmd == "receiver") code = 4;
                            else if (cmd == "year") code = 5;
                            else if (cmd == "month") code = 6;
                            else if (cmd == "day") code = 7;
                            else if (cmd == "number") code = 8;
                            else if (cmd == "realnumber") code = 9;
                            else if (cmd == "money") code = 10;
                            else if (cmd == "type") code = 11;
                            else if (cmd == "fat") code = 12;
                            else if (cmd == "pack") code = 13;
                            else if (cmd == "grade") code = 14;
                            else if (cmd == "hardness") code = 15;
                        }
                        else
                            t = readBeforeSpace(fin);
                        switch (code)
                        {
                            case 1:
                                type = readBeforeSpace(fin);
                                if (type == "cheese")
                                {
                                    tcontract = new cheese();
                                }
                                else if (type == "wheat")
                                {
                                    tcontract = new wheat();
                                }
                                break;
                            case 2:
                                t = readBeforeSpace(fin);
                                tcontract.setName(reSpace(t, false));
                                break;
                            case 3:
                                t = readBeforeSpace(fin);
                                tcontract.setProvider(reSpace(t, false));
                                break;
                            case 4:
                                t = readBeforeSpace(fin);
                                tcontract.setReceiver(reSpace(t, false));
                                break;
                            case 5:
                                ti = Convert.ToInt32(readBeforeSpace(fin));
                                tcontract.setYear(ti);
                                break;
                            case 6:
                                ti = Convert.ToInt32(readBeforeSpace(fin));
                                tcontract.setMonth(ti);
                                break;
                            case 7:
                                ti = Convert.ToInt32(readBeforeSpace(fin));
                                tcontract.setDay(ti);
                                break;
                            case 8:
                                ti = Convert.ToInt32(readBeforeSpace(fin));
                                tcontract.setNumber(ti);
                                break;
                            case 9:
                                ti = Convert.ToInt32(readBeforeSpace(fin));
                                tcontract.setRealNumber(ti);
                                break;
                            case 10:
                                ti = Convert.ToInt32(readBeforeSpace(fin));
                                tcontract.setMoney(ti);
                                break;
                            case 11:
                                if (type == "cheese")
                                {
                                    cheese tcheese = (cheese)tcontract;
                                    ti = Convert.ToInt32(readBeforeSpace(fin));
                                    tcheese.setType(ti);
                                }
                                break;
                            case 12:
                                if (type == "cheese")
                                {
                                    cheese tcheese = (cheese)tcontract;
                                    ti = Convert.ToInt32(readBeforeSpace(fin));
                                    tcheese.setFat(ti);
                                }
                                break;
                            case 13:
                                if (type == "cheese")
                                {
                                    cheese tcheese = (cheese)tcontract;
                                    ti = Convert.ToInt32(readBeforeSpace(fin));
                                    tcheese.setPack(ti);
                                }
                                break;
                            case 14:
                                if (type == "wheat")
                                {
                                    wheat twheat = (wheat)tcontract;
                                    ti = Convert.ToInt32(readBeforeSpace(fin));
                                    twheat.setGrade(ti);
                                }
                                break;
                            case 15:
                                if (type == "wheat")
                                {
                                    wheat twheat = (wheat)tcontract;
                                    ti = Convert.ToInt32(readBeforeSpace(fin));
                                    twheat.setHardness(ti);
                                }
                                break;
                        }
                    }
                }
                fin.Close();
                return true;
            }
        }
        public static bool saveToFile(string filename)
        {
            StreamWriter fout;
            try
            {
                fout = new StreamWriter(filename);
            }
            catch
            {
                return false;
            }
	        {
		        int i=1;
		        for (int j=0; j<db.Count; j++)
		        {
                    fout.Write("[Element" + i + "]\r\n");
			        i++;
			        if (db[j] as cheese != null)
			        {
                        cheese t1 = (cheese)db[j];
                        fout.Write("contract = cheese\r\n");
                        fout.Write("type = " + t1.getType() + "\r\n");
                        fout.Write("fat = " + t1.getFat() + "\r\n");
                        fout.Write("pack = " + t1.getPack() + "\r\n");
			        }
                    if (db[j] as wheat != null)
			        {
                        wheat t2 = (wheat)db[j];
                        fout.Write("contract = wheat\r\n");
                        fout.Write("hardness = " + t2.getHardness() + "\r\n");
                        fout.Write("grade = " + t2.getGrade() + "\r\n");
			        }
                    fout.Write("name = " + reSpace(db[j].getName(), true) + "\r\n");
                    fout.Write("provider = " + reSpace(db[j].getProvider(), true) + "\r\n");
                    fout.Write("receiver = " + reSpace(db[j].getReceiver(), true) + "\r\n");
                    fout.Write("year = " + db[j].getYear() + "\r\n");
                    fout.Write("month = " + db[j].getMonth() + "\r\n");
                    fout.Write("day = " + db[j].getDay() + "\r\n");
                    fout.Write("number = " + db[j].getNumber() + "\r\n");
                    fout.Write("realnumber = " + db[j].getRealNumber() + "\r\n");
                    fout.Write("money = " + db[j].getMoney() + "\r\n");
                    fout.Write("\r\n");
		        }
                fout.Close();
		        return true;
	        }
        }
        public static void addElem(System.Windows.Forms.DataGridView tdataGridView)
        {
            contract tcontract = new cheese();
            db.Add(tcontract);
            showElem(tdataGridView.Rows.Count - 2, false, tdataGridView);
        }
        public static void showElem(int index, bool flag, System.Windows.Forms.DataGridView tdataGridView)
        {
	         contract i = db[index];
	         string str0 = "";
	         string str1 = i.getName();
	         string str2 = i.getProvider();
	         string str3 = i.getReceiver();
	         string str4 = "";
	         str4+=i.getYear().ToString();
	         str4+="-";
	         if (i.getMonth()<10) str4+="0";
	         str4+=i.getMonth().ToString()+"-";
	         if (i.getDay()<10) str4+="0";
	         str4+=i.getDay().ToString();
	         string str5 = i.getNumber().ToString();
	         string str6 = str6=i.getRealNumber().ToString();
	         string str7 = Convert.ToString(i.getRealNumber()-i.getNumber());
	         string str8 = i.getMoney().ToString();
	         string str9 = "";
	         string str10 = "";
	         string str11 = "";
	         string str12 = "";
	         string str13 = "";
             if (i as cheese != null)
	         {
                 cheese t1 = (cheese)i;
		         str0 = "Сыр";
		         switch (t1.getType())
		         {
		         case 1:
			         str9="Алтайский";
			         break;
		         case 2:
			         str9="Радонежский";
			         break;
		         case 3:
			         str9="Голландский";
			         break;
		         }
		         switch (t1.getFat())
		         {
		         case 30:
			         str10="30%";
			         break;
		         case 40:
			         str10="40%";
			         break;
		         case 50:
			         str10="50%";
			         break;
		         }
		         switch (t1.getPack())
		         {
		         case 1:
			         str11="Плёнка";
			         break;
		         case 2:
			         str11="Парафин";
			         break;
		         }
	         }
             if (i as wheat != null)
	         {
                 wheat t2 = (wheat)i;
		         str0 = "Пшеница";
		         switch (t2.getGrade())
		         {
		         case 0:
			         str12="Нестд";
			         break;
		         case 3:
			         str12="3";
			         break;
		         case 4:
			         str12="4";
			         break;
		         }
		         switch (t2.getHardness())
		         {
		         case 1:
			         str13="Твёрдая";
			         break;
		         case 2:
			         str13="Мягкая";
			         break;
		         }
	         }
	         if (flag==true || tdataGridView.Rows.Count <= index+1)
		         tdataGridView.Rows.Add(str0,str1,str2,str3,str4,str5,str6,str7,str8,str9,str10,str11,str12,str13);
	         else
	         {
		         tdataGridView.Rows[index].SetValues(str0,str1,str2,str3,str4,str5,str6,str7,str8,str9,str10,str11,str12,str13);
	         }
        }
        public static void updateElem(int index, System.Windows.Forms.DataGridView tdataGridView)
        {
	        contract tcontract = null;
	        if (string.Compare(tdataGridView.Rows[index].Cells["contract"].Value.ToString(), "Сыр") == 0)
	        {
		        tcontract = new cheese();
		        cheese tcheese = (cheese)tcontract;
		        if (tdataGridView.Rows[index].Cells["type"].Value.ToString() == "Алтайский")
			        tcheese.setType(1);
		        else if (tdataGridView.Rows[index].Cells["type"].Value.ToString() == "Радонежский")
			        tcheese.setType(2);
		        else if (tdataGridView.Rows[index].Cells["type"].Value.ToString() == "Голландский")
			        tcheese.setType(3);
		        if (tdataGridView.Rows[index].Cells["fat"].Value.ToString() == "30%")
			        tcheese.setFat(30);
		        else if (tdataGridView.Rows[index].Cells["fat"].Value.ToString() == "40%")
			        tcheese.setFat(40);
		        else if (tdataGridView.Rows[index].Cells["fat"].Value.ToString() == "50%")
			        tcheese.setFat(50);
		        if (tdataGridView.Rows[index].Cells["pack"].Value.ToString() == "Плёнка")
			        tcheese.setPack(1);
		        else if (tdataGridView.Rows[index].Cells["pack"].Value.ToString() == "Парафин")
			        tcheese.setPack(2);
	        }
	        else if (string.Compare(tdataGridView.Rows[index].Cells["contract"].Value.ToString(), "Пшеница") == 0)
	        {
		        tcontract = new wheat();
		        wheat twheat = (wheat)tcontract;
		        if (tdataGridView.Rows[index].Cells["grade"].Value.ToString() == "Нестд")
			        twheat.setGrade(0);
		        else if (tdataGridView.Rows[index].Cells["grade"].Value.ToString() == "3")
			        twheat.setGrade(3);
                else if (tdataGridView.Rows[index].Cells["grade"].Value.ToString() == "4")
			        twheat.setGrade(4);
		        if (tdataGridView.Rows[index].Cells["hardness"].Value.ToString() == "Твёрдая")
			        twheat.setHardness(1);
                else if (tdataGridView.Rows[index].Cells["hardness"].Value.ToString() == "Мягкая")
			        twheat.setHardness(2);
	        }
	        if (tdataGridView.Rows[index].Cells["name"].Value != null)
		        tcontract.setName(tdataGridView.Rows[index].Cells["name"].Value.ToString());
	        if (tdataGridView.Rows[index].Cells["provider"].Value != null)
		        tcontract.setProvider(tdataGridView.Rows[index].Cells["provider"].Value.ToString());
	        if (tdataGridView.Rows[index].Cells["receiver"].Value != null)
		        tcontract.setReceiver(tdataGridView.Rows[index].Cells["receiver"].Value.ToString());
	        char []tmp = {'-', '.', '/', '\\'};
	        string []date = tdataGridView.Rows[index].Cells["date"].Value.ToString().Split(tmp,3);
	        if (date.Length == 3)
	        {
		        if (checkStr(date[0])) tcontract.setYear(Convert.ToInt32(date[0]));
		        if (checkStr(date[1])) tcontract.setMonth(Convert.ToInt32(date[1]));
		        if (checkStr(date[2])) tcontract.setDay(Convert.ToInt32(date[2]));
	        }
	        if (checkStr(tdataGridView.Rows[index].Cells["number"].Value.ToString()))
		        tcontract.setNumber(Convert.ToInt32(tdataGridView.Rows[index].Cells["number"].Value.ToString()));
	        if (checkStr(tdataGridView.Rows[index].Cells["realnumber"].Value.ToString()))
		        tcontract.setRealNumber(Convert.ToInt32(tdataGridView.Rows[index].Cells["realnumber"].Value.ToString()));
	        if (checkStr(tdataGridView.Rows[index].Cells["money"].Value.ToString()))
		        tcontract.setMoney(Convert.ToInt32(tdataGridView.Rows[index].Cells["money"].Value.ToString()));
	        db[index] = tcontract;
        }
        public static void deleteElem(int index)
        {
	        db.RemoveAt(index);
        }
        public static void showFilter(int []tint, string []tstr, bool []f, System.Windows.Forms.DataGridView tdataGridView)
        {
            tdataGridView.Rows.Clear();
	        int j=0;
	        for (int i=0; i<db.Count; i++)
	        {
		        bool flag = true;
		        if (f[0])
		        {
                    if (db[i] as cheese != null && tint[0] == 2) flag = false;
                    if (db[i] as wheat != null && tint[0] == 1) flag = false;
		        }
		        if (f[1] && flag)
		        {
			        if (db[i].getName() != tstr[0])
				        flag = false;
		        }
		        if (f[2] && flag)
		        {
			        if (db[i].getProvider() != tstr[1])
				        flag = false;
		        }
		        if (f[3] && flag)
		        {
			        if (db[i].getReceiver() != tstr[2])
				        flag = false;
		        }
		        if (f[4] && flag)
		        {
			        if (db[i].getYear() != tint[1])
				        flag = false;
		        }
		        if (f[5] && flag)
		        {
			        if (db[i].getMonth() != tint[2])
				        flag = false;
		        }
		        if (f[6] && flag)
		        {
			        if (db[i].getDay() != tint[3])
				        flag = false;
		        }
		        if (f[7] && flag)
		        {
			        if (db[i].getNumber() != tint[4])
				        flag = false;
		        }
		        if (f[8] && flag)
		        {
			        if (db[i].getRealNumber() != tint[5])
				        flag = false;
		        }
		        if (f[9] && flag)
		        {
			        if ((db[i].getRealNumber() - db[i].getNumber()) != tint[6])
				        flag = false;
		        }
		        if (f[10] && flag)
		        {
			        if (db[i].getMoney() != tint[7])
				        flag = false;
		        }
		        if (f[11] && flag)
		        {
			        cheese tcheese = (cheese)(db[i]);
			        if (tcheese.getType() != tint[8])
				        flag = false;
		        }
		        if (f[12] && flag)
		        {
                    cheese tcheese = (cheese)(db[i]);
			        if (tcheese.getFat() != tint[9])
				        flag = false;
		        }
		        if (f[13] && flag)
		        {
                    cheese tcheese = (cheese)(db[i]);
			        if (tcheese.getPack() != tint[10])
				        flag = false;
		        }
		        if (f[14] && flag)
		        {
			        wheat twheat = (wheat)(db[i]);
			        if (twheat.getGrade() != tint[11])
				        flag = false;
		        }
		        if (f[15] && flag)
		        {
                    wheat twheat = (wheat)(db[i]);
			        if (twheat.getHardness() != tint[12])
				        flag = false;
		        }
		        if ((flag && !f[16]) || (!flag && f[16])) showElem(j, true, tdataGridView);
		        j++;
	        }
        }
        public static void updateTable(System.Windows.Forms.DataGridView tdataGridView)
        {
            tdataGridView.Rows.Clear();
            for (int i = 0; i < db.Count; i++)
            {
                showElem(i, false, tdataGridView);
            }
        }
        public static void updateDb(System.Windows.Forms.DataGridView tdataGridView)
        {
            for (int i = 0; i < tdataGridView.Rows.Count - 1; i++)
                updateElem(i, tdataGridView);
        }
        private static string rndstr(Random rnd) //генерация случайной строки
        {
            int i;
	        string tstr="";
	        for (i=0; i<rnd.Next(7)+3; i++)
	        {
		        tstr+=(char)(65+rnd.Next(26));
	        }
	        //tstr[i]='\0';
            return tstr;
        }
        public static void generate()
        {
            Random rnd = new Random();
	        contract tcontract;
	        cheese t1;
	        wheat t2;
	        //string tstr = "";
	        int t;
	        if (rnd.Next(2)==0)
	        {
		        tcontract = new cheese();
		        t1 = (cheese)tcontract;
		        t1.setType(rnd.Next(3)+1);
		        t1.setFat(10*(rnd.Next(3)+3));
		        t1.setPack(rnd.Next(2)+1);
	        }
	        else
	        {
		        tcontract = new wheat();
		        t2 = (wheat)tcontract;
		        t = rnd.Next(3);
		        if (t==0)
                    t2.setGrade(t);
		        else
                    t2.setGrade(t+2);
		        t2.setHardness(rnd.Next(2)+1);
	        }
	        tcontract.setNumber(rnd.Next(100));
	        t=rnd.Next(2);
	        if (t==0)
                tcontract.setRealNumber(tcontract.getNumber());
	        else
                tcontract.setRealNumber((int)(tcontract.getNumber() * (0.5 + rnd.NextDouble())));
	        if (db.Count != 0)
	        {
		        tcontract.setDay(db[db.Count-1].getDay());
		        if (db[db.Count-1].getMonth()<12)
		        {
			        tcontract.setMonth(db[db.Count-1].getMonth()+1);
			        tcontract.setYear(db[db.Count-1].getYear());
		        }
		        else
		        {
			        tcontract.setMonth(1);
			        tcontract.setYear(db[db.Count-1].getYear()+1);
		        }
	        }
	        else
	        {
		        tcontract.setYear(2008);
		        tcontract.setMonth(1);
		        tcontract.setDay(12);
	        }
	        tcontract.setMoney(rnd.Next(10000));
	        tcontract.setName(rndstr(rnd));
            tcontract.setProvider(rndstr(rnd));
            tcontract.setReceiver(rndstr(rnd));
	        db.Add(tcontract);
        }
        public static int size()
        {
            return db.Count;
        }
    }
}
