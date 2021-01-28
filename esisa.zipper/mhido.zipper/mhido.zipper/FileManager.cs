using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace mhido.zipper
{
    class FileManager
    {
        private ArrayList files;
        public System.Collections.ArrayList Files
        {
            get { return files; }
            set { files = value; }
        }
        public FileManager()
        {
            files = new ArrayList();
        }
        public void add_file(File_info f)
        {
            files.Add(f);
        }
        public int taille_totale()
        {int some=0;
            foreach(File_info f in files){
                some+=f.Size_int;
            }
            return some;
        }
        public string generate_header(){
            string header = "";
            for (int i = 0; i < files.Count; i++)
            {
                if (i == (files.Count - 1)) { header = header + (files[i] as File_info).Info; }
                else
                { header = header + (files[i] as File_info).Info + ";"; }

            }
            
                return header;
        }

    }
}
