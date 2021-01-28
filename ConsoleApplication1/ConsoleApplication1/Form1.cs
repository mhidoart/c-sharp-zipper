using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using System.Collections;
using System.Reflection;
using System.Collections;
namespace ConsoleApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
//             OpenFileDialog op = new OpenFileDialog();
//             op.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            textBox1.Text = openFileDialog1.FileName;
            FileStream file = new FileStream(textBox1.Text, FileMode.Open);
            textBox2.Text = ""+(file.Length/1024+(file.Length%1024!=0?1:0))+"Ko";
            file.Close();
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            f.ShowDialog();
            textBox3.Text = f.SelectedPath + "\\" + textBox3.Text;
        }
        public ArrayList load()
        {ArrayList lines=new ArrayList();
            try
            {
                StreamReader r = new StreamReader(textBox1.Text);
               
                string s;
                while ((s = r.ReadLine()) != null)
                {
                    lines.Add(s);
                }
                r.Close();
            }
            catch (System.Exception ex)
            {

            }
           
            return lines;

        }
        public void save(string header ,ArrayList what,string path)
        {
            path = path + 2;
            try
            {
                MessageBox.Show(header);
                StreamWriter f = new StreamWriter(path);
                f.WriteLine(Encoding.ASCII.GetBytes(header).Length);
                f.WriteLine(header);
                foreach(string s in what)
                {
                    f.WriteLine(s);
                }
                
                f.Close();
            }
            catch (System.Exception ex)
            {

            }

        }
        private void button4_Click(object sender, EventArgs e)
        {
            string[] s = textBox1.Text.Split('\\');

            try
            {
                string path = textBox1.Text;
                FileStream file = new FileStream(path, FileMode.Open);


                int taille = (int)file.Length;
                byte[] t = new byte[taille];
                file.Read(t, 0, taille);

                FileStream outFile = new FileStream(textBox3.Text, FileMode.Create);
                GZipStream zip = new GZipStream(outFile, CompressionMode.Compress);

                int len;
                string nom;
                string[] tstring = path.Split('\\');
                nom = tstring[tstring.Length-1];
                len = nom.Length;
                byte[] tlen = new byte[4];
                tlen = BitConverter.GetBytes(len);
                zip.Write(tlen,0,4);
                byte[] tnom = new byte[len * 2];
                tnom=Encoding.Unicode.GetBytes(nom);
                zip.Write(tnom, 0, len * 2);
                zip.Write(t, 0, taille);
                zip.Close();
                MessageBox.Show("file compressed successfully !!!");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("erreur des chemin !!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            textBox5.Text = openFileDialog1.FileName;
            FileStream file = new FileStream(textBox5.Text, FileMode.Open);
            textBox7.Text = "" + (file.Length / 1024 + (file.Length % 1024 != 0 ? 1 : 0)) + "Ko";
            file.Close();

        }
        private ArrayList first_step()
        {
            ArrayList lines = new ArrayList();
            try
            {

                FileStream file2 = new FileStream(textBox5.Text, FileMode.Open);
                GZipStream unzip = new GZipStream(file2, CompressionMode.Decompress);
                FileStream outFile2 = new FileStream(AppDomain.CurrentDomain.BaseDirectory+"\\temp.txt", FileMode.Create);
                int n;
                byte[] t2 = new byte[1024];

                while ((n = unzip.Read(t2, 0, 1024)) != 0)
                    outFile2.Write(t2, 0, n);
                unzip.Close();
                outFile2.Close();
            }
            catch (Exception ex) { MessageBox.Show("errno : " + ex.Message); }
            return lines;
        }
        private ArrayList load_temp_file()
        {                ArrayList lines = new ArrayList(); 

            try
            {
                MessageBox.Show(AppDomain.CurrentDomain.BaseDirectory + "temp.txt");
                StreamReader r = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "temp.txt");
                string s;
                while ((s = r.ReadLine()) != null)
                {
                   lines.Add(s);
                }
                r.Close();
            }
            catch (System.Exception ex)
            {

            }
            return lines;
        }
        private void second_step()
        {
            ArrayList lines = load_temp_file();
            try
            {
                MessageBox.Show(textBox6.Text + "\\" + (lines[1] as string));
                StreamWriter f = new StreamWriter(textBox6.Text+"\\"+(lines[1] as string));

                for (int i = 2; i < lines.Count;i++ )
                {
                    f.WriteLine((lines[i] as string));
                }
                f.Close();
            }
            catch (System.Exception ex)
            {

            }

        }
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex) { MessageBox.Show("errno : " + ex.Message); }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            byte[] bytes = BitConverter.GetBytes(8);
            MessageBox.Show("byte array: " + BitConverter.ToInt32(bytes,0));
            MessageBox.Show("byte array: " + BitConverter.ToString(bytes, 0));
            MessageBox.Show(AppDomain.CurrentDomain.BaseDirectory);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            f.ShowDialog();
            FileStream file2 = new FileStream(textBox5.Text, FileMode.Open);
            GZipStream unzip = new GZipStream(file2, CompressionMode.Decompress);
            string path= f.SelectedPath + "\\" ;
            byte[] tlen=new byte[4];
            unzip.Read(tlen,0,4);
            int len = BitConverter.ToInt32(tlen,0);
            byte[] tnom=new byte[len*2];
            unzip.Read(tnom,0,len*2);
            string nom= Encoding.Unicode.GetString(tnom);

            path = path  + nom;
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
            textBox8.Text= "" + (fi.Length / 1024 + (fi.Length % 1024 != 0 ? 1 : 0)) + "Ko";

        }
    }
}
