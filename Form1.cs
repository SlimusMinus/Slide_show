using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace _17._04._2023._22
{
   
    public partial class Form1 : Form
    {
        List<Bitmap> images = new List<Bitmap>();
        int np = 0;
        int start = 0;

        public Form1()
        {
            InitializeComponent();
            timer1.Tick += next_click;
            label1.Text = $"Всего картинок в слайд шоу: {images.Count}";
            label2.Text = $"Текущая картинка в слайд шоу: {np}";
        }

        private void next_click(object sender, EventArgs e)
        {
            if (images.Count == 0)
                return;
            np++;
            if (np >= images.Count)
                np = 0;
            pictureBox1.Image = images[np];
        }

       

        private void bt_start_Click(object sender, EventArgs e)
        {
            if (images.Count != 0)
            {
                timer1.Enabled = true;
                timer1.Start();
            }
                         
            else
                MessageBox.Show("Ничего не выбрано");
        }

        private void bt_stop_Click(object sender, EventArgs e)
        {
            bt_stop_Click(null, null);
        }

        private void bt_open_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            FolderBrowserDialog folder = new FolderBrowserDialog();
            if (folder.ShowDialog() == DialogResult.OK)
            {
                timer1.Stop();
                if (images.Count != 0)
                {
                    foreach (var item in images)
                    {
                        item.Dispose();
                    }
                    images.Clear();
                }
                DirectoryInfo direct = new DirectoryInfo(folder.SelectedPath);
                IEnumerable<FileInfo> all_file = direct.EnumerateFiles();//перебор всех элементов в директории
                foreach (var item in all_file)
                {
                    Bitmap pt = new Bitmap(item.FullName);//добавляем картинку по имени
                    Size pt_size = pictureBox1.Size; //подогнали картинку по размерам окна
                    images.Add(new Bitmap(pt, pt_size));
                }
            }
            label1.Text = $"Всего картинок в слайд шоу: {images.Count}";
            
        }

        private void bt_prev_Click(object sender, EventArgs e)
        {
            if (images.Count == 0)
                return;
            np--;
            if (np<0)
            {
                np = images.Count - 1;
            }
            pictureBox1.Image = images[np];
            label2.Text = $"Текущая картинка в слайд шоу: {np}";
        }

        private void bt_next_Click(object sender, EventArgs e)
        {
            if (images.Count == 0)
                return;
            np++;
            if (np >= images.Count)
                np = 0;
            pictureBox1.Image = images[np];
            label2.Text = $"Текущая картинка в слайд шоу: {np+1}";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ++start;
            if (start < (images.Count+1))
                label2.Text = $"Текущая картинка в слайд шоу: {start}";
            else
                start = 0;
        }

    }
}
