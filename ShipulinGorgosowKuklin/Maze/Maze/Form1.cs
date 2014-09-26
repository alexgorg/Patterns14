using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows;


namespace Maze
{

    public partial class Form1 : Form
    {
        public static int MZ=17;
        public static int[,] maze = new int[MZ + 1, MZ + 1];
        static List<NPC> players = new List<NPC>();
        static List<NPC> enemys=new List<NPC>();
        static List<subject> subj = new List<subject>();
        public static String PLAYERNAME="ChuckNorris";
        static NPC Gamer;
        public static Button[] buts=new Button[MZ*MZ];
        public static Form frame;
        public static Random r=new Random();


        void HealAndPatronsGenerarion()
        {
            for (int i = 0; i < 12; i++)//fill array a heal and patrons
            {
                int f = 0;
                while (f == 0)
                {
                    int x = (int)((int)r.Next(MZ));
                    int y = (int)((int)r.Next(MZ));
                    if (maze[x,y] == 0)
                    {
                        maze[x,y] = 3;
                        subject s = new subject();
                        s.setAmount((int)r.Next() + 25);
                        s.setPosition(x, y);
                        subj.Add(s);
                        f = 1;
                    }
                }
                f = 0;
                while (f == 0)
                {
                    int x = (int)((int)r.Next(MZ));
                    int y = (int)((int)r.Next(MZ));
                    if (maze[x,y] == 0)
                    {
                        maze[x,y] = 4;
                        subject s = new subject();
                        s.setAmount(12);
                        s.setPosition(x, y);
                        subj.Add(s);
                        f = 1;
                    }
                }
            }
        }

        void WallsGeneration()
        {
            for (int i = 0; i < MZ * 5; i++)//fill array a walls
            {
                int x = ((int)r.Next(MZ));
                int y = ((int)r.Next(MZ));
                maze[x,y] = 1;
            }
            for (int i = 0; i < MZ + 1; i++)
                maze[i,MZ] = 1;
            for (int i = 0; i < MZ + 1; i++)
                maze[MZ,i] = 1;
        }

        void MobGeneration()
        {
            for (int i = 0; i < 10; i++)
            {
                int f = 0;
                while (f == 0)
                {
                    int x = ((int)r.Next(MZ));
                    int y = ((int)r.Next(MZ));

                    if (maze[x,y] == 0)
                    {
                        maze[x,y] = 2;
                        enemys.Add(new NPC("Angry mob " + i, 40, 3, x, y, 200));
                        f = 1;
                    }
                }
            }
        }

        void PlayersGeneration()
        {
            for (int i = 0; i < 6; i++)
            {
                int f = 0;
                while (f == 0)
                {
                    int x = (int)r.Next(MZ);
                    int y = (int)r.Next(MZ);
                    if (maze[x,y] == 0)
                    {
                        maze[x,y] = 2;
                        players.Add(new NPC("Russian Ivan #" + i, 100, 10, x, y, 7));
                        enemys.Add(players[i]);
                        f = 1;
                    }
                }
            }

            int ff = 0;
            while (ff == 0)
            {
                int x = (int)r.Next(MZ);
                int y = (int)r.Next(MZ);
                if (maze[x,y] == 0)
                {
                    maze[x,y] = 2;
                    players.Add(new NPC(PLAYERNAME, 115, 8, x, y, 8));
                    Gamer = (NPC)players[6];
                    enemys.Add(Gamer);
                    ff = 1;
                }
            }
        }

        void RaiseSubject(NPC npc, subject sub)//!
        {
            if (sub.getAmount() == 12)
                npc.upAmmo(sub.getAmount());
            else
                npc.heal(sub.getAmount());
            subj.Remove(sub);
        }

        void GetSubject()
        {

            for (int i = 0; i < players.Count; i++)
            {
                for (int j = 0; j < subj.Count; j++)
                {
                    NPC p = (NPC)players[i];
                    subject s = (subject)subj[j];
                    if ((p.getX() == s.getX()) && (p.getY() == s.getY()))
                        RaiseSubject(p, s);

                }
            }
        }

        NPC SearchXY(int x, int y)
        {
            for (int i = 0; i < enemys.Count; i++)
                if (enemys[i].getX() == x && enemys[i].getY() == y) return enemys[i];
            return Gamer;
        }

        void Contact(NPC npc)
        {
            int x = npc.getX(), y = npc.getY(), f = 0;

            for (int i = 0; i < npc.Radius() || f == 0; i++)
            {
                if (x - i == 0) break;
                if (maze[x - i,y] == 1) break;
                if (maze[x - i,y] == 3 || maze[x - i,y] == 4) { Step(npc, 0); f = 1; }
                if (maze[x - i,y] == 2)
                {
                    if (npc.getHp() > 10)
                        Combat(npc);
                    else Step(npc, 2);
                    f = 1;
                }
            }
            for (int i = 0; i < npc.Radius() || f == 0; i++)
            {
                if (x + i == MZ - 1) break;
                if (maze[x + i,y] == 1) break;
                if (maze[x + i,y] == 3 || maze[x + i,y] == 4) { Step(npc, 2); f = 1; }
                if (maze[x + i,y] == 2)
                {
                    if (npc.getHp() > 10)
                        Combat(npc);
                    else Step(npc, 0);
                    f = 1;
                }
            }
            for (int i = 0; i < npc.Radius() || f == 0; i++)
            {
                if (y - i == 0) break;
                if (maze[x,y - i] == 1) break;
                if (maze[x,y - i] == 3 || maze[x,y - i] == 4) { Step(npc, 1); f = 1; }
                if (maze[x,y - i] == 2)
                {
                    if (npc.getHp() > 10)
                        Combat(npc);
                    else Step(npc, 3);
                    f = 1;
                }
            }
            for (int i = 0; i < npc.Radius() || f == 0; i++)
            {
                if (y + i == MZ - 1) break;
                if (maze[x,y + i] == 1) break;
                if (maze[x,y + i] == 3 || maze[x,y + i] == 4) { Step(npc, 3); f = 1; }
                if (maze[x,y + i] == 2)
                {
                    if (npc.getHp() > 10)
                        Combat(npc);
                    else Step(npc, 1);
                    f = 1;
                }
            }
            if (f == 0) Step(npc, -1);

        }

        void Combat(NPC npc1)
        {
        int f=0;
        NPC npc2=new NPC("11", 200, 1, 1, 1, 1);
        if(npc1.getX()!=0)
            if(maze[npc1.getX()-1,npc1.getY()]==2) npc2=SearchXY(npc1.getX()-1,npc1.getY());
        if(npc1.getX()!=MZ-1)
            if(maze[npc1.getX()+1,npc1.getY()]==2) npc2=SearchXY(npc1.getX()+1,npc1.getY());
        if(npc1.getY()!=0)
            if(maze[npc1.getX(),npc1.getY()-1]==2) npc2=SearchXY(npc1.getX(),npc1.getY()-1);
        if(npc1.getY()!=MZ-1)
            if(maze[npc1.getX(),npc1.getY()+1]==2) npc2=SearchXY(npc1.getX(),npc1.getY()+1);
        
        if(npc1.Status()==0 && npc2.Status()==0 && npc2.getHp()!=200)
        {
           
           {
               npc1.wound(npc2.shoot());
               npc2.wound(npc1.shoot());
           }
        }
        if (npc1.getHp()==0) {enemys.Remove(npc1);maze[npc1.getX(),npc1.getY()]=0;}
        if (npc2.getHp() == 0) { enemys.Remove(npc2); maze[npc2.getX(), npc2.getY()] = 0; }
        Paint();
    }

        void Step(NPC npc, int st)
        {
            int side;
            if (st == -1) side = r.Next(3);
            else side = st;
            int oldx = npc.getX();
            int oldy = npc.getY();
            int x = 0, y = 0;
            if (!((oldx == 0 && side == 0) || (oldx == MZ && side == 2) || (oldy == 0 && side == 1) || (oldy == MZ && side == 3)))
            {
                switch (side)
                {
                    case 0: { x = oldx - 1; y = oldy; } break;
                    case 3: { x = oldx; y = oldy + 1; } break;
                    case 2: { x = oldx + 1; y = oldy; } break;
                    case 1: { x = oldx; y = oldy - 1; } break;
                }
                if (maze[x,y] == 0)
                { npc.setPosition(x, y); maze[oldx,oldy] = 0; maze[x,y] = 2; }
                else
                    if (maze[x,y] == 3 || maze[x,y] == 4)
                    {
                        for (int i = 0; i < subj.Count; i++)
                            if (subj[i].getX() == x && subj[i].getY() == y)
                            {
                                if (subj[i].getAmount() == 12) npc.upAmmo(subj[i].getAmount());
                                else npc.heal(subj[i].getAmount());
                                break;
                            }
                        npc.setPosition(x, y);
                        maze[oldx,oldy] = 0;
                        maze[x,y] = 2;
                    }
                Combat(npc);
            }

        }

        void NewGame()
        {
            for (int i = 0; i < MZ; i++)
                for (int j = 0; j < MZ; j++)
                    maze[i,j] = 0;
            players.Clear();
            enemys.Clear();
            subj.Clear();
            WallsGeneration();
            HealAndPatronsGenerarion();
            MobGeneration();
            PlayersGeneration();
            Paint();
        }

        void Data()
        {
            label4.Text = Gamer.getName();
            label5.Text = Gamer.getHp()+"";
            label6.Text = Gamer.getAmmo() + "";

            label7.Text = players[0].getName();
            label8.Text = players[0].getHp() + "";
            label9.Text = players[0].getAmmo() + "";

            label10.Text = players[1].getName();
            label11.Text = players[1].getHp() + "";
            label12.Text = players[1].getAmmo() + "";

            label13.Text = players[2].getName();
            label14.Text = players[2].getHp() + "";
            label15.Text = players[2].getAmmo() + "";

            label16.Text = players[3].getName();
            label17.Text = players[3].getHp() + "";
            label18.Text = players[3].getAmmo() + "";

            label19.Text = players[4].getName();
            label20.Text = players[4].getHp() + "";
            label21.Text = players[4].getAmmo() + "";

            label22.Text = players[5].getName();
            label23.Text = players[5].getHp() + "";
            label24.Text = players[5].getAmmo() + "";
            
        }

        void Paint()
        {
            for (int i = 0; i < MZ; i++)
                for (int j = 0; j < MZ; j++)
                {
                    if (maze[i, j] == 0) buts[i * MZ + j].BackColor = Color.White;
                    else
                        if (maze[i, j] == 2)
                        {
                            if (SearchXY(i, j).getDamage() == 3) buts[i * MZ + j].BackColor = Color.LightSeaGreen;
                            else buts[i * MZ + j].BackColor = Color.Red;
                        }
                        else
                            if (maze[i, j] == 1) buts[i * MZ + j].BackColor = Color.Black;
                            else
                                if (maze[i, j] == 3) buts[i * MZ + j].BackColor = Color.Green;
                                else
                                    if (maze[i, j] == 4) buts[i * MZ + j].BackColor = Color.Blue;
                    if (Gamer.getX() == i && Gamer.getY() == j) buts[i * MZ + j].BackColor = Color.Yellow;
                    buts[i * MZ + j].Text = ""; ;

                }
            Data();
            for (int i = 0; i < enemys.Count; i++)
            {
                buts[enemys[i].getX() * MZ + enemys[i].getY()].Text = enemys[i].getHp() + "";
            }
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].getHp() != 0)
                 buts[players[i].getX() * MZ + players[i].getY()].Text = players[i].getHp() + "";
            }
        }


         public Form1()
        {
            InitializeComponent();
            WallsGeneration();
            HealAndPatronsGenerarion();
            MobGeneration();
            PlayersGeneration();
            ButsGen();
  
        }

        void Form1_KeyDown(object sender, KeyEventArgs e)
        {
        }
        void ButsGen()
        {
            for (int i = 0; i < MZ; i++)
                for (int j = 0; j < MZ; j++)
                {
                    System.Windows.Forms.Button b = new System.Windows.Forms.Button();
                    // b.Focus = false;
                    b.ClientSize = new System.Drawing.Size(49, 49);
                    b.Location = new System.Drawing.Point(i * 40, j * 40+30);
                    //b.setFont(f);
                    b.KeyDown += new System.Windows.Forms.KeyEventHandler(this.button2_KeyDown);
                    this.Controls.Add(b);
                    buts[i * MZ + j] = b;


                }
            Paint();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < players.Count - 1; i++)
                if(players[i].getHp()!=0)
                    Step(players[i], -1);
            if (players[0].getHp() == 0 && players[1].getHp() == 0 && players[2].getHp() == 0 && players[3].getHp() == 0 && players[4].getHp() == 0 && players[5].getHp() == 0)
                label25.Text="YOU WIN!";
            Paint();
        }

        private void button1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void button2_KeyDown(object sender, KeyEventArgs e)
        {
            if (Gamer.Status() == 0)
            {
                int pX = Gamer.getX();
                int pY = Gamer.getY();
                maze[pX, pY] = 0;
                if (e.KeyData == Keys.A)
                    if (maze[pX - 1, pY] == 0 || maze[pX - 1, pY] == 3 || maze[pX - 1, pY] == 4)
                    { Step(Gamer, 0); pX--; }
                if (e.KeyData == Keys.W)
                    if (maze[pX, pY - 1] == 0 || maze[pX, pY - 1] == 3 || maze[pX, pY - 1] == 4)
                    { Step(Gamer, 1); pY--; }
                if (e.KeyData == Keys.D)
                    if (maze[pX + 1, pY] == 0 || maze[pX + 1, pY] == 3 || maze[pX + 1, pY] == 4)
                    { Step(Gamer, 2); pX++; }
                if (e.KeyData == Keys.S)
                    if (maze[pX, pY + 1] == 0 || maze[pX, pY + 1] == 3 || maze[pX, pY + 1] == 4)
                    { Step(Gamer, 3); pY++; }
                maze[pX, pY] = 2;
                Gamer.setPosition(pX, pY);

                if (e.KeyData == Keys.Space) Combat(Gamer);
                if (e.KeyData == Keys.Enter) Gamer.puhpuh();
                Paint();
            }
            else
            {
                label25.Visible = true;
                label25.ForeColor = Color.Red;
                label25.Text = "YOU DEAD";
            }
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for(int i=0;i<MZ;i++)
                for (int j = 0; j < MZ; j++)
                {
                    buts[i * MZ + j].Text = "";
                    maze[i, j] = 0;
                   // buts[i * MZ + j].BackColor = Color.White;
                }
            
            players.Clear();
            enemys.Clear();
            subj.Clear();
            WallsGeneration();
            HealAndPatronsGenerarion();
            MobGeneration();
            PlayersGeneration();
            Paint();
            Gamer.setStatus(1);
        }

        
    }
}
