using System;
using System.Collections.Generic;
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
        public CRM()
        {
            InitializeComponent();
        }

        private void Grid_Initialized(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now;
            textboxDate.Text = date.ToString("d");

            List<string> services = new List<string>() { "Интернет", "Мобильная связь", "Телевидение", "Видеонаблюдение" };
            List<string> viewServices = new List<string>() { "Подключение", "Управление договором/контактными данными", "Управление тарифом/услугой", "Диагностика и настройка оборудования/подключения", "Оплата услуг" };
            List<string> connectType = new List<string>() { "Подключение услуг с новой инфраструктурой", "Подключение услуг на существующей инфраструктуре" };
            List<string> treatyType = new List<string>() { "Изменение условий договора", "Включение в договор дополнительной услуги", "Изменение контактных данных" };
            List<string> rateType = new List<string>() { "Изменение тарифа", "Изменение адреса предоставления услуг", "Отключение услуги", "Приостановка предоставления услуги" };
            List<string> diagnosticType = new List<string>() { };
            List<string> paymentType = new List<string>() { };
        }
    }
}
