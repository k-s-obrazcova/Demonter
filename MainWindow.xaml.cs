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

namespace Session1_11
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Session1_11Entities db = new Session1_11Entities();
        public MainWindow()
        {
            InitializeComponent();
            Title = "Самолеты - главная страница";
            List_Reload();

        }

        public void List_Reload()
        {
            var UsersList = db.Users.ToList();
            ListCheck.SelectedValuePath = "ID";
            ListCheck.ItemsSource = UsersList;
            ListCheck.SelectionMode = SelectionMode.Single;
        }

        private void InfoBox_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("INFO", "User Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void WarnBox_Click(object sender, RoutedEventArgs e)
        {
           MessageBoxResult result =  MessageBox.Show("WARN", "User Warn", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            switch(result)
            {
                case MessageBoxResult.Yes:
                    Console.WriteLine("YES");
                    break;
                case MessageBoxResult.No:
                    Console.WriteLine("NO");
                    break;
            }
        }
    }
}
