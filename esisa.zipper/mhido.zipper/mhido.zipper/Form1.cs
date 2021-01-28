using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mhido.zipper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           try
           {
               OpenFileDialog op = new OpenFileDialog();
               op.ShowDialog();
               string s = op.FileName;
               textBox1.Text = s;
               FileInfo info = new FileInfo(s);
               textBox2.Text = "" + (info.Length / 1024 + (info.Length % 1024 != 0 ? 1 : 0)) + "Ko";
               string[] filtre1 = textBox1.Text.Split('\\');
               string[] filtre2 = filtre1[filtre1.Length - 1].Split('.');
               textBox4.Text = filtre2[0] + ".zip";
           }
           catch (System.Exception ex)
           {
               textBox1.Text = "";
               textBox2.Text = "";
               textBox4.Text = "";
           }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            textBox4.Text = folderBrowserDialog1.SelectedPath+"\\"+textBox4.Text;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Equals("") || textBox4.Text.Equals(""))
            {
                MessageBox.Show("veuiller choisir un fichier source et un fichier destination !!!");
            } 
            else
            {string[] s = textBox1.Text.Split('\\');

            try
            {
                string path = textBox1.Text;
                FileStream file = new FileStream(path, FileMode.Open);


                int taille = (int)file.Length;
                byte[] t = new byte[taille];
                file.Read(t, 0, taille);

                FileStream outFile = new FileStream(textBox4.Text, FileMode.Create);
                GZipStream zip = new GZipStream(outFile, CompressionMode.Compress);

                int len;
                string nom;
                string[] tstring = path.Split('\\');
                nom = tstring[tstring.Length - 1];
                len = nom.Length;
                byte[] tlen = new byte[4];
                tlen = BitConverter.GetBytes(len);
                zip.Write(tlen, 0, 4);
                byte[] tnom = new byte[len * 2];
                tnom = Encoding.Unicode.GetBytes(nom);
                zip.Write(tnom, 0, len * 2);
                zip.Write(t, 0, taille);
                zip.Close();
                outFile.Close();
                file.Close();
                MessageBox.Show("file compressed successfully !!!");
                FileInfo info = new FileInfo(textBox4.Text);
                textBox3.Text = "" + (info.Length / 1024 + (info.Length % 1024 != 0 ? 1 : 0)) + "Ko";
              
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("erreur"+ ex.Message);
            }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
           try
           {
               openFileDialog1.Filter = "zip files|*.zip;*.tar.gz;*.rar";
               openFileDialog1.ShowDialog();
               string s = openFileDialog1.FileName;
               textBox8.Text = s;
               FileInfo info = new FileInfo(s);
               textBox7.Text = "" + (info.Length / 1024 + (info.Length % 1024 != 0 ? 1 : 0)) + "Ko";
           }
           catch (System.Exception ex)
           {
               textBox8.Text = "";
               textBox7.Text = "";
           }
            
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            f.ShowDialog();
            FileStream file2 = new FileStream(textBox8.Text, FileMode.Open);
            GZipStream unzip = new GZipStream(file2, CompressionMode.Decompress);
            string path = f.SelectedPath + "\\";
            byte[] tlen = new byte[4];
            unzip.Read(tlen, 0, 4);
            int len = BitConverter.ToInt32(tlen, 0);
            byte[] tnom = new byte[len * 2];
            unzip.Read(tnom, 0, len * 2);
            string nom = Encoding.Unicode.GetString(tnom);

            path = path + nom;
            textBox6.Text = path;
            FileStream outFile2 = new FileStream(path, FileMode.Create);
            int n;
            byte[] t2 = new byte[1024];
            while ((n = unzip.Read(t2, 0, 1024)) != 0)
                outFile2.Write(t2, 0, n);

            unzip.Close();
            outFile2.Close();
            file2.Close();
            FileInfo fi = new FileInfo(path);
            textBox5.Text = "" + (fi.Length / 1024 + (fi.Length % 1024 != 0 ? 1 : 0)) + "Ko";
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }


    }
}
