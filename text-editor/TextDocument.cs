using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace text_editor
{
    class TextDocument
    {
        //Auto implemented properties
        public string filePath { get; set; }
        public string fileName { get; set; }
        public string Content { get; set; }

        public TextDocument()
        {
            filePath = "";
            Content = "";
        }

        public void Open(OpenFileDialog ofd)
        {
            var fileStream = ofd.OpenFile();
            filePath = ofd.FileName;
            fileName = Path.GetFileName(filePath);
            StreamReader reader = new StreamReader(fileStream);
            var fileContent = reader.ReadToEnd();
            Content = fileContent;
        }

        public void Save(SaveFileDialog sfd)
        {
            filePath = sfd.FileName;
            fileName = Path.GetFileName(filePath);
            Save();
        }


        public void Save()
        {
            FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(Content);
            sw.Close();
            fs.Close();
        }
    }
}
