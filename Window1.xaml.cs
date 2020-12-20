using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace WpfApplication1
{
    /// <summary>
    /// Window1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Window1 : Window
    {
        String dirPath;
        DirectoryInfo dirInfo;
        FileInfo[] fileArr;

        Dictionary<String, String> FileNameDict = new Dictionary<String, String>();

        public Window1()
        {
            InitializeComponent();
            DateBox.Text = DateTime.Today.ToString();
        }

        private void ConvertClick(object sender, RoutedEventArgs e)
        {
            foreach (FileInfo fi in fileArr)
            {
                string newName = dirPath + "\\" + FileNameDict[fi.Name];
                fi.MoveTo(newName);
            }
            FileNameDict.Clear();
            foreach (FileInfo fi in fileArr)
            {
                FileNameDict.Add(fi.Name, fi.Name);
            }
            FileListRefresh();
            DebugBox.Text += "\n" + "Names of " + fileArr.Length.ToString() + "files successfully changed.";
        }

        private void LoadFilesClick(object sender, RoutedEventArgs e)
        {
            FileNameDict.Clear();
            dirPath = DirBox.Text;
            dirInfo = new DirectoryInfo(dirPath);
            fileArr = dirInfo.GetFiles();

            foreach (FileInfo fi in fileArr)
            {
                FileNameDict.Add(fi.Name, fi.Name);
            }
            FileListRefresh();
            fromAmount.Text = fileArr.Length.ToString();
            toggle1.Visibility = Visibility.Visible;
            toAmount.Text = fileArr.Length.ToString();
            toggle2.Visibility = Visibility.Visible;
        }
        
        private void ChangeTextClick(object sender, RoutedEventArgs e)
        {
            String fromText = FromBox.Text;
            String toText = ToBox.Text;
            try
            {
                for (int i = 0; i < fileArr.Length; ++i)
                {
                    String converted;
                    converted = FileNameDict[fileArr[i].Name];
                    converted = converted.Replace(fromText, toText);
                    KeyValuePair<String, String> fileName = new KeyValuePair<string, string>(fileArr[i].Name, converted);
                    FileNameDict.Remove(fileName.Key);
                    FileNameDict.Add(fileName.Key, fileName.Value);
                }
                FileListRefresh();
                FromBox.Clear();
                ToBox.Clear();
            }
            catch (Exception exc)
            {
                DebugBox.Text = exc.ToString();
            }

        }
        private void PrefixClick(object sender, RoutedEventArgs e)
        {
            String prefixText = PrefixBox.Text;
            try
            {
                for(int i=0; i<fileArr.Length; ++i)
                {
                    KeyValuePair<String, String> fileName = new KeyValuePair<string,string>(fileArr[i].Name, prefixText + FileNameDict[fileArr[i].Name]);
                    FileNameDict.Remove(fileName.Key);
                    FileNameDict.Add(fileName.Key, fileName.Value);
                }
                FileListRefresh();
                PrefixBox.Clear();
            }
            catch (Exception exc)
            {
                DebugBox.Text = exc.ToString();
            }
        }
        private void PostfixClick(object sender, RoutedEventArgs e)
        {
            String postfixText = PostfixBox.Text;
            try
            {
                for (int i = 0; i < fileArr.Length; ++i)
                {
                    String converted = FileNameDict[fileArr[i].Name];
                    String extension = System.IO.Path.GetExtension(dirInfo + converted);
                    converted = converted.Replace(extension, "");
                    KeyValuePair<String, String> fileName = new KeyValuePair<string, string>(fileArr[i].Name, converted + postfixText + extension);
                    FileNameDict.Remove(fileName.Key);
                    FileNameDict.Add(fileName.Key, fileName.Value);
                }
                FileListRefresh();
                PostfixBox.Clear();
            }
            catch (Exception exc)
            {
                DebugBox.Text = exc.ToString();
            }
        }

        private void FileListRefresh()
        {
            FileList.Items.Clear();
            ConvertedList.Items.Clear();
            foreach (KeyValuePair<String, String> fileName in FileNameDict)
            {
                FileList.Items.Add(fileName.Key);
                ConvertedList.Items.Add(fileName.Value);
            }
        }

        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                for (int i = 0; i < fileArr.Length; ++i)
                {
                    KeyValuePair<String, String> fileName = new KeyValuePair<string, string>(fileArr[i].Name, fileArr[i].Name);
                    FileNameDict.Remove(fileName.Key);
                    FileNameDict.Add(fileName.Key, fileName.Value);
                }
                FileListRefresh();
                PrefixBox.Clear();
            }
            catch (Exception exc)
            {
                DebugBox.Text = exc.ToString();
            }
        }

        private void numberingButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                for (int i = 0; i < fileArr.Length; ++i)
                {
                    String converted = FileNameDict[fileArr[i].Name];
                    String extension = System.IO.Path.GetExtension(dirInfo + converted);
                    converted = converted.Replace(extension, "");
                    KeyValuePair<String, String> fileName = new KeyValuePair<string, string>(fileArr[i].Name, '-' + i.ToString() + converted + extension);
                    FileNameDict.Remove(fileName.Key);
                    FileNameDict.Add(fileName.Key, fileName.Value);
                }
                FileListRefresh();
                PostfixBox.Clear();
            }
            catch (Exception exc)
            {
                DebugBox.Text = exc.ToString();
            }
        }

        private void erasingButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                for (int i = 0; i < fileArr.Length; ++i)
                {
                    String converted = FileNameDict[fileArr[i].Name];
                    String extension = System.IO.Path.GetExtension(dirInfo + converted);
                    KeyValuePair<String, String> fileName = new KeyValuePair<string, string>(fileArr[i].Name, extension);
                    FileNameDict.Remove(fileName.Key);
                    FileNameDict.Add(fileName.Key, fileName.Value);
                }
                FileListRefresh();
                PostfixBox.Clear();
            }
            catch (Exception exc)
            {
                DebugBox.Text = exc.ToString();
            }
        }
    }
}
