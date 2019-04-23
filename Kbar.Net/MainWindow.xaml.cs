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

            CommandBox.Focusable = true;
        }
		

        void _listener_OnKeyPressed(object sender, KeyPressedArgs e)
        {
            // Hotkey for window
            if (e.KeyPressed.ToString() == "F8")
            {
                if (isTurn)
                {
                    Hide_MainWindow();

                    Close_SubWindow();
                }
                else
                {
                    Display_MainWindow();
                }
            }

            if (e.KeyPressed.ToString() == "Escape")
            {
                Hide_MainWindow();

                Close_SubWindow();
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
            // Close sub form which is showed
            if (cur_SubWindow != null) Close_SubWindow();

            // Thread ?
            // Split by blank (0 is Command)
            string[] command = CommandBox.Text.Split(' ');

            if (command.Length > 0)
            {
                string module = command[0];
                switch (module.ToLower())
                {
                    // Go Module
                    case "go":
                        if (command.Length >= 2)
                        {
                            Go go = new Go();
                            go.Call_Go(command[1]);

                            Hide_MainWindow();
                        }

                        break;

                    // Papago Module
                    case "papago":
                    case "nt":
                    case "ntranslation":
                    case "translation":
                        if (command.Length >= 4)
                        {
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

                    // Naver Dictionary Module
                    case "nd":
                    case "ndictionary":
                    case "dictionary":
                        if (command.Length >= 3)
                        {
                            string[] locationArray = new string[command.Length - 2]; // nm location1 location2 -> need (length - 1)
                            Array.Copy(command, 2, locationArray, 0, locationArray.Length); // source array, source start index, target array, target start indext, count
                            

                        }

                        break;

                    // Naver Map Module
                    case "nm":
                    case "nmap":
                        if (command.Length >= 2)
                        {
                            string[] locationArray = new string[command.Length - 1]; // nm location1 location2 -> need (length - 1)
                            Array.Copy(command, 1, locationArray, 0, locationArray.Length); // source array, source start index, target array, target start indext, count

                            // Use Go Module
                            Go go = new Go();
                            go.Call_NaverMap(locationArray);

                            Hide_MainWindow();
                        }

                        break;

                    // Google Map Module
                    case "gm":
                    case "gmap":
                    case "map":
                        if (command.Length >= 2)
                        {
                            string[] locationArray = new string[command.Length - 1]; // nm location1 location2 -> need (length - 1)
                            Array.Copy(command, 1, locationArray, 0, locationArray.Length); // source array, source start index, target array, target start indext, count

                            // Use Go Module
                            Go go = new Go();
                            go.Call_GoogleMap(locationArray);

                            Hide_MainWindow();
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

        private void Display_MainWindow()
        {
            Visibility = Visibility.Visible;
            isTurn = true;

            // Focus on TextBox when it turns on
            CommandBox.Focus();
        }

        private void Hide_MainWindow()
        {
            // Clear input box
            CommandBox.Text = string.Empty;

            Visibility = Visibility.Hidden;
            isTurn = false;
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
