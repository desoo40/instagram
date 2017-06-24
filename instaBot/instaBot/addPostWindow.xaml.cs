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
using static System.Windows.Forms.MessageBoxButtons;
using MessageBox = System.Windows.MessageBox;

namespace instaBot
{
    /// <summary>
    /// Логика взаимодействия для AddPostWindow.xaml
    /// </summary>
    public partial class AddPostWindow : Window
    {
        string Path;
        public bool DoNotAdd = false;
        public AddPostWindow(string p)
        {
            InitializeComponent();

            Path = p;

            BitmapImage b = new BitmapImage(new Uri(Path));

            imagePost.Source = b;         
        }

        private void buttonReady_Click(object sender, RoutedEventArgs e)
        {
            if (DateOfPost.SelectedDate == null && checkBoxTime.IsChecked == false)
            {
                MessageBox.Show("Что по дате?");
                return;
            }
            if (DateOfPost.SelectedDate != null)
            {
                var dd = DateOfPost.SelectedDate.Value.DayOfYear;
                if (dd < DateTime.Now.DayOfYear)
                {
                    MessageBox.Show("Указанная дата уже прошла...");
                    return;
                }
            }

            Close();
        }

        private void buttonAbort_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult r = MessageBox.Show("Не добавлять пост?", "Точно ли", MessageBoxButton.YesNo);

            if (r == MessageBoxResult.Yes)
                DoNotAdd = true;
        }
    }
}
