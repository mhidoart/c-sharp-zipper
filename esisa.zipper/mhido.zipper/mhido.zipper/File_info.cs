using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mhido.zipper
{
    class File_info
    {
        private string info;
        public string Info
        {
            get { return info; }
            set { info = value; }
        }
        private byte[] name;
        public byte[] Name
        {
            get { return name; }
            set { name = value; }
        }
        private int size_int;
        public int Size_int
        {
            get { return size_int; }
            set { size_int = value; }
        }
        private byte[] size;
        public byte[] Size
        {
            get { return size; }
            set { size = value; }
        }
        private string path;
        public string Path
        {
            get { return path; }
            set { path = value; }
        }
        public File_info()
        {

        }
        public File_info(string path,string nom , int size)
        {
            this.path = path;
            string info=nom+","+size;
            int len = info.Length;
            byte[] tlen = new byte[4];
            tlen = BitConverter.GetBytes(len);
            name = new byte[len * 2];
            name = Encoding.Unicode.GetBytes(info);
            this.size = tlen;
            this.size_int = size;
            this.info = info;
        }
    }
}
