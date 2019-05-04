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
using Microsoft.Win32;
using MessageBox = System.Windows.Forms.MessageBox;

// Kbar command docs : https://docs.google.com/document/d/1-0STF1GnlQi-IYj8PNtPBaO657w1jOwlGT1H6X81nZw/edit?usp=sharing
// Code of GUI MainWindows.xaml 

// MessageBox.Show("Y", "Sys");

namespace Kbar.Net
{
	
    public partial class MainWindow : Window
    {
        RegistryKey runRegKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        NotifyIcon ni = new NotifyIcon();
        LowKeyListener _listener;

        // To delete the sub window which is turn on
        private dynamic cur_SubWindow;
        private dynamic cur_CommandWindow;
        private dynamic cur_EdgeWindow;
        // To seperate what to do when F8 is pressed
        private bool isTurn;
        // To check if focus is on inputBox on Main window
        public bool isFocus = true;

        public MainWindow()
        {
            InitializeComponent();
			
			// Code for hook key
            _listener = new LowKeyListener();
            _listener.OnKeyPressed += _listener_OnKeyPressed;

            _listener.HookKeyboard();

            // Set tray feature
            try
            {
                System.Windows.Forms.ContextMenu menu = new System.Windows.Forms.ContextMenu();

                System.Windows.Forms.MenuItem item1 = new System.Windows.Forms.MenuItem();
                System.Windows.Forms.MenuItem item2 = new System.Windows.Forms.MenuItem();
                System.Windows.Forms.MenuItem item3 = new System.Windows.Forms.MenuItem();
                menu.MenuItems.Add(item1);
                menu.MenuItems.Add(item2);
                menu.MenuItems.Add(item3);

                item1.Index = 0;
                item1.Text = "Help";
                item1.Click += delegate (object click, EventArgs eClick)
                {
                    HelpWindow helpWindow = new HelpWindow(this);
                    Load_EdgeWindow(helpWindow);
                };

                item2.Index = 1;
                item2.Text = "Register as StartProcess";
                // Check whether the program register
                if (runRegKey.GetValue("Kbar.Net") == null)
                {
                    item2.Checked = false;
                }
                else
                {
                    item2.Checked = true;
                }
                item2.Click += delegate (object click, EventArgs eClick)
                {
                    if (item2.Checked == true)
                    {
                        item2.Checked = false;
                        runRegKey.DeleteValue("Kbar.Net", false);
                    }
                    else
                    {
                        item2.Checked = true;
                        runRegKey.SetValue("Kbar.Net", Environment.CurrentDirectory + "\\" + AppDomain.CurrentDomain.FriendlyName);
                    }
                };
                
                item3.Index = 2;
                item3.Text = "Exit";
                item3.Click += delegate (object click, EventArgs eClick)
                {
                    System.Windows.Application.Current.Shutdown();
                };

                //ni.Icon = new System.Drawing.Icon("./SampleIcon.ico");
                ni.Icon = Properties.Resources.SampleIcon;
                ni.Visible = true;
                ni.DoubleClick += delegate (object senders, EventArgs args)
                {
                    Display_MainWindow();
                };
                ni.ContextMenu = menu;
                ni.Text = "Kbar.Net";
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\r Report with Error code in https://github.com/KIMCHIway/Kbar", "101 ERROR CODE");
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            ShowInTaskbar = false;
            Topmost = true;
            InputBox.Focus();

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
                    ClosingMethod();
                }
                else
                {
                    Display_MainWindow();

                    // Focus on TextBox when it turns on
                    InputBox.Focus();
                }
            }

            if (e.KeyPressed.ToString() == "Escape" && isTurn == true)
            {
                ClosingMethod();
            }


            // Hotkey for command
            if (e.KeyPressed.ToString() == "Return" && isTurn == true && isFocus == true)
            {
                CommandMethod();
            }
        }

        private void InputBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            isFocus = true;
        }

        private void InputBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Close sub form which is showed
            if (cur_SubWindow != null) Close_SubWindow();


            string[] command = InputBox.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (command.Length == 1) // Input module name
            {
                CommandSuggestion commandSuggestion = new CommandSuggestion();

                var result = commandSuggestion.Search_Command(command[0]);

                Load_CommandWindow(result);
            }
            else if (command.Length > 1 ) // input module command
            {
                CommandSuggestion commandSuggestion = new CommandSuggestion();

                int index = command.Length - 1; // index = length - 1
                var result = commandSuggestion.Search_Command(command[0], command[index]);

                Load_CommandWindow(result);
            }
            else if (command.Length > 3) // Max count of Command is 3, No need to Search for user input
            {
                return;
            }
            
        }

        private void Load_CommandWindow(dynamic result)
        {
            if (result != null)
            {
                if (cur_CommandWindow == null)
                {
                    CommandWindow commandWindow = new CommandWindow();
                    Load_SecondWindow(commandWindow);

                    cur_CommandWindow = commandWindow;
                }

                cur_CommandWindow.Write_RelatedCommand(result);

                Activate();
            }
        }
		
		private void CommandMethod()
		{
            // Close Second Window which is showed
            Close_SubWindow();
            Close_CommandWindow();

            // Split by blank (0 is Command)
            // Don't do ToLower() here for user input (Not command)
            string[] command = InputBox.Text.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);

            string module = command[0];
            switch (module.ToLower())
            {
                // Go Module
                case "go":
                    if (command.Length >= 2)
                    {
                        Go go = new Go();
                        go.Call_Go(command[1]);

                        ClosingMethod();
                    }

                    break;

                // Papago Module
                case "papago":
                case "nt":
                case "ntranslator":
                case "translator":


                    if (command.Length >= 4)
                    {
                        // Combine seperated source text (INDEX 0:papago 1:en 2:ko 3:text1 4:text2 ~
                        string sourceText = string.Empty;
                        for (int i = 3; i < command.Length; i++)
                        {
                            sourceText += command[i] + " ";
                        }

                        // Call Papago API
                        Papago papago = new Papago();
                        string targetText = papago.Call_Papago(command[1], command[2], sourceText);


                        // Set window component
                        PapagoWindow papagoWindow = new PapagoWindow(this);
                        Load_SecondWindow(papagoWindow);

                        cur_SubWindow = papagoWindow;

                        Activate();

                        papagoWindow.Text_sCode.Text = command[1];
                        papagoWindow.Text_tCode.Text = command[2];
                        papagoWindow.Text_sText.Text = sourceText;
                        papagoWindow.Text_tText.Text = targetText;
                    }

                    break;

                // Naver Dictionary Module
                case "nd":
                case "ndictionary":
                case "dictionary":
                    if (command.Length >= 3)
                    {
                        string[] textArray = new string[command.Length - 2]; // nd code text1 text2 -> need (length - 2)
                        Array.Copy(command, 2, textArray, 0, textArray.Length); // source array, source start index, target array, target start indext, count

                        // Use Go Module
                        Go go = new Go();
                        go.Call_NaverDictionary(command[1], textArray);

                        ClosingMethod();
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

                        ClosingMethod();
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

                        ClosingMethod();
                    }

                    break;
                // Calculator Module
                case "calc":
                case "calculator":
                    if (command.Length >= 2)
                    {
                        // Combine seperated formula
                        string formula = string.Empty;
                        for (int i = 1; i < command.Length; i++)
                        {
                            formula += command[i];
                        }

                        Calculator clac = new Calculator();
                        string result = clac.clac(formula);

                        CalculatorWindow calcWindow = new CalculatorWindow(this);
                        Load_SecondWindow(calcWindow);

                        cur_SubWindow = calcWindow;

                        Activate();

                        calcWindow.Text_Formula.Text = formula;
                        calcWindow.Text_Result.Text = result;
                    }
                    break;
                // Notepad Module
                case "notepad":
                case "note":

                    NotepadWindow noteWindow = new NotepadWindow(this);
                    Load_EdgeWindow(noteWindow);

                    ClosingMethod();

                    break;
            }
        }

        private void Display_MainWindow()
        {
            Visibility = Visibility.Visible;
            isTurn = true;
        }

        private void ClosingMethod()
        {
            Hide_MainWindow();

            Close_SubWindow();
            Close_CommandWindow();
        }

        private void Hide_MainWindow()
        {
            // Clear input box
            InputBox.Text = string.Empty;

            Visibility = Visibility.Hidden;
            isTurn = false;
        }

        public void Close_CommandWindow()
        {
            if (cur_CommandWindow != null)
            {
                cur_CommandWindow.Close();
                cur_CommandWindow = null;
            }
        }

        public void Close_SubWindow()
        {
            if (cur_SubWindow != null)
            {
                cur_SubWindow.Close();
                cur_SubWindow = null;
            }
        }

        public void Close_EdgeWindow()
        {
            if (cur_EdgeWindow != null)
            {
                cur_EdgeWindow.Close();
                cur_EdgeWindow = null;
            }
        }

        private void Load_SecondWindow(dynamic window)
        {
            window.Show();
            window.Left = Left;
            window.Top = Top + Height / 3 - 21;
        }

        private void Load_EdgeWindow(dynamic window)
        {
            Close_EdgeWindow();

            window.Show();

            var desktopWorkingArea = SystemParameters.WorkArea;
            window.Left = desktopWorkingArea.Right - window.Width;
            window.Top = desktopWorkingArea.Bottom - window.Height;

            cur_EdgeWindow = window;
        }

        //private void Grid_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        //{
        //    _listener.UnHookKeyboard();
        //}
    }
}
