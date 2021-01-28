using Ionic.Zip;
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

namespace mhido.zipper
{
    public partial class FolderCompressor : Form
    {
        public FolderCompressor()
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
              textBox1.Text = s;
              DirectoryInfo info = new DirectoryInfo(textBox1.Text);

              long size = DirSize(info);
              textBox2.Text = "" + (size / 1024 + (size % 1024 != 0 ? 1 : 0)) + "Ko";
              string[] k = s.Split('\\');
              textBox9.Text = k[k.Length-1] +".zip";
          }
          catch (System.Exception ex)
          {
          	
          }


        }

        private void mhido_compressor(string path)
        {
            List<string> dirs = GetDirectories(path);
            foreach (string s in dirs)
            {
                mhido_compressor(s);
            }
            string[] files = Directory.GetFiles(@"" + path);
         FileManager manager=new FileManager();

            foreach (string s in files) { 
                string[] filtre1 = s.Split('\\');
                FileInfo info = new FileInfo(s);
                File_info fi=new File_info(s,filtre1[filtre1.Length-1],(int)info.Length);
                manager.add_file(fi);
            }
            /*
             size of header
             * generate header
             * 
             */
        }
        private void button2_Click(object sender, EventArgs e)
        {
            
            folderBrowserDialog1.Description="selectionner un chemin :";
            if ((folderBrowserDialog1.ShowDialog() )== DialogResult.OK)
            {
                textBox4.Text = folderBrowserDialog1.SelectedPath ;
            }
        }

        private  List<string> GetDirectories(string path, string searchPattern = "*",
       SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            if (searchOption == SearchOption.TopDirectoryOnly)
                return Directory.GetDirectories(path, searchPattern).ToList();

            var directories = new List<string>(GetDirectories(path, searchPattern));

            for (var i = 0; i < directories.Count; i++)
                directories.AddRange(GetDirectories(directories[i], searchPattern));

            return directories;
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
                   if (contenu.Checked == true)
                   {
                       zip.AddDirectory(@"" + textBox1.Text);
                   }
                   else
                   {
                       string[] k = textBox1.Text.Split('\\');
                       MessageBox.Show(k[k.Length - 1]);
                       zip.AddDirectory(textBox1.Text, k[k.Length - 1]);
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

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

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

        private void button5_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.Description = "selectionner un chemin :";
            if ((folderBrowserDialog1.ShowDialog()) == DialogResult.OK)
            {
                textBox6.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {string[] k =textBox8.Text.Split('\\');
            string[] filter_zip=k[k.Length-1].Split('.');
            textBox6.Text += "\\"+filter_zip[0];
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
        
        private void button4_Click(object sender, EventArgs e)
        {
            string zipFilePath = textBox8.Text;
            string extractedFilePath = textBox6.Text;

            ZipFile zipfile = ZipFile.Read(zipFilePath);
            if (!Directory.Exists(extractedFilePath))
                Directory.CreateDirectory(extractedFilePath);
            int cp = 0;
            foreach (ZipEntry z in zipfile)
            {
                cp++;
                progressBar2.Value =cp*100 /(zipfile.Entries.Count);
                progressBar2.Update();
                z.Extract(extractedFilePath, ExtractExistingFileAction.OverwriteSilently);
            }
            MessageBox.Show("Fichier décompresser avec succes !!");
        }
    }
}
