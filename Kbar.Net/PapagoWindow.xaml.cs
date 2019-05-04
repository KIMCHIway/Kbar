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
using System.Windows.Shapes;

namespace Kbar.Net
{
    /// <summary>
    /// PapagoWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PapagoWindow : Window
    {
        MainWindow mainWindow;

        public PapagoWindow(dynamic window)
        {
            InitializeComponent();
            mainWindow = window;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            ShowInTaskbar = false;
            Topmost = true;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mainWindow.Focus();
        }

        private void Button_Close_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void Button_Copy_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Clipboard.SetText(Text_tText.Text);
        }
    }
}
