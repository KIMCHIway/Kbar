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
    /// Window1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class HelpWindow : Window
    {
        MainWindow mainWindow;

        public HelpWindow(dynamic window)
        {
            InitializeComponent();
            mainWindow = window;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            ShowInTaskbar = false;
            Topmost = true;
        }

        private void Button_Github_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Go go = new Go();
            go.Connect_Chrome("https://github.com/KIMCHIway/Kbar");
        }

        private void Button_Close_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mainWindow.Close_EdgeWindow();
        }
    }
}
