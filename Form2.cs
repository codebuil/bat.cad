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
    public partial class Form2 : Form
    {
        public string MyString { get; set; }
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MyString = this.textBox1.Text.ToString();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MyString = "";
            this.Hide();
        }
    }
}
