using System.Diagnostics.Metrics;
using System.Windows.Forms;

namespace CCad
{
    public partial class Form1 : Form
    {

        private Point _startPoint;
        private Point _endPoint;
        private bool _drawing = false;
        private bool _draw = false;
        Form2 form2 = new Form2();
        Form3 form3 = new Form3();
        Form4 form4 = new Form4();
        int[] xxx = new int[1000];
        int[] yyy = new int[1000];
        int[] xxx2 = new int[1000];
        int[] yyy2 = new int[1000];
        Boolean selects = false;
        Boolean dselects = false;
        int last = 999;
        int count = 0;

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
            xxx[last] = 0;
            xxx2[last] = 0;
            yyy[last] = 0;
            yyy2[last] = 0;
        }


        private void pictureBox1_MouseDown_1(object sender, MouseEventArgs e)
        {
            if (selects)
            {
                
            }
            else
            {
                _startPoint = e.Location;
                _drawing = true;
            }   
        }

        private void pictureBox1_MouseUp_1(object sender, MouseEventArgs e)
        {
            if (selects)
            {
                int n = 0;
                int ssss = -1;
                int xxxxx=0;
                int yyyyy = 0;
                int xxxxx2 = 0;
                int yyyyy2 = 0;
                for (n = count - 1; n > -1; n--)
                {
                    if (xxx[n]<xxx2[n] && yyy[n] < yyy2[n])
                    {
                        if (e.Location.X >= xxx[n] && e.Location.Y >= yyy[n] && e.Location.X <= xxx2[n] && e.Location.Y >= yyy2[n]) ssss = n;
                    }
                    if (xxx[n] > xxx2[n] && yyy[n] > yyy2[n])
                    {
                        if (e.Location.X >= xxx2[n] && e.Location.Y >= yyy2[n] && e.Location.X <= xxx[n] && e.Location.Y >= yyy[n]) ssss = n;
                    }
                    if (xxx[n] > xxx2[n] && yyy[n] < yyy2[n])
                    {
                        if (e.Location.X >= xxx2[n] && e.Location.Y >= yyy[n] && e.Location.X <= xxx[n] && e.Location.Y >= yyy2[n]) ssss = n;
                    }
                    if (xxx[n] < xxx2[n] && yyy[n] > yyy2[n])
                    {
                        if (e.Location.X >= xxx[n] && e.Location.Y >= yyy2[n] && e.Location.X <= xxx2[n] && e.Location.Y >= yyy[n]) ssss = n;
                    }

                }
                if (ssss > -1)
                {
                    
                        selects = false;
                    dselects = true;
                    using (Graphics g = Graphics.FromImage(pictureBox1.Image))
                    {
                        Pen pen = new Pen(Color.Black);
                        xxx[last] = xxx[ssss];
                        yyy[last] = yyy[ssss];
                        xxx2[last] = xxx2[ssss];
                        yyy2[last] = yyy2[ssss];
                        
                        g.DrawLine(pen, xxx[ssss], yyy[ssss], xxx2[ssss], yyy2[ssss]);
                    }
                    pictureBox1.Invalidate(); // Redesenha a imagem
                }
                
            }
            if(!selects && !dselects)
            {
                _endPoint = e.Location;
                _drawing = false;
                _draw = true;
                using (Graphics g = Graphics.FromImage(pictureBox1.Image))
                {
                    Pen pen = new Pen(Color.White);
                    xxx[count] = _startPoint.X;
                    yyy[count] = _startPoint.Y;
                    xxx2[count] = _endPoint.X;
                    yyy2[count] = _endPoint.Y;
                    count++;
                    g.DrawLine(pen, _startPoint, _endPoint);
                }
                pictureBox1.Invalidate(); // Redesenha a imagem
            }
        }

        private void pictureBox1_Paint_1(object sender, PaintEventArgs e)
        {



        }




        private void saveToolStripMenuItem_Click_1(object sender, EventArgs e)
        {


            form2.MyString = "";
            form2.ShowDialog();
            if (form2.MyString != "") pictureBox1.Image.Save(form2.MyString);
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void menuToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {

            form2.MyString = "";
            form2.ShowDialog();
            if (form2.MyString != "" && File.Exists(form2.MyString))
            {
                count = 0;
                Boolean b = false;
                string[] linhas = System.IO.File.ReadAllLines(form2.MyString); // Lê todas as linhas do arquivo

                foreach (string linha in linhas) // Percorre cada linha do arquivo
                {
                    if (linha.StartsWith("line")) // Verifica se a linha começa com a string "line,0,0,100"
                    {
                        // Separa as coordenadas da linha
                        string[] coords = linha.Split(',');
                        if (coords.Count() > 4)
                        {
                            int x1 = int.Parse(coords[1]);
                            int y1 = int.Parse(coords[2]);
                            int x2 = int.Parse(coords[3]);
                            int y2 = int.Parse(coords[4]);
                            xxx[count] = x1;
                            yyy[count] = y1;
                            xxx2[count] = x2;
                            yyy2[count] = y2;
                            count++;
                            // Desenha a linha na picture1 em branco
                            using (Graphics g = Graphics.FromImage(pictureBox1.Image))
                            {
                                if (b == false)
                                {
                                    b = true;
                                    g.Clear(Color.Blue);
                                }
                                Pen pen = new Pen(Color.White);
                                g.DrawLine(pen, x1, y1, x2, y2);
                            }
                        }
                    }
                }
                pictureBox1.Invalidate();
            }
        }

        private void backToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (count > 0)
            {
                int n = 0;
                Boolean b = false;
                count--;
                for (n = 0; n < count; n++)
                {

                    // Desenha a linha na picture1 em branco
                    using (Graphics g = Graphics.FromImage(pictureBox1.Image))
                    {
                        if (b == false)
                        {
                            b = true;
                            g.Clear(Color.Blue);
                        }
                        Pen pen = new Pen(Color.White);
                        g.DrawLine(pen, xxx[n], yyy[n], xxx2[n], yyy2[n]);

                    }


                }
                pictureBox1.Invalidate();

            }
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            form2.MyString = "";
            form2.ShowDialog();
            int n = 0;
            if (form2.MyString != "")
            {
                using (StreamWriter file = new StreamWriter(form2.MyString))
                {
                    for (n = 0; n < count; n++)
                    {




                        Pen pen = new Pen(Color.White);
                        file.WriteLine("line," + xxx[n].ToString() + "," + yyy[n].ToString() + "," + xxx2[n].ToString() + "," + yyy2[n].ToString() + "\r\n");




                    }

                    file.Close();
                }
            }
        }

        private void mergeAtXYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int xxxxx = 0;
            int yyyyy = 0;
            form3.MyString = "";
            form3.ShowDialog();
            xxxxx = form3.xxxxx;
            yyyyy = form3.yyyyy;
            if (form3.MyString != "" && File.Exists(form3.MyString))
            {

                Boolean b = false;
                string[] linhas = System.IO.File.ReadAllLines(form3.MyString); // Lê todas as linhas do arquivo

                foreach (string linha in linhas) // Percorre cada linha do arquivo
                {
                    if (linha.StartsWith("line")) // Verifica se a linha começa com a string "line,0,0,100"
                    {
                        // Separa as coordenadas da linha
                        string[] coords = linha.Split(',');
                        if (coords.Count() > 4)
                        {
                            int x1 = int.Parse(coords[1]) + xxxxx;
                            int y1 = int.Parse(coords[2]) + yyyyy;
                            int x2 = int.Parse(coords[3]) + xxxxx;
                            int y2 = int.Parse(coords[4]) + yyyyy;
                            xxx[count] = x1;
                            yyy[count] = y1;
                            xxx2[count] = x2;
                            yyy2[count] = y2;
                            count++;
                            // Desenha a linha na picture1 em branco
                            using (Graphics g = Graphics.FromImage(pictureBox1.Image))
                            {
                                if (b == false)
                                {
                                    b = true;

                                }
                                Pen pen = new Pen(Color.White);
                                g.DrawLine(pen, x1, y1, x2, y2);
                            }
                        }
                    }
                }
                pictureBox1.Invalidate();
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form2.MyString = "";
            form2.ShowDialog();
            int n = 0;
            if (form2.MyString != "")
            {
                using (StreamWriter file = new StreamWriter(form2.MyString))
                {
                    for (n = 0; n < count; n++)
                    {




                        Pen pen = new Pen(Color.White);
                        file.WriteLine("line," + xxx[n].ToString() + "," + yyy[n].ToString() + "," + xxx2[n].ToString() + "," + yyy2[n].ToString() + "\r\n");




                    }

                    file.Close();
                    using (Graphics g = Graphics.FromImage(pictureBox1.Image))
                    {


                        g.Clear(Color.Blue);
                    }
                    pictureBox1.Invalidate();
                }
            }
        }

        private void Form1_Leave(object sender, EventArgs e)
        {
            form2.MyString = "";
            form2.ShowDialog();
            int n = 0;
            if (form2.MyString != "")
            {
                using (StreamWriter file = new StreamWriter(form2.MyString))
                {
                    for (n = 0; n < count; n++)
                    {




                        Pen pen = new Pen(Color.White);
                        file.WriteLine("line," + xxx[n].ToString() + "," + yyy[n].ToString() + "," + xxx2[n].ToString() + "," + yyy2[n].ToString() + "\r\n");




                    }

                    file.Close();
                    using (Graphics g = Graphics.FromImage(pictureBox1.Image))
                    {


                        g.Clear(Color.Blue);
                    }
                    pictureBox1.Invalidate();
                }
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            form2.MyString = "";
            form2.ShowDialog();
            int n = 0;
            if (form2.MyString != "")
            {
                using (StreamWriter file = new StreamWriter(form2.MyString))
                {
                    for (n = 0; n < count; n++)
                    {




                        Pen pen = new Pen(Color.White);
                        file.WriteLine("line," + xxx[n].ToString() + "," + yyy[n].ToString() + "," + xxx2[n].ToString() + "," + yyy2[n].ToString() + "\r\n");




                    }

                    file.Close();
                    using (Graphics g = Graphics.FromImage(pictureBox1.Image))
                    {


                        g.Clear(Color.Blue);
                    }
                    pictureBox1.Invalidate();
                }
            }
        }

        private void svgToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form2.MyString = "";
            form2.ShowDialog();
            int n = 0;
            string s = "";
            if (form2.MyString != "")
            {
                using (StreamWriter file = new StreamWriter(form2.MyString))
                {
                    file.WriteLine("<svg width=\"800\" height=\"800\" viewBox=\"0 0 800 800\" xmlns=\"http://www.w3.org/2000/svg\">\r\n");
                    file.WriteLine("<rect width=\"800\" height=\"800\" fill=\"#00f\"/>\r\n");
                    for (n = 0; n < count; n++)
                    {




                        Pen pen = new Pen(Color.White);
                        s = "<line x1=\"" + xxx[n].ToString() + "\" y1=\"" + yyy[n].ToString() + "\" x2=\"" + xxx2[n].ToString() + "\" y2=\"" + yyy2[n].ToString() + "\" stroke=\"#fff\" stroke-width=\"2\"/>\r\n";
                        file.WriteLine(s);





                    }
                    file.WriteLine("</svg>\r\n");
                    file.Close();
                }
            }
        }

        private void circleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int xxxxx = 0;
            int yyyyy = 0;
            int zzzzz = 0;
            int xx = 0;
            int yy = 0;
            int xx2 = 0;
            int yy2 = 0;
            int i = 0;
            form4.MyString = "";
            form4.ShowDialog();
            xxxxx = form4.xxxxx;
            yyyyy = form4.yyyyy;
            zzzzz = form4.yyyyy;
            if (form4.MyString != "" )
            {
                double x = xxxxx;
                double y = yyyyy;
                double raio = zzzzz;
                double delta = Math.PI / 12;
                using (Graphics g = Graphics.FromImage(pictureBox1.Image))
                {
                    for (i = 0; i < 24; i++)
                    {
                        double angle = i * delta;
                        double x1 = x + raio * Math.Cos(angle);
                        double y1 = y + raio * Math.Sin(angle);
                        angle = (i + 1) * delta;
                        if (i == 23) angle = 0 * delta;
                        double x2 = x + raio * Math.Cos(angle);
                        double y2 = y + raio * Math.Sin(angle);
                        xx = (int)x1;
                        yy = (int)y1;
                        xx2 = (int)x2;
                        yy2 = (int)y2;
                        xxx[count] = xx;
                        yyy[count] = yy;
                        xxx2[count] = xx2;
                        yyy2[count] = yy2;
                        count++;

                        Pen pen = new Pen(Color.White);
                        g.DrawLine(pen, xx, yy, xx2, yy2);
                    }

                }
            }
                pictureBox1.Invalidate();

            }

        private void gridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            int i = 0;
            
            
            
            
                
                using (Graphics g = Graphics.FromImage(pictureBox1.Image))
                {
                Pen pen = new Pen(Color.Black);
                for (i = 0; i < 801; i=i+50)
                    {

                    g.DrawLine(pen, i, 0, i, 800);

                    g.DrawLine(pen, 0, i, 800, i);
                    }

                }
            
            pictureBox1.Invalidate();
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (count > 0)
            {
                int n = 0;
                Boolean b = false;
                
                for (n = 0; n < count; n++)
                {

                    // Desenha a linha na picture1 em branco
                    using (Graphics g = Graphics.FromImage(pictureBox1.Image))
                    {
                        if (b == false)
                        {
                            b = true;
                            g.Clear(Color.Blue);
                        }
                        Pen pen = new Pen(Color.White);
                        g.DrawLine(pen, xxx[n], yyy[n], xxx2[n], yyy2[n]);

                    }


                }
                pictureBox1.Invalidate();

            }
        }

        private void selectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!selects && !dselects)
            {
                dselects = false;
                selects = true;
                selectToolStripMenuItem.Checked = true;
            }
            else
            {
                if (dselects)
                {
                    using (Graphics g = Graphics.FromImage(pictureBox1.Image))
                    {

                        Pen pen = new Pen(Color.White);
                        g.DrawLine(pen, xxx[last], yyy[last], xxx2[last], yyy2[last]);
                    }
                    pictureBox1.Invalidate();
                    dselects = false;
                    selects = false;
                    selectToolStripMenuItem.Checked = false;
                }
                else { 

                    selects = false;
                    dselects = false;
                    selectToolStripMenuItem.Checked = false;
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
    }
