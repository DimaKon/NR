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

namespace ImageProcessing
{
    public partial class BinarisationForm : Form
    {

        private string pathIn;
        private string pathOut;
        private double lev;

        public BinarisationForm()
        {
            InitializeComponent();
        }


        private void InitialiseParams()
        {
            label4.BackColor = Color.Gray;
            label4.Text = "";
            pathIn = Convert.ToString(textBox1.Text);
            pathOut = Convert.ToString(textBox2.Text) + "\\" + (String.IsNullOrEmpty(textBox4.Text) ? Path.GetFileName(pathIn) : textBox4.Text + Path.GetExtension(pathIn));
            lev = Convert.ToDouble(textBox3.Text);
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

            Binarisator binarisator = new Binarisator(bits, lev);
            Bitmap outBits;

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            if (radioButton1.Checked) 
                outBits = binarisator.Binarisation();
            else if (radioButton2.Checked)
                outBits = binarisator.DoubleBinarisation();
            else if (radioButton3.Checked)
                outBits = binarisator.Sobel();
            else 
                outBits = binarisator.Laplas();

            stopWatch.Stop();

            string errorMessage = loader.Save(outBits, pathOut);
            if (! String.IsNullOrEmpty(errorMessage))
            {
                LoadingFailed(errorMessage);
                return;               
            }

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
