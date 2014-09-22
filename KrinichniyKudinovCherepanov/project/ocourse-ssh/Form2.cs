using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ocourse_ssh
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        public int getContract()
		{
			return comboBox1.SelectedIndex+1;
		}
        public string getName()
		{
			return textBox1.Text;
		}
        public string getProvider()
		{
			return textBox2.Text;
		}
        public string getReceiver()
		{
			return textBox3.Text;
		}
        public int getYear()
		{
			return Convert.ToInt32(numericUpDown1.Value);
		}
        public int getMonth()
		{
			return Convert.ToInt32(numericUpDown2.Value);
		}
        public int getDay()
		{
			return Convert.ToInt32(numericUpDown3.Value);
		}
        public int getNumber()
		{
			return Convert.ToInt32(numericUpDown4.Value);
		}
        public int getRealNumber()
		{
			return Convert.ToInt32(numericUpDown5.Value);
		}
        public int getDifference()
		{
			return Convert.ToInt32(numericUpDown6.Value);
		}
        public int getMoney()
		{
			return Convert.ToInt32(numericUpDown7.Value);
		}
        public int getType()
		{
			return comboBox2.SelectedIndex+1;
		}
        public int getFat()
		{
			return (comboBox3.SelectedIndex+3)*10;
		}
        public int getPack()
		{
			return comboBox4.SelectedIndex+1;
		}
        public int getGrade()
		{
			int i=-1;
			switch (comboBox5.SelectedIndex)
			{
			case 0:
				i=0;
				break;
			case 1:
				i=3;
				break;
			case 2:
				i=4;
				break;
			default:
				break;
			}
			return i;
		}
        public int getHardness()
		{
			return comboBox6.SelectedIndex+1;
		}
        public bool fContract()
		{
			return checkBox1.Checked;
		}
        public bool fName()
		{
			return checkBox2.Checked;
		}
        public bool fProvider()
		{
			return checkBox3.Checked;
		}
        public bool fReceiver()
		{
			return checkBox4.Checked;
		}
        public bool fYear()
		{
			return checkBox5.Checked;
		}
        public bool fMonth()
		{
			return checkBox6.Checked;
		}
        public bool fDay()
		{
			return checkBox7.Checked;
		}
        public bool fNumber()
		{
			return checkBox8.Checked;
		}
        public bool fRealNumber()
		{
			return checkBox9.Checked;
		}
        public bool fDifference()
		{
			return checkBox10.Checked;
		}
        public bool fMoney()
		{
			return checkBox11.Checked;
		}
        public bool fType()
		{
			return checkBox12.Checked;
		}
        public bool fFat()
		{
			return checkBox13.Checked;
		}
        public bool fPack()
		{
			return checkBox14.Checked;
		}
        public bool fGrade()
		{
			return checkBox15.Checked;
		}
        public bool fHardness()
		{
			return checkBox16.Checked;
		}
        public bool fInverse()
		{
			return checkBox17.Checked;
		}

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                if (comboBox1.SelectedIndex == -1) comboBox1.SelectedIndex = 0;
                comboBox1_SelectedIndexChanged(sender, e);
            }
            else
            {
                comboBox2.Enabled = false;
                comboBox3.Enabled = false;
                comboBox4.Enabled = false;
                comboBox5.Enabled = false;
                comboBox6.Enabled = false;
                checkBox12.Enabled = false;
                checkBox13.Enabled = false;
                checkBox14.Enabled = false;
                checkBox15.Enabled = false;
                checkBox16.Enabled = false;
                checkBox12.Checked = false;
                checkBox13.Checked = false;
                checkBox14.Checked = false;
                checkBox15.Checked = false;
                checkBox16.Checked = false;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = true;
            if (comboBox1.Text == "Сыр")
            {
                comboBox2.Enabled = true;
                comboBox3.Enabled = true;
                comboBox4.Enabled = true;
                comboBox5.Enabled = false;
                comboBox6.Enabled = false;
                checkBox12.Enabled = true;
                checkBox13.Enabled = true;
                checkBox14.Enabled = true;
                checkBox15.Enabled = false;
                checkBox16.Enabled = false;
                checkBox15.Checked = false;
                checkBox16.Checked = false;
            }
            else if (comboBox1.Text == "Пшеница")
            {
                comboBox2.Enabled = false;
                comboBox3.Enabled = false;
                comboBox4.Enabled = false;
                comboBox5.Enabled = true;
                comboBox6.Enabled = true;
                checkBox12.Enabled = false;
                checkBox13.Enabled = false;
                checkBox14.Enabled = false;
                checkBox15.Enabled = true;
                checkBox16.Enabled = true;
                checkBox12.Checked = false;
                checkBox13.Checked = false;
                checkBox14.Checked = false;
            }
        }

        private void checkBox12_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox12.Checked && comboBox2.SelectedIndex == -1)
                comboBox2.SelectedIndex = 0;
        }

        private void checkBox13_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox13.Checked && comboBox3.SelectedIndex == -1)
                comboBox3.SelectedIndex = 0;
        }

        private void checkBox14_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox14.Checked && comboBox4.SelectedIndex == -1)
                comboBox4.SelectedIndex = 0;
        }

        private void checkBox15_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox15.Checked && comboBox5.SelectedIndex == -1)
                comboBox5.SelectedIndex = 0;
        }

        private void checkBox16_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox16.Checked && comboBox6.SelectedIndex == -1)
                comboBox6.SelectedIndex = 0;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            checkBox2.Checked = true;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            checkBox3.Checked = true;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            checkBox4.Checked = true;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            checkBox5.Checked = true;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            checkBox6.Checked = true;
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            checkBox7.Checked = true;
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            checkBox8.Checked = true;
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            checkBox9.Checked = true;
        }

        private void numericUpDown6_ValueChanged(object sender, EventArgs e)
        {
            checkBox10.Checked = true;
        }

        private void numericUpDown7_ValueChanged(object sender, EventArgs e)
        {
            checkBox11.Checked = true;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkBox12.Checked = true;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkBox13.Checked = true;
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkBox14.Checked = true;
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkBox15.Checked = true;
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkBox16.Checked = true;
        }
    }
}
