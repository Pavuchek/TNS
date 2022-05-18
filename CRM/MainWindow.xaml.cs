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
        string ivents = "";
        string ivent = "";

        public Pages.CRM crm = new Pages.CRM();

        public MainWindow()
        {
            InitializeComponent();
            contentControl.Content = new CRM.Pages.Subscribers();
            string conStr = @"Data Source=DESKTOP-96QPEGU;Initial Catalog=Database_Telekom_Neva_Svyaz;Integrated Security=True";
            connection = new SqlConnection(conStr);

            connection.Open();
            SqlCommand query = new SqlCommand("SELECT * FROM Sotrudnik", connection);

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
                btnAssets.Visibility = Visibility.Hidden;
                btnBilling.Visibility = Visibility.Hidden;
                btnCRM.Visibility = Visibility.Hidden;
                btnEquipment.Visibility = Visibility.Hidden;
                btnSubscribers.Visibility = Visibility.Hidden;
                btnSupport.Visibility = Visibility.Hidden;

                btnAssets.Margin = new Thickness(0, 0, 0, 5);
                btnBilling.Margin = new Thickness(0, 0, 0, 5);
                btnCRM.Margin = new Thickness(0, 0, 0, 5);
                btnEquipment.Margin = new Thickness(0, 0, 0, 5);
                btnSubscribers.Margin = new Thickness(0, 0, 0, 5);
                btnSupport.Margin = new Thickness(0, 0, 0, 5);

                imgAssets.Visibility = Visibility.Hidden;
                imgBilling.Visibility = Visibility.Hidden;
                imgCRM.Visibility = Visibility.Hidden;
                imgEquipment.Visibility = Visibility.Hidden;
                imgSubscribers.Visibility = Visibility.Hidden;
                imgSupport.Visibility = Visibility.Hidden;

                textBlock1.Text = null;
                textBlock2.Text = null;
                textBlock3.Text = null;
                textBlock4.Text = null;
                textBlock5.Text = null;
                textBlock6.Text = null;

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
                reader.Close();


                SqlCommand query1 = new SqlCommand("SELECT * FROM InformDlyaSotrudnikov WHERE Должность=@position", connection);

                query1.Parameters.AddWithValue("@position", position);

                SqlDataReader reader1 = query1.ExecuteReader();

                while (reader1.Read())
                {
                    int iventIndex = reader1.GetOrdinal("События");
                    ivents = reader1.GetString(iventIndex);
                }
                reader1.Close();
                int startIvent = 0;
                int index = 1;

                for (int i = 0; i < ivents.Length; i++)
                {
                    if(ivents[i] == ',' || i == ivents.Length - 1)
                    {
                        int endIvent = i + 1;
                        ivent = "";

                        for(; startIvent < endIvent; startIvent++)
                        {
                            ivent += ivents[startIvent];
                        }

                        if(ivent.EndsWith(","))
                        {
                            ivent = ivent.Remove(ivent.Length - 1);
                        }

                        switch(index)
                        {
                            case 1:
                                textBlock1.Text = ivent;
                                break;

                            case 2:
                                textBlock2.Text = ivent;
                                break;

                            case 3:
                                textBlock3.Text = ivent;
                                break;

                            case 4:
                                textBlock4.Text = ivent;
                                break;

                            case 5:
                                textBlock5.Text = ivent;
                                break;

                            case 6:
                                textBlock6.Text = ivent;
                                break;

                            default:
                                MessageBox.Show("Вы привысили максимальное количество событий", "Ошибка");
                                break;
                        }

                        index++;

                        if (index == 6)
                        {
                            break;
                        }

                        startIvent = endIvent + 1;
                    }
                }



                if (position == "Руководитель по работе с клиентами")
                {
                    btnBilling.Visibility = Visibility.Visible;
                    btnCRM.Visibility = Visibility.Visible;
                    btnSubscribers.Visibility = Visibility.Visible;

                    imgBilling.Visibility = Visibility.Visible;
                    imgCRM.Visibility = Visibility.Visible;
                    imgSubscribers.Visibility = Visibility.Visible;

                    btnBilling.Margin = new Thickness(0, -125, 0, 0);
                    btnCRM.Margin = new Thickness(0, -125, 0, 0);

                    imgBilling.Margin = new Thickness(0, -175, 0, 0);
                    imgCRM.Margin = new Thickness(0, -175, 0, 0);
                }
                else if (position == "Менеджер по работе с клиентами")
                {
                    btnCRM.Visibility = Visibility.Visible;
                    btnSubscribers.Visibility = Visibility.Visible;

                    imgCRM.Visibility = Visibility.Visible;
                    imgSubscribers.Visibility = Visibility.Visible;

                    btnCRM.Margin = new Thickness(0, -290, 0, 0);

                    imgCRM.Margin = new Thickness(0, -375, 0, 0);
                }
                else if (position == "Руководитель отдела технической поддержки" || position == "Специалист технической поддержки")
                {
                    btnCRM.Visibility = Visibility.Visible;
                    btnEquipment.Visibility = Visibility.Visible;
                    btnSubscribers.Visibility = Visibility.Visible;
                    btnSupport.Visibility = Visibility.Visible;

                    imgCRM.Visibility = Visibility.Visible;
                    imgEquipment.Visibility = Visibility.Visible;
                    imgSubscribers.Visibility = Visibility.Visible;
                    imgSupport.Visibility = Visibility.Visible;

                    btnCRM.Margin = new Thickness(0, -45, 0, 0);
                    btnSupport.Margin = new Thickness(0, -125, 0, 0);

                    imgCRM.Margin = new Thickness(0, -65, 0, 0);
                    imgSupport.Margin = new Thickness(0, -160, 0, 0);
                }
                else if (position == "Бухгалтер")
                {
                    btnBilling.Visibility = Visibility.Visible;
                    btnCRM.Visibility = Visibility.Visible;
                    btnSubscribers.Visibility = Visibility.Visible;

                    imgBilling.Visibility = Visibility.Visible;
                    imgCRM.Visibility = Visibility.Visible;
                    imgSubscribers.Visibility = Visibility.Visible;

                    btnCRM.Margin = new Thickness(0, -125, 0, 0);
                    btnBilling.Margin = new Thickness(0, -125, 0, 0);

                    imgCRM.Margin = new Thickness(0, -175, 0, 0);
                    imgBilling.Margin = new Thickness(0, -175, 0, 0);
                }
                else if (position == "Директор по развитию")
                {
                    btnAssets.Visibility = Visibility.Visible;
                    btnBilling.Visibility = Visibility.Visible;
                    btnCRM.Visibility = Visibility.Visible;
                    btnEquipment.Visibility = Visibility.Visible;
                    btnSubscribers.Visibility = Visibility.Visible;
                    btnSupport.Visibility = Visibility.Visible;

                    imgAssets.Visibility = Visibility.Visible;
                    imgBilling.Visibility = Visibility.Visible;
                    imgCRM.Visibility = Visibility.Visible;
                    imgEquipment.Visibility = Visibility.Visible;
                    imgSubscribers.Visibility = Visibility.Visible;
                    imgSupport.Visibility = Visibility.Visible;
                }
                else if (position == "Специалист ТП (выездной инженер)" || position == "Технический департамент")
                {
                    btnAssets.Visibility = Visibility.Visible;
                    btnCRM.Visibility = Visibility.Visible;
                    btnEquipment.Visibility = Visibility.Visible;
                    btnSubscribers.Visibility = Visibility.Visible;

                    imgAssets.Visibility = Visibility.Visible;
                    imgCRM.Visibility = Visibility.Visible;
                    imgEquipment.Visibility = Visibility.Visible;
                    imgSubscribers.Visibility = Visibility.Visible;

                    btnCRM.Margin = new Thickness(0, -125, 0, 0);

                    imgCRM.Margin = new Thickness(0, -160, 0, 0);
                }

                imgPhoto.ImageSource = new BitmapImage(new Uri($"pack://application:,,,/Images/Users/{id}.png", UriKind.RelativeOrAbsolute));
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
            textblockName.Text = "Абоненты ТНС";
            contentControl.Content = new CRM.Pages.Subscribers();
            blockIvents.Visibility = Visibility.Visible;
            borderContent.Width = 500;
        }

        private void btnEquipment_Click(object sender, RoutedEventArgs e)
        {
            textblockName.Text = "Управление оборудованием";
            contentControl.Content = new CRM.Pages.Equipment();
            blockIvents.Visibility = Visibility.Hidden;
            borderContent.Width = 680;
        }

        private void btnAssets_Click(object sender, RoutedEventArgs e)
        {
            textblockName.Text = "Активы";
            contentControl.Content = new CRM.Pages.Assets();
            blockIvents.Visibility = Visibility.Hidden;
            borderContent.Width = 680;
        }

        private void btnBilling_Click(object sender, RoutedEventArgs e)
        {
            textblockName.Text = "Биллинг";
            contentControl.Content = new CRM.Pages.Billing();
            blockIvents.Visibility = Visibility.Hidden;
            borderContent.Width = 680;
        }

        private void btnSupport_Click(object sender, RoutedEventArgs e)
        {
            textblockName.Text = "Поддержка пользователей";
            contentControl.Content = new CRM.Pages.Support();
            blockIvents.Visibility = Visibility.Hidden;
            borderContent.Width = 680;
        }

        private void btnCRM_Click(object sender, RoutedEventArgs e)
        {
            textblockName.Text = "CRM";
            contentControl.Content = crm;
            blockIvents.Visibility = Visibility.Hidden;
            borderContent.Width = 680;
        }
    }
}
