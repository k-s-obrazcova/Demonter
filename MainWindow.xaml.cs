using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
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
        public Session1_11Entities db = new Session1_11Entities();
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

            var OfficeList = db.Offices.ToList();
            Office.ItemsSource = OfficeList;
            Office.DisplayMemberPath = "Title";
            Office.SelectedValuePath = "ID";
            Office.SelectedIndex = 0;



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

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


        }

        private void ListCheck_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListCheck.SelectedItem != null)
            {
                Users selectedItem = (Users)ListCheck.SelectedItem;
                Email.Text = selectedItem.Email;
                Last_Name.Text = selectedItem.LastName;
                First_Name.Text = selectedItem.FirstName;
                Office.SelectedItem = selectedItem.Offices;
                Password.Text = selectedItem.Password;
                Birthday.SelectedDate = selectedItem.Birthdate;
                if (selectedItem.RoleID == 1)
                {
                    Role.IsChecked = true;
                }
                else
                {
                    Role.IsChecked = false;
                }
            }

        }

        private void Photo_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Выбор фото";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png";
            if (op.ShowDialog() == true)
            {
                Profile.Source = new BitmapImage(new Uri(op.FileName));
                File.Delete(Directory.GetCurrentDirectory() +"/Photo/" + op.SafeFileName);
                File.Copy(op.FileName, Directory.GetCurrentDirectory() + "/Photo/" + op.SafeFileName);
                Console.WriteLine("Фото успешно скопировано в папку Photo.");
            }
            //File.Delete(Directory.GetCurrentDirectory() +"/Photo/" + "тут название староrо файла");
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            string hash = BitConverter.ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(Password.Text))).Replace("-", "");
            Users user = new Users
            {
                Email = Email.Text,
                Password = hash,
                LastName = Last_Name.Text,
                FirstName = First_Name.Text,
                OfficeID = (int)Office.SelectedValue,
                Birthdate = Birthday.SelectedDate.Value,
                RoleID = (bool)Role.IsChecked ? 1 : 2,
                Active = true,


            };
            db.Users.Add(user);

            try { 
                db.SaveChanges();
            }
            catch
            {

            }
            List_Reload();

        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (ListCheck.SelectedItem != null)
            {
                Users selectedItem = (Users)ListCheck.SelectedItem;
                selectedItem.LastName = Last_Name.Text;
                db.SaveChanges();
                List_Reload();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Users selectedItem = (Users)ListCheck.SelectedItem;
            db.Users.Remove(selectedItem);
            db.SaveChanges();
            List_Reload();

        }
        private void Info_Click(object sender, RoutedEventArgs e)
        {

            Button button = sender as Button;
            if (button != null)
            {
                Grid grid = button.Parent as Grid;
                if (grid != null)
                {
                    Users selectedItem = grid.DataContext as Users;
                    if (selectedItem != null)
                    {
                        Window1 window = new Window1(selectedItem);
                        window.Show();
                        this.Close();
                    }
                }
            }
            
        }
    }
}
