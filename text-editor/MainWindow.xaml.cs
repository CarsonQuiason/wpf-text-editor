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
    /// Carson Rottinghaus
    public partial class MainWindow : Window
    {
        bool textChanged;
        TextDocument td = new TextDocument();
        public MainWindow()
        {
            InitializeComponent();
            fastSaveBtn.IsEnabled = false;
        }

        private void textChangedEventHandler(object sender, TextChangedEventArgs args)
        {
            textChanged = true;
            fastSaveBtn.IsEnabled = true;
        }

        public void New_File(Object sender, System.EventArgs e)
        {
            if (textChanged)
            {
                int result = prompt_unsaved_changes();
                if(result == -1)
                {
                    return;
                }
            }
            td = new TextDocument();
            textEditorBox.Text = td.Content;
            Title = "New Document";
            fastSaveBtn.IsEnabled = false;
            textChanged = false;
        }

        public void Open_File(Object sender, System.EventArgs e)
        {
            if (textChanged)
            {
                int result = prompt_unsaved_changes();
                if (result == -1)
                {
                    return;
                }
            }
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "txt files (*.txt)|*.txt";
            ofd.FilterIndex = 2;
            ofd.RestoreDirectory = true;

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK) 
            {
                td.Open(ofd); //Sets TextDocument Class Variables
                textEditorBox.Text = td.Content;
                Title = td.fileName;
                textChanged = false;
                fastSaveBtn.IsEnabled = false;
            }

        }

        public void Exit(object sender, System.EventArgs e)
        {
            if (textChanged)
            {
                int result = prompt_unsaved_changes();
                if (result == -1)
                {
                    return;
                }
                textChanged = false;
            }
            System.Windows.Application.Current.Shutdown();
        }

        public void Fast_Save_File(Object sender, System.EventArgs e)
        {
            if (textChanged)
            {
                td.Content = textEditorBox.Text;
                if (td.filePath == "") //If file is new
                {
                    Save_File_Dialog(); //Prompt for save location
                }
                else
                {
                    td.Save();
                    fastSaveBtn.IsEnabled = false;
                }
                textChanged = false;
            }
        }

        public void Save_Location_Handler(Object sender, System.EventArgs e)
        {
            Save_File_Dialog();
        }

        public void Open_About(object sender, System.EventArgs e)
        {
            string message = "Created by Carson Rottinghaus\n\nThe text editor has the following functions:\nNew: Creates a new blank text document\nOpen: Opens a prompt to select a text document" +
                "\nSave: Saves a text document, prompts user with a file location if one is not saved\nSave As: Prompts user with a save location to save the file\nExit: Exits the program";
            string caption = "About";
            System.Windows.Forms.MessageBox.Show(message, caption);
        }
        private void Save_File_Dialog()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "txt files (*.txt)|*.txt";
            sfd.RestoreDirectory = true;
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                td.Content = textEditorBox.Text;
                td.Save(sfd);
                Title = td.fileName;
                textChanged = false;
                fastSaveBtn.IsEnabled = false;
            }
        }
        private int prompt_unsaved_changes()
        {
            string message = "Your current document has not been saved. Do you want to save your changes?";
            string caption = "Unsaved Changes";
            MessageBoxButtons buttons = MessageBoxButtons.YesNoCancel;
            DialogResult result;
            result = System.Windows.Forms.MessageBox.Show(message, caption, buttons);
            switch (result)
            {
                case System.Windows.Forms.DialogResult.Cancel:
                    return -1;

                case System.Windows.Forms.DialogResult.Yes:
                    if (td.filePath == "")
                    {
                        Save_File_Dialog();
                    }
                    else
                    {
                        td.Content = textEditorBox.Text;
                        td.Save();
                    }
                    fastSaveBtn.IsEnabled = false;
                    return 0;

                case System.Windows.Forms.DialogResult.No:
                    return 0;
            }
            return 0;
        }
    }
}
