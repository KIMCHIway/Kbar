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
        public CommandWindow()
        {
            InitializeComponent();
        }


        public void Write_RelatedCommand(Dictionary<string, string> command) // Search Module name
        {
            // Initialize all TextBlock at every input
            for (int i = 0; i < 5; i++)
            {
                TextBlock name = FindName("NameIndex" + i) as TextBlock;
                name.Text = "";
                TextBlock description = FindName("DescriptionIndex" + i) as TextBlock;
                description.Text = "";
            }


            // Write on component
            int index = 0; // TextBlock index variable
            foreach (var value in command)
            {
                TextBlock name = FindName("NameIndex" + index) as TextBlock;
                name.Text = value.Key;
                TextBlock description = FindName("DescriptionIndex" + index) as TextBlock;
                description.Text = value.Value;

                index++;
                if (index == 5) break; // only show 5 search result (0 ~ 4)
            }
        }
    }
}
