using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CRM.Pages
{
    /// <summary>
    /// Логика взаимодействия для CheckEquipment.xaml
    /// </summary>
    public partial class CheckEquipment : Window
    {
        BackgroundWorker worker = new BackgroundWorker();
        int counter = 0;

        public CheckEquipment()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.WorkerSupportsCancellation = true;

            worker.RunWorkerAsync();
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while(!worker.CancellationPending)
            {
                Render();
                (sender as BackgroundWorker).ReportProgress(counter);
            }
        }

        public bool Render()
        {
            counter++;
            Thread.Sleep(75);
            return true;
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pBar.Value = e.ProgressPercentage;
        }

        private void pBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Random rnd = new Random();
            int chance = rnd.Next(1, 100);

            if (pBar.Value == 100.0)
            {
                MessageBox.Show("Оборудование успешно прошло проверку!", "Успех");
                pBar.IsIndeterminate = false;
                worker.CancelAsync();
                ((MainWindow)Application.Current.MainWindow).crm.stat = 2;
            }
            else if (chance <= 1)
            {
                pBar.IsIndeterminate = false;
                worker.CancelAsync();
                MessageBox.Show("Оборудование не прошло проверку! Возникли неполадки", "Ошибка");
                ((MainWindow)Application.Current.MainWindow).crm.stat = 1;
            }
        }
    }
}
