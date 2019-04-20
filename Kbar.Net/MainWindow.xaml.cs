using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kbar.Net
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private LowKeyListener _listener;
		private readonly DispatcherTimer timer;

        private bool isTurn;

        public MainWindow()
        {
            InitializeComponent();
			
			// Code for input timer
			timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1.5f) };
			timer.Tick += HandleCommand;
			
			// Code for hook key
            _listener = new LowKeyListener();
            _listener.OnKeyPressed += _listener_OnKeyPressed;

            _listener.HookKeyboard();
        }
		
		private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            ShowInTaskbar = false;
            Topmost = true;

            Top = (SystemParameters.PrimaryScreenHeight / 6) - (Height / 2);
            Visibility = Visibility.Hidden;
        }
		

        void _listener_OnKeyPressed(object sender, KeyPressedArgs e)
        {
            if (e.KeyPressed.ToString() == "F8")
            {
                if (isTurn)
                {
                    Visibility = Visibility.Hidden;
                    isTurn = false;
                }
                else
                {
                    Visibility = Visibility.Visible;
                    isTurn = true;
                }
            }

            if (e.KeyPressed.ToString() == "Escape")
            {
                Visibility = Visibility.Hidden;
                isTurn = false;
            }
        }


        private void CommandBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Wait for 1.5s to check whether user will not type anymore.
			timer.Stop();
			timer.Start();
        }
		
		private void HandleCommand(object sender, EventArgs e)
		{
		    timer.Stop();
			
		    // Split by blank (0 is Command)
			string[] command = CommandBox.Text.Split(' ');
			if (command.Length > 0)
			{
			    string module = command[0];
				switch (module)
				{
				    case 'ntrans':
					case 'ntranslation':
					    Papago papago = new Papago();
						
						break;
				}
			}
			
		}

        //private void Grid_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        //{
        //    _listener.UnHookKeyboard();
        //}
    }
}
