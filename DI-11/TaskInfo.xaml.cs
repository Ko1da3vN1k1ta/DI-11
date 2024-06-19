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
    /// Логика взаимодействия для TaskInfo.xaml
    /// </summary>
    public partial class TaskInfo : Window
    {
        public TaskInfo(string taskName, string taskInfo)
        {
            InitializeComponent();
            TaskName.Text = taskName;
            TaskInformation.Text = taskInfo;
        }
    }
}
