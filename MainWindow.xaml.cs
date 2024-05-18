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

namespace WpfApp26
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public SamoletsEntities db = new SamoletsEntities();
        public MainWindow()
        {
            InitializeComponent();
            Title = "News";
            List_Reload();
        }
        public void List_Reload()
        {
            
                var UsersList = db.Users.ToList();
                //UsersList = UsersList.Where(a => a.First_Name == "Андрей").Distinct().ToList();
                ListCheck.SelectedValuePath = "ID";
                ListCheck.ItemsSource = UsersList;
                ListCheck.SelectionMode = SelectionMode.Single;

        }
        private void ConsoleWarn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("WARN", "User Warn", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    Console.WriteLine("ДА");
                    break;
                case MessageBoxResult.No:
                    Console.WriteLine("Нет");
                    break;

            }

        }
        private void ConsoleInfo_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("INFO", "User Info", MessageBoxButton.OK, MessageBoxImage.Information);

        }
    }
}
