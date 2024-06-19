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
    /// Логика взаимодействия для DoElement.xaml
    /// </summary>
    public partial class DoElement : Page
    {
        public string taskInfo;
        public int typeTask = 0;
        Stocks st;
        public int index;
        public DoElement(string task, string taskInfo, Stocks st, int type, int index)
        {
            InitializeComponent();
            NameTask.Text = task;
            this.taskInfo = taskInfo;
            this.st = st;
            this.index = index;
            this.typeTask = type;
            updateButton();
        }
        private void updateButton()
        {
            if (typeTask != 0)
            {
                BackButton.Visibility = Visibility.Visible;
            }
            else
            {
                BackButton.Visibility = Visibility.Hidden;
            }
            if (typeTask == 3)
            {
                NextButton.Visibility = Visibility.Hidden;
            }
            else NextButton.Visibility = Visibility.Visible;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!(typeTask >= 3))
            {
                typeTask++;
                updateButton();
                updateTaskType();
                st.updateList();
            }
        }
        public void showInfo()
        {
            TaskInfo info = new TaskInfo(NameTask.Text, taskInfo);
            info.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            showInfo();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (typeTask >= 0)
            {
                typeTask--;
                updateTaskType();
                updateButton();
                st.updateList();
            }
        }

        private void updateTaskType()
        {
            st.updateTaskType(typeTask, index);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            st.deleteTask(index);
        }
    }
}

