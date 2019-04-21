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
using System.Windows.Threading;
using static System.Console;

// Kbar command docs : https://docs.google.com/document/d/1-0STF1GnlQi-IYj8PNtPBaO657w1jOwlGT1H6X81nZw/edit?usp=sharing
// Code of GUI MainWindows.xaml 

namespace Kbar.Net
{
	
    public partial class MainWindow : Window
    {
        private LowKeyListener _listener;

        private dynamic cur_SubWindow;
        private dynamic cur_Module;
        private bool isTurn;

        public MainWindow()
        {
            InitializeComponent();
			
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
            // Hotkey for window
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


            // Hotkey for command
            if (e.KeyPressed.ToString() == "Return")
            {
                CommandMethod();
            }
        }


        private void CommandBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // module check
        }
		
		private void CommandMethod()
		{
            // Thread ?
            // Split by blank (0 is Command)
            string[] command = CommandBox.Text.Split(' ');

            if (command.Length > 0)
            {
                string module = command[0];
                switch (module)
                {
                    // papago module command
                    case "papago":
                    case "nt":
                    case "ntranslation":
                    case "translation":
                        if (command.Length >= 4)
                        {
                            // Close sub form which is showed
                            if (cur_SubWindow != null) cur_SubWindow.Close();

                            // Turn on new sub form
                            PapagoWindow subWindow = new PapagoWindow();
                            Load_SubWindow(subWindow);

                            // Combine seperated source text (INDEX 0:papago 1:en 2:ko 3:text1 4:text2 ~
                            string sourceText = string.Empty;
                            for (int i = 3; i < command.Length; i++)
                            {
                                sourceText += command[i] + " ";
                            }

                            // Call Papago API
                            Papago papago = new Papago();
                            string targetText = papago.Call_Papago(command[1], command[2], sourceText);

                            // Allocate state variable
                            Allocate_State(subWindow, papago);


                            // Set window component
                            subWindow.Label_sCode.Content = command[1];
                            subWindow.Label_tCode.Content = command[2];
                            subWindow.Label_sText.Content = sourceText;
                            subWindow.Label_tText.Content = targetText;
                        }

                        break;
                }
            }
            else if (command.Length == 0)
            {
                // if Sub Windows is showed, intialize state variable
                if (cur_SubWindow != null)
                {
                    cur_SubWindow.Close();
                    cur_SubWindow = null;

                    cur_Module = null;
                }
            }
        }


        private void Load_SubWindow(dynamic window)
        {
            window.Show();
            window.Left = Left;
            window.Top = Top + Height - 10;
        }

        private void Allocate_State(dynamic subWindow, dynamic module)
        {
            cur_SubWindow = subWindow;
            cur_Module = module;
        }

        //private void Grid_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        //{
        //    _listener.UnHookKeyboard();
        //}
    }
}
