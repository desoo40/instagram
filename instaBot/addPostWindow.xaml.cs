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
using System.Windows.Shapes;

namespace instaBot
{
    /// <summary>
    /// Логика взаимодействия для AddPostWindow.xaml
    /// </summary>
    public partial class AddPostWindow : Window
    {
        string Path;
        public AddPostWindow(string p)
        {
            InitializeComponent();

            Path = p;

            BitmapImage b = new BitmapImage();
            b.BeginInit();
            b.UriSource = new Uri(Path);
            b.EndInit();

            imagePost.Source = b;         
        }

        private void buttonReady_Click(object sender, RoutedEventArgs e)
        {
            if (DateOfPost.SelectedDate == null && checkBoxTime.IsChecked == false)
            {
                MessageBox.Show("Что по дате?");
                return;
            }

            if (DateOfPost.SelectedDate < DateTime.Now)
            {
                MessageBox.Show("Указанная дата уже прошла...");
                return;
            }
            
            Close();
        }

    }
}
