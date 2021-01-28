using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Compression;


namespace mhido.zipper
{
    public partial class MultipleFiles : Form
    {
        private Int64 taille_totale = 0;
        private FileManager manager;
        public MultipleFiles()
        {
            manager = new FileManager();
            InitializeComponent();
        }
       
        private void button2_Click(object sender, EventArgs e)
        {
             folderBrowserDialog1.ShowDialog();
             textBox4.Text = folderBrowserDialog1.SelectedPath+"\\MultiFiles.zip";
           
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string header=manager.generate_header();
            MessageBox.Show(header);
            int len = header.Length;
            byte[] tlen = new byte[4];
            tlen = BitConverter.GetBytes(len);
            byte[] tabheader = new byte[len * 2];
            tabheader = Encoding.Unicode.GetBytes(header);

            FileStream outFile = new FileStream(textBox4.Text, FileMode.Create);
            GZipStream zip = new GZipStream(outFile, CompressionMode.Compress);
            zip.Write(tlen,0,4);
            zip.Write(tabheader, 0, len * 2);
            foreach (File_info f in manager.Files)
            {

                FileStream file = new FileStream(f.Path, FileMode.Open);
                int taille = (int)file.Length;
                byte[] t = new byte[taille];
                file.Read(t, 0, taille);
                zip.Write(t, 0, taille);
                file.Close();
            }
            zip.Close();
            MessageBox.Show("files are  compressed successfully !!!");
            FileInfo info = new FileInfo(textBox4.Text);
            textBox3.Text = "" + (info.Length / 1024 + (info.Length % 1024 != 0 ? 1 : 0)) + "Ko";
            
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.ShowDialog();
                string s = openFileDialog1.FileName;
                listBox1.Items.Add(s);
                FileInfo info = new FileInfo(s);
                 taille_totale= taille_totale+ (info.Length / 1024 + (info.Length % 1024 != 0 ? 1 : 0));
                 textBox2.Text = taille_totale + "Ko";
                string[] filtre1 = s.Split('\\');
                File_info fi=new File_info(s,filtre1[filtre1.Length-1],(int)info.Length);
                manager.add_file(fi);
                
            }
            catch (System.Exception ex)
            {
                
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

            {
                if (textBox8.Text.Equals(""))
                {
                    MessageBox.Show("veuiller choisir un fichier source et un fichier a decompreser !!!");
                }
                else
                {
                    try
                    {
                        FolderBrowserDialog f = new FolderBrowserDialog();
                        f.ShowDialog();
                        textBox6.Text = f.SelectedPath;
                        FileStream file2 = new FileStream(textBox8.Text, FileMode.Open);
                        GZipStream unzip = new GZipStream(file2, CompressionMode.Decompress);
                        string path = f.SelectedPath + "\\";
                        byte[] tlen = new byte[4];
                        unzip.Read(tlen, 0, 4);
                        int len = BitConverter.ToInt32(tlen, 0);
                        byte[] tnom = new byte[len * 2];
                        unzip.Read(tnom, 0, len * 2);
                        string nom = Encoding.Unicode.GetString(tnom);
                        string[] filtre1 = nom.Split(';');
                        long taille_totale_after_decompress = 0;

                        foreach (string info in filtre1)
                        {
                            string[] filtre2 = info.Split(',');
                           // byte[] size_f = new byte[4];
                         //   size_f= Encoding.Unicode.GetBytes(filtre2[1]);
                            int len_file = Int32.Parse(filtre2[1]); // BitConverter.ToInt32(size_f,0);
                            FileStream outFile = new FileStream(path+filtre2[0], FileMode.Create);
                            MessageBox.Show("nom: " + path + filtre2[0] + " size : " + len_file);
                            byte[] t2 = new byte[len_file];
                            unzip.Read(t2, 0, len_file);
                                outFile.Write(t2, 0,len_file );      
                            outFile.Close();
                            FileInfo fi = new FileInfo(path + filtre2[0]);
                            taille_totale_after_decompress += fi.Length;
                        }
                       
                        textBox6.Text = path;
                        
                       
                        textBox5.Text = "" + (taille_totale_after_decompress / 1024 + (taille_totale_after_decompress % 1024 != 0 ? 1 : 0)) + "Ko";
                        MessageBox.Show("les fichier sont decompresser avec succee !!");
                        unzip.Close();
                        file2.Close();
                        
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show("errno : " + ex.Message);
                    }
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog op = new OpenFileDialog();
                op.ShowDialog();
                string s = op.FileName;
                textBox8.Text = s;
                FileInfo info = new FileInfo(s);
                textBox7.Text = "" + (info.Length / 1024 + (info.Length % 1024 != 0 ? 1 : 0)) + "Ko";
                string[] filtre1 = textBox8.Text.Split('\\');
                string[] filtre2 = filtre1[filtre1.Length - 1].Split('.');
             //   textBox4.Text = filtre2[0] + ".zip"; ////////nm du dossier
            }
            catch (System.Exception ex)
            {
                textBox8.Text = "";
                textBox7.Text = "";
            }
        }

        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {

        }
    }
}
