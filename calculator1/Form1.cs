using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {

        public int sum=0;
        public int var = 0;
        public String fnc = "+";

        public Form1()
        {
            InitializeComponent();
            textBox1.Text = sum.ToString();
            textBox2.Text = fnc;
            textBox3.Text = var.ToString();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            var = 1;
            textBox3.Text = "1";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var = 2;
            textBox3.Text = "2";
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            calc();
        }

        public void calc() { 
            if (fnc == "+")
                sum = sum + var;

            if (fnc == "-")
                sum = sum - var;

            textBox1.Text = sum.ToString();
        }

        private void pls_Click(object sender, EventArgs e)
        {
            fnc = "+";
            textBox2.Text = "+";
        }

        private void min_Click(object sender, EventArgs e)
        {
            fnc = "-";
            textBox2.Text = "-";
        }
    }
}
