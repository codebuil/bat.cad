using System.Windows.Forms;

namespace CCad
{
    public partial class Form1 : Form
    {
        private Point _startPoint;
        private Point _endPoint;
        private bool _drawing=false;
        private bool _draw = false;

        public Form1()
        {
            InitializeComponent();
        }

       
    

 

  
        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = new Bitmap(800, 800);
            using (Graphics g = Graphics.FromImage(pictureBox1.Image))
            {
               
                g.Clear(Color.Blue);
            }
            pictureBox1.Invalidate(); // Redesenha a imagem
        }

 
        private void pictureBox1_MouseDown_1(object sender, MouseEventArgs e)
        {
            _startPoint = e.Location;
            _drawing = true;
            
        }

        private void pictureBox1_MouseUp_1(object sender, MouseEventArgs e)
        {
            _endPoint = e.Location;
            _drawing = false;
            _draw = true;
            using (Graphics g = Graphics.FromImage(pictureBox1.Image))
            {
                Pen pen = new Pen(Color.White);
                g.DrawLine(pen, _startPoint, _endPoint);
            }
            pictureBox1.Invalidate(); // Redesenha a imagem
        }

        private void pictureBox1_Paint_1(object sender, PaintEventArgs e)
        {
           
                
            
        }

       
        private void saveToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            pictureBox1.Image.Save("my.bmp");
        }
    }
}