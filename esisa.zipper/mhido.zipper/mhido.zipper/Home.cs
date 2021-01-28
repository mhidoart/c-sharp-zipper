using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mhido.zipper
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MultipleFiles mf = new MultipleFiles();
            mf.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderCompressor fc = new FolderCompressor();
            fc.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            this.Hide();
            Entry eee = new Entry();
            eee.Show();
            wait(2000);
            eee.Close();
            this.Show();
        }
        public void wait(int milliseconds)
        {
            System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();
            if (milliseconds == 0 || milliseconds < 0) return;
            //Console.WriteLine("start wait timer");
            timer1.Interval = milliseconds;
            timer1.Enabled = true;
            timer1.Start();
            timer1.Tick += (s, e) =>
            {
                timer1.Enabled = false;
                timer1.Stop();
                //Console.WriteLine("stop wait timer");
            };
            while (timer1.Enabled)
            {
                Application.DoEvents();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MultifolderCompressor mfc = new MultifolderCompressor();
            mfc.ShowDialog();
        }
    }
}
