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
            ListCheck.SelectedValuePath = "ID";
            ListCheck.ItemsSource = UsersList;
            ListCheck.SelectionMode = SelectionMode.Single;

            var OfficeList = db.Offices.ToList();
            Office.ItemsSource = OfficeList;
            Office.SelectedIndex = 0;
            Office.DisplayMemberPath = "Title";
            Office.SelectedValuePath = "ID";



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

        private void Office_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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
            try
            {
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
                Users users = (Users)ListCheck.SelectedItem;
                users.LastName = Last_Name.Text;
                db.SaveChanges();
                List_Reload();
            }


        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (ListCheck.SelectedItem != null)
            {
                Users users = (Users)ListCheck.SelectedItem;
                db.Users.Remove(users);
                db.SaveChanges();
                List_Reload();
            }

        }
        private void Photo_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Выбор фото";
            openFileDialog.Filter = "All supported graphics| *.jpg;*.jpeg;*.png;";
            if (openFileDialog.ShowDialog() == true)
            {
                Profile.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                File.Delete(Directory.GetCurrentDirectory() + "/Photo/" + openFileDialog.SafeFileName);
                File.Copy(openFileDialog.FileName, Directory.GetCurrentDirectory() + "/Photo/" + openFileDialog.SafeFileName);

            }

        }

        private void ListCheck_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListCheck.SelectedItem != null)
            {
                Users users = (Users)ListCheck.SelectedItem;
                Email.Text = users.Email;
                Last_Name.Text = users.LastName;
                First_Name.Text = users.FirstName;
                Password.Text = users.Password;
                Office.SelectedItem = users.Offices;
                Birthday.SelectedDate = users.Birthdate;
                if (users.RoleID == 1)
                {
                    Role.IsChecked = true;
                }
                else
                {
                    Role.IsChecked = false;
                }
            }
        }
    }
}
