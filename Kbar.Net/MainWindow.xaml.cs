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

        // To delete the sub window which is turn on
        private dynamic cur_SubWindow;
        // To seperate what to do when F8 is pressed
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
                    CommandBox.Text = string.Empty;

                    Close_SubWindow();

                    Visibility = Visibility.Hidden;
                    isTurn = false;

                }
                else
                {
                    PapagoWindow papagoWindow = new PapagoWindow();
                    Load_SubWindow(papagoWindow);

                    Visibility = Visibility.Visible;
                    isTurn = true;

                    // Focus on TextBox when it turns on
                    CommandBox.Focusable = true;
                    CommandBox.Focus();
                }
            }

            if (e.KeyPressed.ToString() == "Escape")
            {
                CommandBox.Text = string.Empty;

                Close_SubWindow();

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
                switch (module.ToLower())
                {
                    // go module
                    case "go":
                        if (command.Length >= 2)
                        {
                            Go go = new Go();
                            go.Call_Go(command[1]);
                        }

                        break;

                    // papago module
                    case "papago":
                    case "nt":
                    case "ntranslation":
                    case "translation":
                        if (command.Length >= 4)
                        {
                            // Close sub form which is showed
                            if (cur_SubWindow != null) cur_SubWindow.Close();

                            // Turn on new sub form
                            PapagoWindow papagoWindow = new PapagoWindow();
                            Load_SubWindow(papagoWindow);

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
                            cur_SubWindow = papagoWindow;


                            // Set window component
                            papagoWindow.Label_sCode.Content = command[1];
                            papagoWindow.Label_tCode.Content = command[2];
                            papagoWindow.Label_sText.Content = sourceText;
                            papagoWindow.Label_tText.Content = targetText;
                        }

                        break;
                }
            }
            else if (command.Length == 0)
            {
                // if Sub Windows is showed, intialize state variable
                Close_SubWindow();
            }
        }

        private void Close_SubWindow()
        {
            if (cur_SubWindow != null)
            {
                cur_SubWindow.Close();
                cur_SubWindow = null;
            }
        }

        private void Load_SubWindow(dynamic window)
        {
            window.Show();
            window.Left = Left;
            window.Top = Top + Height / 3 - 21;
        }

        //private void Grid_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        //{
        //    _listener.UnHookKeyboard();
        //}
    }
}
