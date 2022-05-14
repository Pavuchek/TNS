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
using Assets.Pages;

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
        string name = "";
        string roleText = "";

        int role = 0;
        int timerTicked = 1;
        int timerToTick = 10;

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

                        int roleIndex = reader.GetOrdinal("Роль");
                        role = reader.GetInt32(roleIndex);

                        int nameIndex = reader.GetOrdinal("Имя");
                        name = reader.GetString(nameIndex);
                    }
                    reader.Close();

                    if (textboxNumber.Text == number)
                    {
                        textboxPassword.IsEnabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Данный пользователь не найден", "Ошибка");
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
                        textboxCode.IsEnabled = true;
                        Code page = new Code();
                        page.timer = Convert.ToString(timerToTick);
                        page.code = Code();
                        page.Show();
                        page.Closing += Method;
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
                switch(role)
                {
                    case 1:
                        roleText = "Руководитель отдела по работе с клиентами";
                        break;
                    case 2:
                        roleText = "Менеджер по работе с клиентами";
                        break;
                    case 3:
                        roleText = "Руководитель отдела технической поддержки";
                        break;
                    case 4:
                        roleText = "Специалист технической поддержки";
                        break;
                    case 5:
                        roleText = "Бухгалтер";
                        break;
                    case 6:
                        roleText = "Директор по развитию";
                        break;
                    case 7:
                        roleText = "Сотрудник технического департамента";
                        break;
                }
                MessageBox.Show($"{roleText}", $"Добро пожаловать, {name}");
            }
            else
            {
                MessageBox.Show("Вы ввели неверный код! Повторите попытку позже","Ошибка");
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

        private void Code_Tick(object sender, EventArgs e)
        {
            timerTicked++;
            Code page = new Code();
            page.timer = Convert.ToString(timerToTick - timerTicked);
            if(timerTicked >= timerToTick)
            {
                timer.Stop();
                code = Code();
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

        private void imgRefresh_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            code = "";
            timerTicked = 1;
            Code page = new Code();
            page.timer = Convert.ToString(timerToTick);
            page.code = Code();
            page.Show();
            page.Closing += Method;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            code = "";
            textboxNumber.Text = "";
            textboxPassword.Text = "";
            textboxCode.Text = "";
        }

        private void Method(object sender, System.ComponentModel.CancelEventArgs e)
        {
            timer = new DispatcherTimer();
            timer.Tick += Code_Tick;
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
            Environment.Exit(0);
        }

        private void textboxCode_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    if (textboxCode.Text == code)
                    {
                        switch (role)
                        {
                            case 1:
                                roleText = "Руководитель отдела по работе с клиентами";
                                break;
                            case 2:
                                roleText = "Менеджер по работе с клиентами";
                                break;
                            case 3:
                                roleText = "Руководитель отдела технической поддержки";
                                break;
                            case 4:
                                roleText = "Специалист технической поддержки";
                                break;
                            case 5:
                                roleText = "Бухгалтер";
                                break;
                            case 6:
                                roleText = "Директор по развитию";
                                break;
                            case 7:
                                roleText = "Сотрудник технического департамента";
                                break;
                        }
                        MessageBox.Show($"{roleText}", $"Добро пожаловать, {name}");
                    }
                    else
                    {
                        MessageBox.Show("Вы ввели неверный код! Повторите попытку позже", "Ошибка");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }
    }
}