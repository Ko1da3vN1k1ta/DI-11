using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

    public partial class Stocks : Window
    {
        bool isSelected = false;
        List<DoElement> ListElements = new List<DoElement>();
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-H3UE754; Initial Catalog=StockControl; Integrated Security=True");
        public Stocks()
        {

            InitializeComponent();
            conn.Open();
            SqlCommand cmd = new SqlCommand("select nameProduct, description, typeProduct, id from Products", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                DoElement doEl = new DoElement(reader[0].ToString(), reader[1].ToString(), this, Int32.Parse(reader[2].ToString()), Int32.Parse(reader[3].ToString()));
                ListElements.Add(doEl);
            }
            reader.Close();
            updateList();
        }

        //Метод распределяет задачи по их типам
        public void updateList()
        {
            DoList.Items.Clear();
            ProcessList.Items.Clear();
            ReadyList.Items.Clear();
            InventoryList.Items.Clear();

            for (int i = 0; i < ListElements.Count(); i++)
            {
                if (ListElements[i].typeTask == 0)
                {
                    DoList.Items.Add(setToFrame(ListElements[i]));
                }
                else if (ListElements[i].typeTask == 1)
                {
                    ProcessList.Items.Add(setToFrame(ListElements[i]));
                }
                else if (ListElements[i].typeTask == 2)
                {
                    ReadyList.Items.Add(setToFrame(ListElements[i]));
                }
                else if (ListElements[i].typeTask == 3)
                {
                    InventoryList.Items.Add(setToFrame(ListElements[i]));
                }
            }
        }

        //Метод который вставляет page в Frame чтобы вставить page в ListBox
        private Frame setToFrame(DoElement element)
        {
            Frame frame = new Frame();
            frame.Content = element;
            frame.Width = 333;
            return frame;
        }

        private void DoList_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void DoList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DoList_Selected(object sender, RoutedEventArgs e)
        {

        }

        //Метод обработки клика на кнопку добавить задачу
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddTask adWindow = new AddTask(this);
            adWindow.Show();
        }

        //Метод добавления задачи принимающий имя и описание задачи, так же метод добавляет задачу в бд
        public void addTask(string nameTask, string taskInfo)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO Products (nameProduct, description, typeProduct, UserId) VALUES (@nameTask, @description, @typeTask, @UserId); SELECT SCOPE_IDENTITY();", conn);
            cmd.Parameters.AddWithValue("@nameTask", nameTask);
            cmd.Parameters.AddWithValue("@description", taskInfo);
            cmd.Parameters.AddWithValue("@typeTask", 0);
            cmd.Parameters.AddWithValue("@UserId", 1);

            // Execute the command and retrieve the new ID
            int newTaskId = Convert.ToInt32(cmd.ExecuteScalar());

            // Create the new DoElement and add it to the list
            DoElement doEl = new DoElement(nameTask, taskInfo, this, 0, newTaskId);
            ListElements.Add(doEl);

            // Update the UI list
            updateList();
        }

        //Метод вызывается при перемещении задачи в разные столбцы тем самым обновляя её статус
        public void updateTaskType(int type, int id)
        {
            SqlCommand cmd = new SqlCommand($"UPDATE Products set typeProduct = {type} where id = {id}", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Close();

        }
        //Метод удаления задачи
        public void deleteTask(int id)
        {
            for (int i = 0; i <= ListElements.Count(); i++) {
                if (ListElements.ElementAt(i).index == id) {
                    ListElements.Remove(ListElements.ElementAt(i));
                    SqlCommand cmd = new SqlCommand($"DELETE FROM Products where id = {id}", conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Close();
                    break;
                }
            }
            updateList();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainMenu mm = new MainMenu();
            mm.Show();
            this.Close();
        }
    }
}

