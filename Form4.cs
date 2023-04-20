using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CCad
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        public string MyString { get; set; }
        public int xxxxx { get; set; }
        public int yyyyy { get; set; }
        public int zzzzz { get; set; }


        private void button1_Click(object sender, EventArgs e)
        {
            zzzzz = int.Parse(this.textBox1.Text.ToString());
            xxxxx = int.Parse(this.textBox2.Text.ToString());
            yyyyy = int.Parse(this.textBox3.Text.ToString());
            MyString = ".";
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            xxxxx = 0;
            yyyyy = 0;
            MyString = "";
            this.Hide();
        }
    }
}
