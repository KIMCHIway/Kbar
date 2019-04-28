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
    /// CommandWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CommandWindow : Window
    {
        CommandList command = new CommandList();

        public CommandWindow()
        {
            InitializeComponent();
        }


        public bool Load_RelatedCommand(string text) // Search Module name
        {
            // Initialize all TextBlock at every input
            for (int count = 0; count < 5; count++)
            {
                TextBlock name = FindName("NameIndex" + count) as TextBlock;
                name.Text = "";
                TextBlock description = FindName("DescriptionIndex" + count) as TextBlock;
                description.Text = "";
            }

            // Search
            var matchingValue = from module in command.modules
                                where module.Key.StartsWith(text)
                                select module;


            if (matchingValue.Count() > 0)
            {
                int count = 0; // TextBlock index variable
                foreach (var value in matchingValue)
                {
                    TextBlock name = FindName("NameIndex" + count) as TextBlock;
                    name.Text = value.Key;
                    TextBlock description = FindName("DescriptionIndex" + count) as TextBlock;
                    description.Text = value.Value;

                    count++;
                    if (count == 5) break; // only show 5 search result (0 ~ 4)
                }

                return true;
            }
            else // No Search result
            {
                return false;
            }
        }

        public bool Load_RelatedCommand(string module, string text) // Search command
        {
            // Initialize all TextBlock at every input
            for (int count = 0; count < 5; count++)
            {
                TextBlock name = FindName("NameIndex" + count) as TextBlock;
                name.Text = "";
                TextBlock description = FindName("DescriptionIndex" + count) as TextBlock;
                description.Text = "";
            }

            // Search
            Dictionary<string, string> matchingValue = null;
            switch (module)
            {
                // Papago Module
                case "papago":
                case "nt":
                case "ntranslation":
                case "translation":
                    matchingValue = (from command in command.papago
                                    where command.Key.StartsWith(text)
                                    select command).ToDictionary(i => i.Key, i => i.Value);
                    break;

                // Naver Dictionary Module
                case "nd":
                case "ndictionary":
                case "dictionary":
                    matchingValue = (from command in command.naverDictionary
                                    where command.Key.StartsWith(text)
                                    select command).ToDictionary(i => i.Key, i => i.Value);
                    break;
            }

            // Only (count of search result > 0)
            if (matchingValue.Count() > 0)
            {
                int count = 0; // TextBlock index variable
                foreach (var value in matchingValue)
                {
                    TextBlock name = FindName("NameIndex" + count) as TextBlock;
                    name.Text = value.Key;
                    TextBlock description = FindName("DescriptionIndex" + count) as TextBlock;
                    description.Text = value.Value;

                    count++;
                    if (count == 5) break; // only show 5 search result (0 ~ 4)
                }

                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
