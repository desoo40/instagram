using InstaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
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

namespace instaBot
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        InstagramUploader uploader;
        string login;
        public MainWindow()
        {
            InitializeComponent();

            //InstaBotWindow instaWind = new InstaBotWindow(uploader, login);
            //App.Current.MainWindow = instaWind;
            //Close();
            //instaWind.Show();
        }
        public static SecureString ConvertToSecureString(string strPassword)
        {
            var secureStr = new SecureString();
            if (strPassword.Length > 0)
            {
                foreach (var c in strPassword.ToCharArray()) secureStr.AppendChar(c);
            }
            return secureStr;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            login = textBoxLogin.Text;
            SecureString pass = ConvertToSecureString(passwordBox.Password);

            uploader = new InstagramUploader(login, pass);

            uploader.SuccessfulLoginEvent += Uploader_SuccessfulLoginEvent;
            uploader.InvalidLoginEvent += (o, args) =>
            {
                MessageBox.Show("Неверный логин или пароль. Попробуйте еще раз", "Ошибка");
            };
            //uploader.UploadImage(@"D:\Фото\desoo88\IMG_6638.JPG", "");
            uploader.Login(); // без этой херни не работают две верхние (?)
        }


        private void Uploader_SuccessfulLoginEvent(object sender, EventArgs e)
        {
            InstaBotWindow instaWind = new InstaBotWindow(login, passwordBox.Password);
            App.Current.MainWindow = instaWind;
            Close();
            instaWind.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var login = "sasanichkin";
            var pass = "svirina";

            uploader = new InstagramUploader(login, ConvertToSecureString (pass));

            uploader.Login();
            uploader.Like();
        }
    }
}
