using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace InstaBot
{
    class Program
    {
        static void Main(string[] args)
        {
            InstAPI api = new InstAPI("desoo88", "Sd1324567");

            if(!api.Login())
                return;

            var listOfMediaId = api.GetFeedMediaIdsByUser("shurubushek_");

            var kek = new Random();

            foreach (var publication in listOfMediaId)
            {
                api.SetLikeToMedia(publication);
                Console.WriteLine("ID = " + publication);
                var sleepTimeKek = kek.Next(-5000, 5000);
                System.Threading.Thread.Sleep(20000 + sleepTimeKek);
                //api.GetUserFeed();
                //api.SetLikeToMedia(api.TempId);
            }

            //End
            Console.WriteLine("Press Enter to End...");
            Console.ReadLine();
        }
    }
}
