using System;

namespace instaSpider
{
    class Program
    {
        static void Main(string[] args)
        {
            InstAPI api = new InstAPI("sasanichkin", "svirina");

            if(!api.Login())
                return;

            #region Follow

            api.FollowUser("dimonstrik");
            #endregion

            #region Like
            //var listOfMediaId = api.GetFeedMediaIdsByUser("desoo");

            //var kek = new Random();

            //foreach (var publication in listOfMediaId)
            //{
            //    if (api.SetLikeToMedia(publication))
            //    {
            //        var sleepTimeKek = kek.Next(-2000, 2000);
            //        System.Threading.Thread.Sleep(5000 + sleepTimeKek);
            //        //api.GetUserFeed();
            //        //api.SetLikeToMedia(api.TempId);
            //    }
            //}
            #endregion
            //End
            Console.WriteLine("Press Enter to End...");
            Console.ReadLine();
        }
    }
}
