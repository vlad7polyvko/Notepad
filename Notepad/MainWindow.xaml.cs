using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Win32;

namespace Notepad
{
    public partial class MainWindow : Window
    {
        private bool isTextChanged = false;
        private string currentFilePath = string.Empty;

        public MainWindow()
        {
            InitializeComponent();
            UpdateWindowTitle();
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            PromptToSaveChanges();

            textBox.Clear();
            currentFilePath = string.Empty;
            isTextChanged = false;
            UpdateWindowTitle();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            PromptToSaveChanges();

            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string filename = openFileDialog.FileName;
                string fileText = File.ReadAllText(filename);
                textBox.Text = fileText;
                currentFilePath = filename;
                isTextChanged = false;
                UpdateWindowTitle();
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(currentFilePath))
            {
                SaveAs_Click(sender, e);
            }
            else
            {
                SaveToFile(currentFilePath);
            }
        }

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                string filename = saveFileDialog.FileName;
                SaveToFile(filename);
                currentFilePath = filename;
            }
        }

        private void SaveToFile(string filePath)
        {
            File.WriteAllText(filePath, textBox.Text);
            isTextChanged = false;
            UpdateWindowTitle();
        }

        private void PromptToSaveChanges()
        {
            if (isTextChanged)
            {
                MessageBoxResult result = MessageBox.Show("Do you want to save the changes?", "Confirm", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Save_Click(this, null);
                }
                else if (result == MessageBoxResult.Cancel)
                {
                    return;
                }
            }
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            isTextChanged = true;
            UpdateWindowTitle();
        }

        private void UpdateWindowTitle()
        {
            string title = "Text Editor";
            if (!string.IsNullOrEmpty(currentFilePath))
            {
                title += " - " + Path.GetFileName(currentFilePath);
            }
            if (isTextChanged)
            {
                title += " (Unsaved Changes)";
            }
            this.Title = title;
        }

        private void Font_Click(object sender, RoutedEventArgs e)
        {
            //FontDialog fontDialog = new FontDialog(); 
            //DialogResult result = fontDialog.ShowDialog();
            //if (result == DialogResult.OK) 
            {
                //FontFamily fontFamily = new FontFamily(fontDialog.Font.Name);
                //double fontSize = fontDialog.Font.Size * 96.0 / 72.0; 
                //textBox.FontFamily = fontFamily;
                //textBox.FontSize = fontSize;
            }
        }

        private void Cut_Click(object sender, RoutedEventArgs e)
        {
            textBox.Cut();
        }

        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            textBox.Copy();
        }

        private void Paste_Click(object sender, RoutedEventArgs e)
        {
            textBox.Paste();
        }

        private void SelectAll_Click(object sender, RoutedEventArgs e)
        {
            textBox.SelectAll();
        }
    }
}



