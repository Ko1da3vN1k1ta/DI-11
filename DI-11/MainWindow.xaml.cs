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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DI_11
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        private string connectionString = @"Data Source=DESKTOP-H3UE754; Initial Catalog=StockControl; Integrated Security=True";
        public MainWindow()
        {
            
            InitializeComponent();
           
        }



        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand sql = new SqlCommand("select Id from Users where Login = '" + LoginTextBox.Text + "' and Password = '" + PasswordBox.Text + "';", conn); 
                SqlDataReader reader = sql.ExecuteReader();
                if (reader.Read())
                {
                    var id = reader[0].ToString(); reader.Close();
                    MainMenu mainMenu = new MainMenu();
                    mainMenu.Show();
                    this.Close();

                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль");
                }
                reader.Close();
            }
        }
                
            private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string password = PasswordBox.Text;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand checkUserCmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Login = @login", conn);
                    checkUserCmd.Parameters.AddWithValue("@login", login);

                    int userCount = (int)checkUserCmd.ExecuteScalar();

                    if (userCount > 0)
                    {
                        MessageBox.Show("Пользователь с таким логином уже существует!");
                    }
                    else
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO Users ( Login, Password) VALUES (@login, @password)", conn);
                        cmd.Parameters.AddWithValue("@login", login);
                        cmd.Parameters.AddWithValue("@password", password);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Регистрация прошла успешно!");
                           
                        }
                        else
                        {
                            MessageBox.Show("Регистрация не удалась. Повторите попытку.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при выполнении запроса: {ex.Message}");
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        
    }
}