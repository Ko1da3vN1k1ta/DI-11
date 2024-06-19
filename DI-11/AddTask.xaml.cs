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

namespace DI_11
{
    /// <summary> 
    /// Interaction logic for AddTask.xaml 
    /// </summary> 
    public partial class AddTask : Window
    {
        Stocks st;
        public AddTask(Stocks st)
        {
            InitializeComponent();
            this.st = st;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            st.addTask(NameBox.Text, InfoBox.Text);
            this.Close();
        }
    }
}