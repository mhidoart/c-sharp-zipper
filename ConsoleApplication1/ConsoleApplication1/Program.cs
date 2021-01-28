using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;
namespace ConsoleApplication1

{
    class Program
    { 
        [STAThread]
        static void Main(string[] args)
        {
            #region 
            //             compression
//                         FileStream file = new FileStream("D:/test.dat", FileMode.Open);
//                         int taille = (int)file.Length;
//                         byte[] t = new byte[taille];
//                         file.Read(t, 0, taille);
//             
//                         FileStream outFile = new FileStream("D:/test.zip", FileMode.Create);
//                         GZipStream zip = new GZipStream(outFile,CompressionMode.Compress);
//                         zip.Write(t, 0, taille);
//                         zip.Close();
//             
//                         FileStream file2 = new FileStream("D:/test.zip", FileMode.Open);
//                         GZipStream unzip = new GZipStream(file2,CompressionMode.Decompress);
//             
//                         FileStream outFile2 = new FileStream("D:/test2.dat", FileMode.Create);
//                         unzip.Read(t, 0, taille);
//                         outFile2.Write(t, 0, taille);
//                         int n;
//                         byte[] t2 = new byte[1024];
//                         while ((n = unzip.Read(t2, 0, 1024)) != 0)
//                             outFile2.Write(t2, 0, n);
//                         unzip.Close();
            //                         outFile2.Close();
            #endregion
            
            Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            
        }
    }
}
