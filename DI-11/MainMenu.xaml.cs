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
    /// <summary>
    /// Логика взаимодействия для MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        private string connectionString = @"Data Source=DESKTOP-H3UE754; Initial Catalog=StockControl; Integrated Security=True";

        public MainMenu()
        {
            
            InitializeComponent();
           
            calculateCount();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Stocks st = new Stocks();
            st.Show();
            this.Close();
        }

        private void calculateCount() {
            using (SqlConnection conn = new SqlConnection(connectionString)){
                conn.Open();
                SqlCommand sql = new SqlCommand("select COUNT(*) from Products", conn);
                SqlDataReader reader = sql.ExecuteReader();
                if (reader.Read())
                {
                    CountProducts.Text = reader[0].ToString();
                    conn.Close();
                }
                else
                {
                    CountProducts.Text = "0";
                    conn.Close();
                }
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand sql = new SqlCommand("select COUNT(*) from Products where typeProduct = 1", conn);
                SqlDataReader reader = sql.ExecuteReader();
                if (reader.Read())
                {
                    CountProductsIncome.Text = reader[0].ToString();
                    conn.Close();

                }
                else
                {
                    CountProducts.Text = "0";
                    conn.Close();
                }
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand sql = new SqlCommand("select COUNT(*) from Products where typeProduct = 2", conn);
                SqlDataReader reader = sql.ExecuteReader();
                if (reader.Read())
                {
                    CountProductsOutcome.Text = reader[0].ToString();
                    conn.Close();

                }
                else
                {
                    CountProducts.Text = "0";
                    conn.Close();
                }
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow= new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
