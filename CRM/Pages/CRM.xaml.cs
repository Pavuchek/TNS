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

namespace CRM.Pages
{
    /// <summary>
    /// Логика взаимодействия для CRM.xaml
    /// </summary>
    public partial class CRM : UserControl
    {
        SqlConnection connection;

        List<string> services = new List<string>() { "Интернет", "Мобильная связь", "Телевидение", "Видеонаблюдение" };
        List<string> viewServices = new List<string>() { "Подключение", "Управление договором/контактными данными", "Управление тарифом/услугой", "Диагностика и настройка оборудования/подключения", "Оплата услуг" };
        List<string> connectType = new List<string>() { "Подключение услуг с новой инфраструктурой", "Подключение услуг на существующей инфраструктуре" };
        List<string> treatyType = new List<string>() { "Изменение условий договора", "Включение в договор дополнительной услуги", "Изменение контактных данных" };
        List<string> rateType = new List<string>() { "Изменение тарифа", "Изменение адреса предоставления услуг", "Отключение услуги", "Приостановка предоставления услуги" };
        List<string> diagnosticType = new List<string>() { "Нет доступа к услуге", "Разрыв соединения", "Низкая скорость соединения" };
        List<string> paymentType = new List<string>() { "Выписка по платежам", "Информация о платежах", "Получение квитанции на оплату услуги" };
        List<string> status = new List<string> { "Новая", "Требует выезда", "Закрыта" };

        public int stat
        {
            set
            { 
                comboboxStatus.SelectedIndex = value;
            }
        }

        public CRM()
        {
            InitializeComponent();

            string conStr = @"Data Source=DESKTOP-96QPEGU;Initial Catalog=Database_Telekom_Neva_Svyaz;Integrated Security=True";
            connection = new SqlConnection(conStr);
        }

        private void Grid_Initialized(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now;
            textboxDate.Text = date.ToString("d");

            foreach(string s in services)
            {
                comboboxServices.Items.Add(s);
            }

            foreach(string v in viewServices)
            {
                comboboxView.Items.Add(v);
            }

            foreach(string s in status)
            {
                comboboxStatus.Items.Add(s);
            }

            comboboxStatus.SelectedIndex = 0;
        }

        private void comboboxView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            comboboxType.Items.Clear();
            comboboxType.IsEnabled = true;
            if(comboboxView.SelectedItem.ToString() == "Подключение")
            {
                foreach (string c in connectType)
                {
                    comboboxType.Items.Add(c);
                }
            }
            else if (comboboxView.SelectedItem.ToString() == "Управление договором/контактными данными")
            {
                foreach (string t in treatyType)
                {
                    comboboxType.Items.Add(t);
                }
            }
            else if (comboboxView.SelectedItem.ToString() == "Управление тарифом/услугой")
            {
                foreach (string r in rateType)
                {
                    comboboxType.Items.Add(r);
                }
            }
            else if (comboboxView.SelectedItem.ToString() == "Диагностика и настройка оборудования/подключения")
            {
                foreach (string d in diagnosticType)
                {
                    comboboxType.Items.Add(d);
                }
            }
            else if (comboboxView.SelectedItem.ToString() == "Оплата услуг")
            {
                foreach (string p in paymentType)
                {
                    comboboxType.Items.Add(p);
                }
            }
        }

        private void btnCheck_Click(object sender, RoutedEventArgs e)
        {
            CheckEquipment page = new CheckEquipment();
            page.Show();
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connection.Open();

                SqlCommand query = new SqlCommand("INSERT INTO [Zayavki] ([номер заявки], [Дата создания], ЛС, Услуга, [Вид услуги], [Тип услуги], Статус, [Тип оборудования], [Описание проблемы], [Дата закрытия], [Тип проблема]) VALUES (@number, @dateStart, @ls, @service, @viewService, @typeService, @status, @typeEquipment, @problem, @dateEnd, @typeProblem)", connection);

                query.Parameters.AddWithValue("@number", textboxNumber.Text);
                query.Parameters.AddWithValue("@dateStart", textboxDate.Text);
                query.Parameters.AddWithValue("@dateEnd", textboxDateEnd.Text);
                query.Parameters.AddWithValue("@ls", textboxLS.Text);
                query.Parameters.AddWithValue("@problem", textboxProblem.Text);
                query.Parameters.AddWithValue("@typeEquipment", textboxTypeEquipment.Text);
                query.Parameters.AddWithValue("@typeProblem", textboxTypeProblem.Text);
                query.Parameters.AddWithValue("@service", comboboxServices.SelectedItem.ToString());
                query.Parameters.AddWithValue("@status", comboboxStatus.SelectedItem.ToString());
                query.Parameters.AddWithValue("@typeService", comboboxType.SelectedItem.ToString());
                query.Parameters.AddWithValue("@viewService", comboboxView.SelectedItem.ToString());

                query.ExecuteNonQuery();
                MessageBox.Show("Заявка сохранена", "Заявка");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
