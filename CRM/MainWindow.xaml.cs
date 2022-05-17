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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CRM
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection connection;
        string name = "";
        string id = "";
        string position = "";

        public MainWindow()
        {
            InitializeComponent();
            contentControl.Content = new CRM.Pages.Subscribers();
            string conStr = @"Data Source=.\SQLEXPRESS; Initial Catalog=Database_Telekom_Neva_Svyaz;Integrated Security=True;";
            connection = new SqlConnection(conStr);

            connection.Open();
            SqlCommand query = new SqlCommand("SELECT * FROM Sotrudnik");
            query.Connection = connection;

            SqlDataReader reader = query.ExecuteReader();
            while (reader.Read())
            {
                int nameIndex = reader.GetOrdinal("ФИО");
                name = reader.GetString(nameIndex);

                UsersComboBox.Items.Add(name);
            }

            reader.Close();
            connection.Close();
        }

        private void verticalMenu_MouseLeave(object sender, MouseEventArgs e)
        {
            menuOpen.Visibility = Visibility.Hidden;
            menuFold.Visibility = Visibility.Visible;
            menuBorder.Width = 45;

            btnProf.Width = 25;
            btnProf.Height = 25;
            btnProf.FontSize = 8;

            btnHelp.Width = 25;
            btnHelp.Height = 25;
            btnHelp.FontSize = 14;
        }

        private void verticalMenu_MouseEnter(object sender, MouseEventArgs e)
        {
            menuOpen.Visibility = Visibility.Visible;
            menuFold.Visibility = Visibility.Hidden;
            menuBorder.Width = 100;

            btnProf.Width = 35;
            btnProf.Height = 35;
            btnProf.FontSize = 12;

            btnHelp.Width = 35;
            btnHelp.Height = 35;
            btnHelp.FontSize = 20;
        }

        private void UsersComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                connection.Open();
                SqlCommand query = new SqlCommand("SELECT * FROM Sotrudnik WHERE ФИО=@name", connection);

                query.Parameters.AddWithValue("@name", UsersComboBox.SelectedItem);

                SqlDataReader reader = query.ExecuteReader();
                while (reader.Read())
                {
                    int idIndex = reader.GetOrdinal("ID");
                    id = reader.GetString(idIndex);

                    int positionIndex = reader.GetOrdinal("Должность");
                    position = reader.GetString(positionIndex);
                }
                imgPhoto.ImageSource = new BitmapImage(new Uri($"pack://application:,,,/Images/Users/{id}.png", UriKind.RelativeOrAbsolute));

                if(position == "Руководитель отдела по работе с клиентами")
                {
                    btnAssets.Visibility = Visibility.Hidden;
                    btnBilling.Visibility = Visibility.Visible;
                    btnCRM.Visibility = Visibility.Visible;
                    btnEquipment.Visibility = Visibility.Hidden;
                    btnSubscribers.Visibility = Visibility.Visible;
                    btnSupport.Visibility = Visibility.Hidden;
                }
                else if(position == "Менеджер по работе с клиентами")
                {
                    btnAssets.Visibility = Visibility.Hidden;
                    btnBilling.Visibility = Visibility.Hidden;
                    btnCRM.Visibility = Visibility.Visible;
                    btnEquipment.Visibility = Visibility.Hidden;
                    btnSubscribers.Visibility = Visibility.Visible;
                    btnSupport.Visibility = Visibility.Hidden;
                }
                else if (position == "Руководитель отдела технической поддержки")
                {
                    btnAssets.Visibility = Visibility.Hidden;
                    btnBilling.Visibility = Visibility.Hidden;
                    btnCRM.Visibility = Visibility.Visible;
                    btnEquipment.Visibility = Visibility.Visible;
                    btnSubscribers.Visibility = Visibility.Visible;
                    btnSupport.Visibility = Visibility.Visible;
                }
                else if (position == "Специалист технической поддержки ")
                {
                    btnAssets.Visibility = Visibility.Hidden;
                    btnBilling.Visibility = Visibility.Hidden;
                    btnCRM.Visibility = Visibility.Visible;
                    btnEquipment.Visibility = Visibility.Visible;
                    btnSubscribers.Visibility = Visibility.Visible;
                    btnSupport.Visibility = Visibility.Visible;
                }
                else if (position == "Бухгалтер")
                {
                    btnAssets.Visibility = Visibility.Hidden;
                    btnBilling.Visibility = Visibility.Visible;
                    btnCRM.Visibility = Visibility.Visible;
                    btnEquipment.Visibility = Visibility.Hidden;
                    btnSubscribers.Visibility = Visibility.Visible;
                    btnSupport.Visibility = Visibility.Hidden;
                }
                else if (position == "Директор по развитию")
                {
                    btnAssets.Visibility = Visibility.Hidden;
                    btnBilling.Visibility = Visibility.Hidden;
                    btnCRM.Visibility = Visibility.Visible;
                    btnEquipment.Visibility = Visibility.Hidden;
                    btnSubscribers.Visibility = Visibility.Visible;
                    btnSupport.Visibility = Visibility.Hidden;
                }
                else if (position == "Специалист ТП (выездной инженер)" || position == "Технический департамент")
                {
                    btnAssets.Visibility = Visibility.Hidden;
                    btnBilling.Visibility = Visibility.Hidden;
                    btnCRM.Visibility = Visibility.Visible;
                    btnEquipment.Visibility = Visibility.Hidden;
                    btnSubscribers.Visibility = Visibility.Visible;
                    btnSupport.Visibility = Visibility.Hidden;
                }
            }
            catch
            {
                imgPhoto.ImageSource = new BitmapImage(new Uri($"pack://application:,,,/Images/Users/noPhoto.png", UriKind.RelativeOrAbsolute));
            }
            finally
            {
                connection.Close();
            }
        }

        private void btnSubscribers_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = new CRM.Pages.Subscribers();
        }
    }
}
