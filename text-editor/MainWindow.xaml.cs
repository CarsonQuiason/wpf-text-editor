using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.IO;

namespace text_editor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool canSave, canSaveAs = false;
        TextDocument td = new TextDocument();
        public MainWindow()
        {
            InitializeComponent();
        }

        public void New_File(Object sender, System.EventArgs e)
        {
            td = new TextDocument();
            textEditorBox.Text = td.Content;
        }

        public void Open_File(Object sender, System.EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = "c:\\";
            ofd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            ofd.FilterIndex = 2;
            ofd.RestoreDirectory = true;

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK) 
            {
                td.Open(ofd); //Sets TextDocument Class Variables
                textEditorBox.Text = td.Content;
                this.Title = td.filePath;
                canSave = true;
                canSaveAs = true;
            }
        }

        public void Save_File(Object sender, System.EventArgs e)
        {
            if (canSave)
            {

            }
        }

        public void Save_File_As(Object sender, System.EventArgs e)
        {
            if (canSaveAs)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.InitialDirectory = "c:\\";
                sfd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                sfd.ShowDialog();
                canSave = true;
            }
        }

        public void Exit(object sender, System.EventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }


    }
}
