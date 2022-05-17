using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;

namespace CRM.Pages
{
    /// <summary>
    /// Логика взаимодействия для Subscribers.xaml
    /// </summary>
    public partial class Subscribers : UserControl
    {
        SqlConnection connection;
        SqlDataAdapter dataAdp;

        public Subscribers()
        {
            InitializeComponent();
            string conStr = @"Data Source=.\SQLEXPRESS; Initial Catalog=Database_Telekom_Neva_Svyaz;Integrated Security=True;";
            connection = new SqlConnection(conStr);
            connection.Open();
            SqlCommand query = new SqlCommand("SELECT Номер_абонента, ФИО, Номер_договора, Услуги, Услуги1, Услуги2 Лицевой_счет FROM Abonenti", connection);

            query.ExecuteNonQuery();

            dataAdp = new SqlDataAdapter(query);
            DataTable dt = new DataTable("Абоненты ТНС");
            dataAdp.Fill(dt);
            listSubscribers.ItemsSource = dt.DefaultView;

            connection.Close();
        }
    }
}
