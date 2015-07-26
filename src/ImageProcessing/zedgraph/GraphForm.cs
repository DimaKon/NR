using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZedGraph;

namespace zedgraph
{
    public partial class GraphForm : Form
    {
        GraphPane pane;
        private int M = 1, H = 6;
        private int index = 1;
        private int minIndex = 1;
        private int maxIndex;
        private Color[] color = new[] {Color.DarkOrchid, Color.ForestGreen, Color.DarkOrange, Color.DeepPink, Color.Gold, Color.LightBlue, Color.Chartreuse, Color.Tomato};
        public GraphForm()
        {
            InitializeComponent();
            InitGraph();
            //MakeGraph();
        }

        private void InitGraph()
        {
            pane = graph.GraphPane;
            pane.XAxis.MajorGrid.IsVisible = true;
            pane.XAxis.MajorGrid.DashOn = 10;
            pane.XAxis.MajorGrid.DashOff = 5;
            pane.YAxis.MajorGrid.IsVisible = true;
            pane.YAxis.MajorGrid.DashOn = 10;
            pane.YAxis.MajorGrid.DashOff = 5;
            pane.YAxis.MinorGrid.IsVisible = true;
            pane.YAxis.MinorGrid.DashOn = 1;
            pane.YAxis.MinorGrid.DashOff = 2;
            pane.XAxis.MinorGrid.IsVisible = true;
            pane.XAxis.MinorGrid.DashOn = 1;
            pane.XAxis.MinorGrid.DashOff = 2;
            pane.XAxis.Title.Text = "Ось X";
            pane.YAxis.Title.Text = "Ось U";
            pane.Title.Text = "График функции";
        }

        private void shift(double [] list)
        {
            double first = list[0];
            for (int i = 0; i < 359; i++)
                list[i] = list[i + 1];

            list[359] = first;
        }

        private List<string> pathesEtalon;
        private string undef;
        private void MakeGraph()
        {
            pane.CurveList.Clear();
            pathesEtalon = new List<string>();
            DirectoryInfo d = new DirectoryInfo(textBox3.Text);

            FileInfo [] f = d.GetFiles();
            foreach (var ff in f)
            {
                pathesEtalon.Add(ff.FullName);
            }

            undef = pathIn;
            double delta;
            int phi = 0;

            double [] undefList = new double [360];
            ReadPoints(undefList, undef, 1);
            string mes = "";
            PointPairList undefFunction = new PointPairList();
            for (int j = 0; j < 360; j++)
                undefFunction.Add(j, undefList[j]/H);

            pane.AddCurve("", undefFunction, Color.FromArgb(0, 0, 255), SymbolType.None);
            int kk = -1;
            foreach (string path in pathesEtalon)
            {
                kk++;
                delta = Double.MaxValue;
                double [] etalonList = new double [360]; 
               
                ReadPoints(etalonList, path, M);

                for (int i = 0; i < 360; i++)
                {
                    double newDelta = 0;
                    for (int k = 0; k < 360; k++)
                        newDelta += Math.Abs(etalonList[k]-undefList[k]);

                    if (newDelta < delta)
                    {
                        phi = i;
                        delta = newDelta;
                    }

                    shift(etalonList);
                }

                PointPairList function = new PointPairList();
                for (int j = 0; j < 360; j++)
                    function.Add(j, etalonList[(j+phi)%360]);

                pane.AddCurve("", function, color[kk], SymbolType.None);

                mes += path + ": delta " + delta + "; phi: " + phi + "\n"; 
            }

            itog.Text = mes;
            graph.AxisChange();
            graph.Invalidate();
        }

        private void FShow(int s)
        {
            pane.CurveList.Clear();
            double[] undefList = new double[360];
            ReadPoints(undefList, undef, 1);
            string mes = "";
            PointPairList undefFunction = new PointPairList();
            for (int j = 0; j < 360; j++)
                undefFunction.Add(j, undefList[j]/H);

            pane.AddCurve("", undefFunction, Color.FromArgb(0, 0, 255), SymbolType.None);
            int kk = -1;
            foreach (string path in pathesEtalon)
            {
                kk++;
                double[] etalonList = new double[360];

                ReadPoints(etalonList, path, M);

                for (int i = 0; i < s; i++)
                {
                    shift(etalonList);
                }

                PointPairList function = new PointPairList();
                for (int j = 0; j < 360; j++)
                    function.Add(j, etalonList[j]);

                pane.AddCurve("", function, color[kk], SymbolType.None);

            }

            itog.Text = mes;
            graph.AxisChange();
            graph.Invalidate();
        }

        private void ReadPoints(double [] list, string path, double h)
        {

            string line; 
            System.IO.StreamReader file = new System.IO.StreamReader(path);

            int i = 0;
            while ((line = file.ReadLine()) != null)
            {
                line = line.Replace("угол:", "");
                line = line.Replace("радиус:", "");
                double[] doubles = Array.ConvertAll(line.Split(';'), new Converter<string, double>(Double.Parse));

                list[i] = doubles[1]*h;
                i++;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Bitmap bmp = pane.GetImage();
            bmp.Save(pathOut + "\\График.png");
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (index < maxIndex)
                index++;
            pane.CurveList.Clear();
            MakeGraph();
        }

        private void buttonPrev_Click(object sender, EventArgs e)
        {
            if (index > minIndex)
                index--;
            pane.CurveList.Clear();
            MakeGraph();
        }

        private void selectedBest_CheckedChanged(object sender, EventArgs e)
        {
            if (selectedBest.Checked == false)
                return;
            MakeGraph();
        }

        private void selected0_CheckedChanged(object sender, EventArgs e)
        {
            if (selected0.Checked == false)
                return;
            FShow(0);
            /*PointPairList function = new PointPairList();
            for (int j = 0; j < 360; j++)
                function.Add(j, etalonList[(j + phi) % 360]);*/
        }

        private void pathButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            textBox1.Text = openFileDialog.FileName;
        }

        private void pathFolderButton_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
                return;

            textBox2.Text = folderBrowserDialog.SelectedPath;
        }

        private string pathIn;
        private string pathOut;
        private void BuildButton_Click(object sender, EventArgs e)
        {
            //try
            {
                pathIn = Convert.ToString(textBox1.Text);
                pathOut = Convert.ToString(textBox2.Text);
                MakeGraph();
            }


            
        }

        private void selected90_CheckedChanged(object sender, EventArgs e)
        {
            if (selected90.Checked == false)
                return;
            FShow(90);
        }

        private void selected180_CheckedChanged(object sender, EventArgs e)
        {
            if (selected180.Checked == false)
                return;
            FShow(180);
        }

        private void selected270_CheckedChanged(object sender, EventArgs e)
        {
            if (selected270.Checked == false)
                return;
            FShow(270);
        }

        private void pathEtalon_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
                return;

            textBox3.Text = folderBrowserDialog.SelectedPath;

            maxIndex = (new DirectoryInfo(textBox3.Text).GetFiles().Count() - 1);
        }
    }
}
