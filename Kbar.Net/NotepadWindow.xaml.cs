using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace Kbar.Net
{
    /// <summary>
    /// NotepadWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class NotepadWindow : Window
    {
        MainWindow mainWindow;

        public NotepadWindow(dynamic window)
        {
            InitializeComponent();
            mainWindow = window;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            ShowInTaskbar = false;
            Topmost = true;
            InputBox.Focus();

            // Load saved file
            try
            {
                string text = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "notepad.txt");
                InputBox.Text = text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r Report with Error code in https://github.com/KIMCHIway/Kbar", "201 ERROR CODE");
            }
        }

        private void InputBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            mainWindow.isFocus = false;
        }

        private void Button_Close_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                StreamWriter writer = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "notepad.txt");
                writer.Write(InputBox.Text);
                writer.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r Report with Error code in https://github.com/KIMCHIway/Kbar", "202 ERROR CODE");
            }

            Close();
        }

        private void Button_Copy_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Clipboard.SetText(InputBox.Text);
        }
    }
}
