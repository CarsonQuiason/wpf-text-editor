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
            StreamReader reader = new StreamReader(fileStream);
            var fileContent = reader.ReadToEnd();
            Content = fileContent;
        }

        public void Save(SaveFileDialog sfd)
        {
            filePath = sfd.FileName;

        }


        public void Save()
        {
            
        }
        //public string Parse_File_Path(string fileName)
        //{
            //List<String> fileNameArray = fileName.Split("//").ToList;
            //return fileNameArray;
        //}
                    
    }
}
