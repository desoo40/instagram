using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace instaBot
{
    class Post
    {
        public string Path { get; set; }
        public string Caption { get; set; }
        public DateTime PosTime { get; set; }

        public Post()
        {
            Path = "";
            Caption = "";
            PosTime = new DateTime();
        }

        public Post(string path, string cap, DateTime dt)
        {
            Path = path;
            Caption = cap;
            PosTime = new DateTime();
        }

        public Post(string path)
        {
            Path = path;
            Caption = "";
            PosTime = new DateTime();
        }
    }
}
