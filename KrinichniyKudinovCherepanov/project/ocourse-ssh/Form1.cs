using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ocourse_ssh
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            dbcontract.addElem(dataGridView1);
        }

        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            dbcontract.deleteElem(e.Row.Index);
        }

        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            dbcontract.updateDb(dataGridView1);
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < dataGridView1.Rows.Count - 1)
            {
                dbcontract.updateElem(e.RowIndex, dataGridView1);
                dbcontract.showElem(e.RowIndex, false, dataGridView1);
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            toolStripButton2.Enabled = false;
            toolStripButton3.Enabled = true;
            начатьИмитациюToolStripMenuItem.Enabled = false;
            закончитьИмитациюToolStripMenuItem.Enabled = true;
            видToolStripMenuItem.Enabled = false;
            toolStripSplitButton1.Enabled = false;
            dataGridView1.ReadOnly = true;
            toolStripStatusLabel1.Text = "Идёт имитация...";
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            toolStripButton2.Enabled = true;
            toolStripButton3.Enabled = false;
            начатьИмитациюToolStripMenuItem.Enabled = true;
            закончитьИмитациюToolStripMenuItem.Enabled = false;
            видToolStripMenuItem.Enabled = true;
            toolStripSplitButton1.Enabled = true;
            dataGridView1.ReadOnly = false;
            toolStripStatusLabel1.Text = "Готово";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            dbcontract.generate();
            dbcontract.showElem(dbcontract.size() - 1, false, dataGridView1);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (отклонениеToolStripMenuItem.Checked == false)
            {
                dataGridView1.Columns["difference"].Visible = true;
                отклонениеToolStripMenuItem.Checked = true;
            }
            else
            {
                dataGridView1.Columns["difference"].Visible = false;
                отклонениеToolStripMenuItem.Checked = false;
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void фильтрToolStripMenuItem1_Click(object sender, EventArgs e)
        {
             Form2 tform = new Form2();
			 tform.ShowDialog();
			 if (tform.DialogResult == DialogResult.OK)
			 {
				 string []tstr = new string[3];
				 tstr[0] = tform.getName();
				 tstr[1] = tform.getProvider();
				 tstr[2] = tform.getReceiver();
				 int []tint = new int[13];
				 tint[0] = tform.getContract();
				 tint[1] = tform.getYear();
				 tint[2] = tform.getMonth();
				 tint[3] = tform.getDay();
				 tint[4] = tform.getNumber();
				 tint[5] = tform.getRealNumber();
				 tint[6] = tform.getDifference();
				 tint[7] = tform.getMoney();
				 tint[8] = tform.getType();
				 tint[9] = tform.getFat();
				 tint[10] = tform.getPack();
				 tint[11] = tform.getGrade();
				 tint[12] = tform.getHardness();
				 bool []f = new bool[17];
				 f[0] = tform.fContract();
				 f[1] = tform.fName();
				 f[2] = tform.fProvider();
				 f[3] = tform.fReceiver();
				 f[4] = tform.fYear();
				 f[5] = tform.fMonth();
				 f[6] = tform.fDay();
				 f[7] = tform.fNumber();
				 f[8] = tform.fRealNumber();
				 f[9] = tform.fDifference();
				 f[10] = tform.fMoney();
				 f[11] = tform.fType();
				 f[12] = tform.fFat();
				 f[13] = tform.fPack();
				 f[14] = tform.fGrade();
				 f[15] = tform.fHardness();
				 f[16] = tform.fInverse();
				 редактированиеToolStripMenuItem.Checked = false;
				 редактированиеToolStripMenuItem1.Checked = false;
				 фильтрToolStripMenuItem.Checked = true;
				 фильтрToolStripMenuItem1.Checked = true;
				 dataGridView1.ReadOnly = true;
				 toolStripButton2.Enabled = false;
				 начатьИмитациюToolStripMenuItem.Enabled = false;
				 toolStripStatusLabel1.Text = "Режим фильтра";
				 dbcontract.showFilter(tint, tstr,f, dataGridView1);
			 }
        }

        private void редактированиеToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            фильтрToolStripMenuItem.Checked = false;
            фильтрToolStripMenuItem1.Checked = false;
            редактированиеToolStripMenuItem.Checked = true;
            редактированиеToolStripMenuItem1.Checked = true;
            dbcontract.updateTable(dataGridView1);
            dataGridView1.ReadOnly = false;
            toolStripButton2.Enabled = true;
            начатьИмитациюToolStripMenuItem.Enabled = true;
            toolStripStatusLabel1.Text = "Готово";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dbcontract.loadFromFile(Directory.GetCurrentDirectory() + "\\" + "db.txt");
            dbcontract.updateTable(dataGridView1);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            dbcontract.saveToFile(Directory.GetCurrentDirectory() + "\\" + "db.txt");
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            dbcontract.saveToFile(Directory.GetCurrentDirectory() + "\\" + "db.txt");
        }
    }
}
