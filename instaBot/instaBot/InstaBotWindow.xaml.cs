using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using InstaSharp;

namespace instaBot
{
    /// <summary>
    /// Логика взаимодействия для InstaBotWindow.xaml
    /// </summary>
    public partial class InstaBotWindow : Window
    {
        InstagramUploader uploader;
        string login;
        Thread posting;
        List<Post> postQueue = new List<Post>();

        public static SecureString ConvertToSecureString(string strPassword)
        {
            var secureStr = new SecureString();
            if (strPassword.Length > 0)
            {
                foreach (var c in strPassword.ToCharArray()) secureStr.AppendChar(c);
            }
            return secureStr;
        }

        public InstaBotWindow(string l, string p)
        {
            InitializeComponent();
            login = l;
            SecureString pass = ConvertToSecureString(p);
            uploader = new InstagramUploader(l, pass);
            listViewQueue.ItemsSource = postQueue;
            posting = new Thread(PostUpload);
            posting.Start();
            
            //BitmapImage b = new BitmapImage();
            //b.BeginInit();
            //b.UriSource = new Uri(@"D:\Фото\ЖОПИК\2016\IMG_6022.JPG");
            //b.EndInit();

            textBoxLog.Text += $"[{DateTime.Now}]   Батя в здании! Под логином @{login}\n";
            
            //imagePost.Source = b;
        }

        private void listViewQueue_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] filesArr = (string[])e.Data.GetData(DataFormats.FileDrop);

                if (filesArr == null)
                    return;

                List<string> postList = filesArr.ToList();

                foreach (var post in postList)
                {
                    if (!File.Exists(post))
                    {
                        MessageBox.Show("Такого файла не существует?");
                        continue;
                    }

                    var ext = System.IO.Path.GetExtension(post);

                    if (ext.ToLower() != ".jpg")
                    { 
                        
                        MessageBox.Show($"{post} недопустимое расширение файла");
                        continue;
                    }

                    AddPostWindow wind = new AddPostWindow(post);
                    wind.ShowDialog();

                   

                    var _post = new Post(post);

                    _post.Caption = wind.textBox_Caption.Text;

                    if (wind.DateOfPost.SelectedDate == null)
                        _post.PosTime = CaclTimeByLastInQueuePost();
                    else
                    {
                        var hours = 15;
                        bool mor = wind.radioButtonMorning.IsChecked != null && wind.radioButtonMorning.IsChecked.Value;
                        bool ev = wind.radioButtonEvening.IsChecked != null && wind.radioButtonEvening.IsChecked.Value;
                        if (mor)
                            hours = 11;
                        if (ev)
                            hours = 19;

                        _post.PosTime = wind.DateOfPost.SelectedDate.Value;
                    
                        Random kek = new Random();

                        _post.PosTime = _post.PosTime.AddHours(hours);

                        _post.PosTime = _post.PosTime.AddMinutes(kek.Next(-60, 60));
                        _post.PosTime = _post.PosTime.AddSeconds(kek.Next(-60, 60));


                    }

                    textBoxLog.Text += $"[{DateTime.Now}]   Пост добавлен в очередь с подписью \"{_post.Caption}\". Дата публикации: {_post.PosTime}\n";

                    if (wind.DoNotAdd)
                        continue;

                    postQueue.Add(_post);

                    postQueue = new List<Post>(postQueue.OrderBy(p => p.PosTime));
                    listViewQueue.ItemsSource = null;
                    listViewQueue.ItemsSource = postQueue;

                    
                }
            }
        }

        private DateTime CaclTimeByLastInQueuePost()
        {
            if (postQueue.Count == 0)
                return DateTime.Now.AddMinutes(2);

            var temp = postQueue.Last().PosTime;

            temp = temp.AddDays(1);
            Random kek = new Random();

            int tmp = 18 - temp.Hour;


            temp = temp.AddHours(tmp);

            temp = temp.AddMinutes(kek.Next(-60, 60));
            temp = temp.AddSeconds(kek.Next(-60, 60));
            return temp;

            //var temp = postQueue.Last().PosTime;

            //Random kek = new Random();


            //temp = temp.AddSeconds(kek.Next(60));
            //return temp;
        }

        private void PostUpload()
        {
            while (true)
            {
                Thread.Sleep(100000);
                if (postQueue.Count == 0)
                    continue;

               

                var tmp = postQueue.First();

                if (tmp.PosTime < DateTime.Now)
                {
                    uploader.UploadImage(tmp.Path, tmp.Caption);
                    Dispatcher.Invoke((Action) (() =>
                    {
                        textBoxLog.Text +=
                            $"[{DateTime.Now}]   Опубликован пост с подписью \"{tmp.Caption}\".\n";

                    }
                        ));
                    postQueue.Remove(tmp);
                }
                Dispatcher.Invoke((Action)(UpdateGUI));
            }
        }

        private void UpdateGUI()
        {
            if (postQueue.Count == 0)
            {
                imagePost.Source = null;
                textBoxCaption.Text = null;
                labelTimer.Content = null;

            }
            else
            {
                var current = postQueue.First();
                string DateFormat = "dd.MM.yy HH:mm";
                BitmapImage bi = new BitmapImage(new Uri(current.Path));
                imagePost.Source = bi;
                textBoxCaption.Text = current.Caption;
                labelTimer.Content = current.PosTime.ToString(DateFormat);
            }

            postQueue = new List<Post>(postQueue.OrderBy(p => p.PosTime));
            listViewQueue.ItemsSource = null;
            listViewQueue.ItemsSource = postQueue;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult r = MessageBox.Show("Уверены что хотите выйти?", "Точно ли", MessageBoxButton.YesNo);

            if (r == MessageBoxResult.No)
                e.Cancel = true;
            else
                posting.Abort();
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                MessageBoxResult r = MessageBox.Show("Удалить пост?", "Точно ли", MessageBoxButton.YesNo);

                
            }
        }
    }
}
