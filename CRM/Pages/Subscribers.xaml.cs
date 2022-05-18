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
        DataTable dt = new DataTable("Абоненты ТНС");

        public Subscribers()
        {
            string conStr = @"Data Source=DESKTOP-96QPEGU;Initial Catalog=Database_Telekom_Neva_Svyaz;Integrated Security=True";
            connection = new SqlConnection(conStr);
            InitializeComponent();
        }

        private void Grid_Initialized(object sender, EventArgs e)
        {
            listSubscribers.Items.Clear();
            connection.Open();
            SqlCommand query = new SqlCommand("SELECT Номер_абонента as 'Номер абонента', ФИО, Номер_договора as 'Номер договора', (Услуги + ', ' + Услуги1 + ', ' + Услуги2) as Услуги, Лицевой_счет as 'Лицевой счет', Дата_расторжения_договора as 'Дата расторжения договора' FROM Abonenti", connection);

            query.ExecuteNonQuery();

            dataAdp = new SqlDataAdapter(query);
            dataAdp.Fill(dt);

            DataView dv = new DataView(dt);
            dv.RowFilter = $"'Дата расторжения договора' = ''";
            listSubscribers.ItemsSource = dt.DefaultView;

            connection.Close();
        }
    }
}
