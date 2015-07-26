using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CircuitFunctionMaker
{
    public partial class CuircuitFunctionForm : Form
    {
        private string pathIn;
        private string pathOut;
        private double d1, d2, p1, p2;

        public CuircuitFunctionForm()
        {
            InitializeComponent();
        }

        private void InitialiseParams()
        {
            label4.BackColor = Color.Gray;
            label4.Text = "";
            pathIn = Convert.ToString(textBox1.Text);
            pathOut = Convert.ToString(textBox2.Text);
            Double.TryParse(textBox3.Text.ToString(), out d1);
            Double.TryParse(textBox4.Text.ToString(), out d2);
            Double.TryParse(numericUpDown1.Text.ToString(), out p1);
            Double.TryParse(numericUpDown2.Text.ToString(), out p2);
        }

        private void LoadingFailed(string message)
        {
            label4.BackColor = Color.Red;
            label4.Text = message;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InitialiseParams();
            ImageLoader loader = new ImageLoader();

            Bitmap bits = loader.LoadPicture(pathIn);

            if (bits == null)
            {
                LoadingFailed("Неверный путь к исходному файлу!");
                return;
            }

            Circuit circuit = new Circuit(bits, d1, d2, p1, p2, pathOut);
            Bitmap outBits;

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            outBits = circuit.GetFunction();
            stopWatch.Stop();

            string errorMessage = loader.Save(outBits, pathOut+"\\Расширенная КФ.png");
            if (!String.IsNullOrEmpty(errorMessage))
            {
                LoadingFailed(errorMessage);
                return;
            }

            loader.Save(circuit.bitsCF, pathOut + "\\Нерасширенная КФ.png");

            label4.Text = "Программа отработала успешно! Время " + stopWatch.Elapsed;
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
    }
}
