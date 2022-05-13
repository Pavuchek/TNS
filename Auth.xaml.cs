using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SqlClient;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Threading;
using System.Windows.Threading;

namespace Assets
{
    /// <summary>
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class Auth : Window
    {
        SqlConnection connection;
        DispatcherTimer timer;

        string number = "";
        string password = "";
        string code = "";

        public Auth()
        {
            string conStr = @"Data Source=.\SQLEXPRESS; Initial Catalog=Database;Integrated Security=True;";
            connection = new SqlConnection(conStr);
            InitializeComponent();
        }

        private void textboxNumber_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    connection.Open();
                    SqlCommand query = new SqlCommand("SELECT * FROM Пользователи WHERE Номер=@number", connection);

                    query.Parameters.AddWithValue("@number", textboxNumber.Text);

                    SqlDataReader reader = query.ExecuteReader();
                    while (reader.Read())
                    {
                        int numberIndex = reader.GetOrdinal("Номер");
                        number = reader.GetString(numberIndex);

                        int passwordIndex = reader.GetOrdinal("Пароль");
                        password = reader.GetString(passwordIndex);
                    }
                    reader.Close();

                    if (textboxNumber.Text == number)
                    {
                        textboxPassword.IsEnabled = true;
                        textboxCode.IsEnabled = true;
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
            finally
            {
                connection.Close();
            }
        }

        private void textboxPassword_KeyUp(object sender, KeyEventArgs e)

        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    if (textboxPassword.Text == password)
                    {
                        new Thread(() => MessageBox.Show("У вас 10 секунд!\n" + Code(), "Код доступа")).Start();
                        timer = new DispatcherTimer();
                        timer.Tick += new EventHandler(Code());
                    }
                    else
                    {
                        MessageBox.Show("Вы ввели неверный пароль! Повторите попытку", "Ошибка");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            if(textboxCode.Text == code)
            {
                MessageBox.Show("Вы внутри!", "Вход");
            }
        }

        private void textboxCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textboxCode.Text != "")
            {
                btnEnter.IsEnabled = true;
            }
            else
            {
                btnEnter.IsEnabled = false;
            }
        }

        public string Code()
        {
            int[] arr = new int[8];
            Random rnd = new Random();

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = rnd.Next(33, 125);
                code += (char)arr[i];
            }

            return code;
        }


    }
}
