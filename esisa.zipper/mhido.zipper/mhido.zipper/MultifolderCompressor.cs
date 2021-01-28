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
using System.Collections;
using Ionic.Zip;

namespace mhido.zipper
{ 
    public partial class MultifolderCompressor : Form
    {
        private long taille_totale = 0;
        ArrayList paths;
        public MultifolderCompressor()
        {
            InitializeComponent();
        }
        private long DirSize(DirectoryInfo d)
        {
            long size = 0;
            // Add file sizes.
            FileInfo[] fis = d.GetFiles();
            foreach (FileInfo fi in fis)
            {
                size += fi.Length;
            }
            // Add subdirectory sizes.
            DirectoryInfo[] dis = d.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                size += DirSize(di);
            }
            return size;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                folderBrowserDialog1.ShowDialog();

                string s = folderBrowserDialog1.SelectedPath;
                listBox1.Items.Add(s);
                paths.Add(s);
                DirectoryInfo info = new DirectoryInfo(s);

                long size = DirSize(info);
                if (taille_totale == 0)
                {
                    string[] k = s.Split('\\');
                    textBox9.Text = k[k.Length - 1] + ".zip";
                }

                taille_totale += size;
                textBox2.Text = "" + (taille_totale / 1024 + (taille_totale % 1024 != 0 ? 1 : 0)) + "Ko";
                
            }
            catch (System.Exception ex)
            {

            }
        }

        private void MultifolderCompressor_Load(object sender, EventArgs e)
        {
            paths = new ArrayList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.Description = "selectionner un chemin :";
            if ((folderBrowserDialog1.ShowDialog()) == DialogResult.OK)
            {
                textBox4.Text = folderBrowserDialog1.SelectedPath;
            }
        }
        private void Zip_SaveProgress(object sender, SaveProgressEventArgs e)
        {
            if (e.EventType == Ionic.Zip.ZipProgressEventType.Saving_BeforeWriteEntry)
            {
                progressBar1.Invoke(new MethodInvoker(delegate
                {
                    progressBar1.Maximum = e.EntriesTotal;
                    progressBar1.Value = e.EntriesSaved + 1;
                    progressBar1.Update();
                }));
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                using (ZipFile zip = new ZipFile())
                {
                    zip.UseUnicodeAsNecessary = true;  // utf-8
                    foreach (string p in paths)
                    {string[] k =p.Split('\\');
                    MessageBox.Show(k[k.Length - 1]);
                    zip.AddDirectory(p, k[k.Length - 1]);
                    }
                    
                    zip.Comment = "ce fichier a ete creer dans: " + System.DateTime.Now.ToString("G") + "on utilisant mhido.zipper";
                    progressBar1.Maximum = zip.Entries.Count;
                    zip.SaveProgress += Zip_SaveProgress;

                    zip.Save(textBox4.Text + "\\" + textBox9.Text);

                    MessageBox.Show("folder compressed successfully !!");
                    FileInfo fi = new FileInfo(textBox4.Text + "\\" + textBox9.Text);
                    textBox3.Text = "" + (fi.Length / 1024 + (fi.Length % 1024 != 0 ? 1 : 0)) + "Ko";
                    //calc ratio
                    string ko = "ko";
                    string totale = textBox2.Text.Remove(textBox2.Text.Length - ko.Length);
                    string apres = textBox3.Text.Remove(textBox3.Text.Length - ko.Length);
                    long x = (100 - ((long.Parse(apres) * 100) / (long.Parse(totale))));
                    textBox10.Text = "" + x + " %";
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("erreur de compression verrifier les chemins ");
            }
        }
    }
}
