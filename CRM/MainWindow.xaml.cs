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

namespace CRM
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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
    }
}
