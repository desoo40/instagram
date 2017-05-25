using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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

        List<Post> postQueue = new List<Post>();
     
        public InstaBotWindow(InstagramUploader up, string l)
        {
            InitializeComponent();

            uploader = up;
            login = l;

            //BitmapImage b = new BitmapImage();
            //b.BeginInit();
            //b.UriSource = new Uri(@"D:\Фото\ЖОПИК\2016\IMG_6022.JPG");
            //b.EndInit();

            postQueue.Sort();

            textBoxLog.Text += $"[{DateTime.Now}]   Батя в здании! Под логином @{login}\n";
            //imagePost.Source = b;
        }

        private void listBoxQueue_Drop(object sender, DragEventArgs e)
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
                        _post.PosTime = caclTimeByLastInQueuePost();
                    else
                        _post.PosTime = wind.DateOfPost.SelectedDate.Value;

                    textBoxLog.Text += $"[{DateTime.Now}]   Пост добавлен в очередь с подписью \"{_post.Caption}\". Дата публикации: {_post.PosTime}\n";

                    //_post = wind.buttonReady_Click();

                    //if (wind.DateOfPost.SelectedDate == null)
                    //    _post.PosTime = caclTimeByLastInQueuePost();

                    //postQueue.Add();
                    //postQueue.Sort();
                }
            }
        }

        private DateTime caclTimeByLastInQueuePost()
        {
            return DateTime.Now;
        }
    }
}
