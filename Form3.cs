using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CCad
{
    public partial class Form3 : Form
    {
        public string MyString { get; set; }
        public int xxxxx { get; set; }
        public int yyyyy { get; set; }
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            xxxxx=int.Parse(this.textBox2.Text.ToString());
            yyyyy = int.Parse(this.textBox2.Text.ToString());
            MyString = this.textBox1.Text.ToString();
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
