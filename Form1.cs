using Microsoft.VisualBasic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Windows.Forms;
using System;
using System.IO;
using static System.Runtime.InteropServices.JavaScript.JSType;


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
        bool selects = false;
        bool dselects = false;
        int sselect = -1;
        int last = 999;
        int count = 0;
        double scale = 400.00;
        double units = 10.00;

        public Form1()
        {
            InitializeComponent();
        }




        double wasm_draw3Dx(double x, double y, double z)
        {
            double zz = (z + 1.00) * 1.50;
            double zzz = scale / zz;
            double zzzz = scale / 2 - zzz / 2;
            double zzzzz = (zzz / units) * x;
            double zzzzzz = zzzz + zzzzz;
            return zzzzzz;
        }
        double wasm_draw3Dy(double x, double y, double z)
        {
            double zz = (z + 1.00) * 1.50;
            double zzz = scale / zz;
            double zzzz = scale / 2 - zzz / 2;
            double zzzzz = (zzz / units) * (units - y);
            double zzzzzz = zzzz + zzzzz - (z * 2);
            return zzzzzz;
        }
        void setScale(double sc)
        {
            scale = sc;
        }
        void setunits(double sc)
        {
            units = sc;
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
            if (dselects || selects)
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
                int xxxxx = 0;
                int yyyyy = 0;
                int xxxxx2 = 0;
                int yyyyy2 = 0;
                sselect = -1;
                for (n = count - 1; n > -1; n--)
                {
                    if (xxx[n] <= xxx2[n] && yyy[n] <= yyy2[n])
                    {
                        if (e.Location.X >= xxx[n] && e.Location.Y >= yyy[n] && e.Location.X <= xxx2[n] && e.Location.Y <= yyy2[n])
                        {
                            Console.WriteLine("xxx[n] >= xxx2[n] && yyy[n] >= yyy2[n]");
                            sselect = n;
                            n = -1;
                        }
                    }
                    else
                    {
                        if (xxx[n] >= xxx2[n] && yyy[n] >= yyy2[n])
                        {
                            if (e.Location.X >= xxx2[n] && e.Location.Y >= yyy2[n] && e.Location.X <= xxx[n] && e.Location.Y <= yyy[n])
                            {
                                Console.WriteLine("xxx[n] >= xxx2[n] && yyy[n] >= yyy2[n]");
                                sselect = n;
                                n = -1;
                            }
                        }
                        else
                        {
                            if (xxx[n] >= xxx2[n] && yyy[n] <= yyy2[n])
                            {
                                if (e.Location.X >= xxx2[n] && e.Location.Y >= yyy[n] && e.Location.X <= xxx[n] && e.Location.Y <= yyy2[n])
                                {
                                    Console.WriteLine("xxx[n] >= xxx2[n] && yyy[n] <= yyy2[n]");
                                    sselect = n;
                                    n = -1;
                                }
                            }
                            else
                            {
                                if (xxx[n] <= xxx2[n] && yyy[n] >= yyy2[n])
                                {
                                    if (e.Location.X >= xxx[n] && e.Location.Y >= yyy2[n] && e.Location.X <= xxx2[n] && e.Location.Y <= yyy[n])
                                    {
                                        // ("xxx[n] <= xxx2[n] && yyy[n] >= yyy2[n]");

                                        sselect = n;
                                        n = -1;
                                    }
                                }
                            }
                        }
                    }
                }

                if (sselect > -1)
                {

                    selects = false;
                    dselects = true;
                    using (Graphics g = Graphics.FromImage(pictureBox1.Image))
                    {
                        Pen pen = new Pen(Color.Black);
                        xxx[last] = xxx[sselect];
                        yyy[last] = yyy[sselect];
                        xxx2[last] = xxx2[sselect];
                        yyy2[last] = yyy2[sselect];

                        g.DrawLine(pen, xxx[sselect], yyy[sselect], xxx2[sselect], yyy2[sselect]);
                    }
                    pictureBox1.Invalidate(); // Redesenha a imagem
                }

            }
            if (!selects && !dselects)
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
                bool b = false;
                string[] linhas = System.IO.File.ReadAllLines(form2.MyString); // L� todas as linhas do arquivo

                foreach (string linha in linhas) // Percorre cada linha do arquivo
                {
                    if (linha.StartsWith("line")) // Verifica se a linha come�a com a string "line,0,0,100"
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
                bool b = false;
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

                bool b = false;
                string[] linhas = System.IO.File.ReadAllLines(form3.MyString); // L� todas as linhas do arquivo

                foreach (string linha in linhas) // Percorre cada linha do arquivo
                {
                    if (linha.StartsWith("line")) // Verifica se a linha come�a com a string "line,0,0,100"
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
            count = 0;
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
            if (form4.MyString != "")
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
                for (i = 0; i < 801; i = i + 50)
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
                bool b = false;

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
                    sselect = -1;
                }
                else
                {
                    sselect = -1;

                    selects = false;
                    dselects = false;
                    selectToolStripMenuItem.Checked = false;
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dselects && sselect > -1)
            {
                int n = 0;
                count--;
                for (n = sselect; n < count; n++)
                {
                    xxx[n] = xxx[n + 1];
                    yyy[n] = yyy[n + 1];
                    xxx2[n] = xxx2[n + 1];
                    yyy2[n] = yyy2[n + 1];

                }
                if (count > 0)
                {
                    n = 0;
                    bool b = false;

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
            selects = false;
            dselects = false;
            selectToolStripMenuItem.Checked = false;
        }

        private void loadbinaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form2.MyString = "";
            form2.ShowDialog();
            if (form2.MyString != "" && File.Exists(form2.MyString))
            {
                
                count = 0;
                const string Header = "batcad";
                const int RecordSize = 20; // 5 integers * 4 bytes each

                using (var fileStream = new FileStream(form2.MyString, FileMode.Open))
                using (var binaryReader = new BinaryReader(fileStream))
                {
                    // Read header
                    
                    var headerBytes = binaryReader.ReadBytes(Header.Length);
                    var header = System.Text.Encoding.ASCII.GetString(headerBytes);
                    if (header != Header)
                    {
                        
                        return;
                    }

                    // Read file size
                    Int32 fileSize = binaryReader.ReadInt32();
                    var recordCount =  fileSize;
                    
                    // Read records
                    for (int i = 0; i < recordCount; i++)
                    {
                        
                        int firstInt =(int) binaryReader.ReadInt32();
                        if (firstInt == 1)
                        {
                            
                            var ints = new int[5];
                            for (int j = 0; j < 4; j++)
                            {
                                ints[j] =(int) binaryReader.ReadInt32();
                            }
                            
                            
                                
                                    xxx[count] = ints[0];
                                    yyy[count] = ints[1];
                                    xxx2[count] = ints[2];
                                    yyy2[count] = ints[3];
                                    count++;
                                
                            
                        }
                    }
                    binaryReader.Close();
                }
                if (count > 0)
                {

                    int n = 0;
                    bool b = false;

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

        }

        private void saveToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            form2.MyString = "";
            form2.ShowDialog();
            if(form2.MyString != "")
            {
                const string Header = "batcad";

                using (var fileStream = new FileStream(form2.MyString, FileMode.Create))
                using (var binaryWriter = new BinaryWriter(fileStream))
                {
                    // Write header
                    var headerBytes = System.Text.Encoding.ASCII.GetBytes(Header);
                    binaryWriter.Write(headerBytes);

                    // Write file size
                    var fileSize = (int)count;
                    binaryWriter.Write(fileSize);
                    int v = 1;
                    // Write data
                    for(int i=0;i<count;i++)
                    {
                        binaryWriter.Write(v);
                        binaryWriter.Write(xxx[i]);
                        binaryWriter.Write(yyy[i]);
                        binaryWriter.Write(xxx2[i]);
                        binaryWriter.Write(yyy2[i]);
                    }
                }
            }
        }

        private void load3dbatToolStripMenuItem_Click(object sender, EventArgs e)
        {

            form2.MyString = "";
            form2.ShowDialog();
            if (form2.MyString != "" && File.Exists(form2.MyString))
            {
                count = 0;
                bool b = false;
                string[] linhas = System.IO.File.ReadAllLines(form2.MyString); // L� todas as linhas do arquivo

                foreach (string linha in linhas) // Percorre cada linha do arquivo
                {
                    if (linha.StartsWith("line")) // Verifica se a linha come�a com a string "line,0,0,100"
                    {
                        // Separa as coordenadas da linha
                        string[] coords = linha.Split(',');
                        if (coords.Count() > 6)
                        {
                            int x1 =(int)wasm_draw3Dx((double)int.Parse(coords[1].ToString()), (double)int.Parse(coords[2].ToString()), (double)int.Parse(coords[3].ToString()));
                            int y1 = (int)wasm_draw3Dy((double)int.Parse(coords[1].ToString()), (double)int.Parse(coords[2].ToString()), (double)int.Parse(coords[3].ToString()));
                            int x2 = (int)wasm_draw3Dx((double)int.Parse(coords[4].ToString()), (double)int.Parse(coords[5].ToString()), (double)int.Parse(coords[6].ToString()));
                            int y2 = (int)wasm_draw3Dy((double)int.Parse(coords[4].ToString()), (double)int.Parse(coords[5].ToString()), (double)int.Parse(coords[6].ToString()));
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

        private void loadbmptocopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form2.MyString = "";
            form2.ShowDialog();
            int n = 0;
            if (form2.MyString != "" && File.Exists(form2.MyString))
            {
                pictureBox1.Load(form2.MyString);

            }
            if (count > 0)
            {
                n = 0;
                bool b = false;

                for (n = 0; n < count; n++)
                {

                    // Desenha a linha na picture1 em branco
                    using (Graphics g = Graphics.FromImage(pictureBox1.Image))
                    {
                        if (b == false)
                        {
                            b = true;
                            
                        }
                        Pen pen = new Pen(Color.White);
                        g.DrawLine(pen, xxx[n], yyy[n], xxx2[n], yyy2[n]);

                    }


                }
                pictureBox1.Invalidate();

            }
        }

        private void loToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form2.MyString = "";
            form2.ShowDialog();
            if (form2.MyString != "" && File.Exists(form2.MyString))
            {

                count = 0;
                const string Header = "bat3d";
                const int RecordSize = 20; // 5 integers * 4 bytes each

                using (var fileStream = new FileStream(form2.MyString, FileMode.Open))
                using (var binaryReader = new BinaryReader(fileStream))
                {
                    // Read header

                    var headerBytes = binaryReader.ReadBytes(Header.Length);
                    var header = System.Text.Encoding.ASCII.GetString(headerBytes);
                    if (header != Header)
                    {

                        return;
                    }

                    // Read file size
                    Int32 fileSize = binaryReader.ReadInt32();
                    var recordCount = fileSize;

                    // Read records
                    for (int i = 0; i < recordCount; i++)
                    {
                       
                        int firstInt = (int)binaryReader.ReadInt32();
                        if (firstInt == 1)
                        {
                           
                            var ints = new int[8];
                            for (int j = 0; j < 6; j++)
                            {
                                ints[j] = (int)binaryReader.ReadInt32();
                            }

                            int x1 = (int)wasm_draw3Dx((double)ints[0], (double)ints[1], (double)ints[2]);
                            int y1 = (int)wasm_draw3Dy((double)ints[0], (double)ints[1], (double)ints[2]);
                            int x2 = (int)wasm_draw3Dx((double)ints[3], (double)ints[4], (double)ints[5]);
                            int y2 = (int)wasm_draw3Dy((double)ints[3], (double)ints[4], (double)ints[5]);
                            xxx[count] = x1;
                            yyy[count] = y1;
                            xxx2[count] = x2;
                            yyy2[count] = y2;
                            count++;

                           
                            


                        }
                    }
                    binaryReader.Close();
                }
                if (count > 0)
                {
                    
                    int n = 0;
                    bool b = false;

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
        }
    }
}
    
