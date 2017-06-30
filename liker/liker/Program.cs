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
            InstAPI api = new InstAPI("sasanichkin", "svirina");

            if(!api.Login())
                return;

            var listOfMediaId = api.GetFeedMediaIdsByUser("desoo");

            var kek = new Random();

            foreach (var publication in listOfMediaId)
            {
                if (api.SetLikeToMedia(publication))
                {
                    var sleepTimeKek = kek.Next(-2000, 2000);
                    System.Threading.Thread.Sleep(5000 + sleepTimeKek);
                    //api.GetUserFeed();
                    //api.SetLikeToMedia(api.TempId);
                }
            }

            //End
            Console.WriteLine("Press Enter to End...");
            Console.ReadLine();
        }
    }
}
