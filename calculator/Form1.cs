using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace calculator
{
    public partial class Form1 : Form
    {
        public int sum = 0;
        public int var = 0;
        public String var2 = "";
        public String fnc = "";
        public String history = "";

        public Form1()
        {
            InitializeComponent();
            textBox1.Text = sum.ToString();
            textBox2.Text = fnc;
            textBox3.Text = var.ToString();
        }

        public void Calc()
        {
            var = Convert.ToInt32(var2);

            history = history + "\n" + sum.ToString() + " " + fnc + " " + var.ToString();

            if (fnc == "+") { sum += var; }
            else if (fnc == "-") { sum -= var; }
            else if (fnc == "*") { sum *= var; }
            else if (fnc == "/") { sum /= var; }

            history = history + " = " + sum.ToString();

            fnc = "";
            var = 0;
            var2 = "";

            richTextBox1.Text = history;

            textBox1.Text = sum.ToString();
            textBox2.Text = fnc;
            textBox3.Text = var.ToString();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            var = (var * 10) + 1;
            var2 = var2 + "1";
            textBox3.Text = var.ToString();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            var = (var * 10) + 2;
            var2 = var2 + "2";
            textBox3.Text = var.ToString();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            var = (var * 10) + 3;
            var2 = var2 + "3";
            textBox3.Text = var.ToString();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            var = (var * 10) + 4;
            var2 = var2 + "4";
            textBox3.Text = var.ToString();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            var = (var * 10) + 5;
            var2 = var2 + "5";
            textBox3.Text = var.ToString();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            var = (var * 10) + 6;
            var2 = var2 + "16";
            textBox3.Text = var.ToString();
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            var = (var * 10) + 7;
            var2 = var2 + "7";
            textBox3.Text = var.ToString();
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            var = (var * 10) + 8;
            var2 = var2 + "8";
            textBox3.Text = var.ToString();
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            var = (var * 10) + 9;
            var2 = var2 + "9";
            textBox3.Text = var.ToString();
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            var = (var * 10) + 0;
            var2 = var2 + "0";
            textBox3.Text = var.ToString();
        }

        private void Button_Addition_Click(object sender, EventArgs e)
        {
            fnc = "+";
            textBox2.Text = "+";
        }

        private void Button_Subtraction_Click(object sender, EventArgs e)
        {
            fnc = "-";
            textBox2.Text = "-";
        }

        private void Button_Multiplication_Click(object sender, EventArgs e)
        {
            fnc = "*";
            textBox2.Text = "*";
        }

        private void Button_Division_Click(object sender, EventArgs e)
        {
            fnc = "/";
            textBox2.Text = "/";
        }

        private void Button_Calc_Click(object sender, EventArgs e)
        {
            Calc();
        }

        private void Button_C_Click(object sender, EventArgs e)
        {
            var = 0;
            fnc = "";
            sum = 0;
            textBox1.Text = "";
            textBox2.Text = fnc;
            textBox3.Text = sum.ToString();
        }

        private void Button_CE_Click(object sender, EventArgs e)
        {
            var = 0;
            textBox3.Text = "";
        }
    }
}
