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
            if (api.Login())
            {
                var list = api.GetFeedMediaIdsByUser("desoo88");

                foreach (var kek in list)
                {
                    api.SetLikeToMedia(kek);
                    Console.WriteLine("Liked post with ID = " + kek);

                    System.Threading.Thread.Sleep(1000);
                }
                //api.GetUserFeed();
                //api.SetLikeToMedia(api.TempId);
            }

            //End
            Console.WriteLine("Press Enter to End...");
            Console.ReadLine();
        }
    }
}
