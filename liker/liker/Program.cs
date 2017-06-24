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
            var client = new RestClient("https://www.instagram.com/");
            client.CookieContainer = new CookieContainer();
            client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36";

            var firstRequest = new RestRequest("/", Method.GET);
            var firstResponse = client.Execute(firstRequest);
            //use token to the next request, etc.
            var csrftoken = firstResponse.Cookies.First(x => x.Name == "csrftoken").Value;

            //login
            var loginRequest = new RestRequest("/accounts/login/ajax/", Method.POST);
            loginRequest.AddHeader("X-CSRFToken", csrftoken);
            loginRequest.AddHeader("X-Requested-With", "XMLHttpRequest");
            loginRequest.AddHeader("X-Instagram-AJAX", "1");
            loginRequest.AddHeader("Referer", client.BaseUrl.ToString());
            loginRequest.AddParameter("username", "sasanichkin");
            loginRequest.AddParameter("password", "svirina");

            var loginResponse = client.Execute(loginRequest);
            csrftoken = loginResponse.Cookies.First(x => x.Name == "csrftoken").Value;
            Console.WriteLine("May be LogIn successful...");
            
            //getUserFeed JSON
            var feedRequest = new RestRequest("?__a=1", Method.GET);
            feedRequest.AddHeader("X-CSRFToken", csrftoken);
            feedRequest.AddHeader("X-Requested-With", "XMLHttpRequest");
            feedRequest.AddHeader("X-Instagram-AJAX", "1");
            feedRequest.AddHeader("Referer", client.BaseUrl.ToString());
            //rq.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            var feedResponse = client.Execute(feedRequest);
            csrftoken = feedResponse.Cookies.First(x => x.Name == "csrftoken").Value;

            //get first media ID from JSON feed
            JObject o = JObject.Parse(feedResponse.Content);
            var a = o["graphql"]["user"]["edge_web_feed_timeline"]["edges"];
            var id = a.First["node"]["id"].ToString();
            var shortcode = a.First["node"]["shortcode"].ToString();
            var shortcodeUrl = "https://www.instagram.com/p/shortcode/".Replace("shortcode", shortcode);
            Console.WriteLine(id);
            Console.WriteLine(shortcodeUrl);

            //setLike to id
            var urlLike = $"web/likes/{id}/like/";
            var likeRequest = new RestRequest(urlLike, Method.POST);
            
            likeRequest.AddHeader("Referer", "https://www.instagram.com/");
            likeRequest.AddHeader("X-Requested-With", "XMLHttpRequest");
            likeRequest.AddHeader("Accept-Language", "ru-RU,ru;q=0.8,en-US;q=0.6,en;q=0.4");
            likeRequest.AddHeader("Accept-Encoding", "gzip, deflate, br");
            likeRequest.AddHeader("Accept", "*/*");
            likeRequest.AddHeader("X-Instagram-AJAX", "1");
            likeRequest.AddHeader("Origin", "https://www.instagram.com");
            likeRequest.AddHeader("Host", "www.instagram.com");
            likeRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            likeRequest.AddHeader("X-CSRFToken", csrftoken);

            var likeResponse = client.Execute(likeRequest);
            if(likeResponse.Content.Contains("\"status\": \"ok\""))
            {
                Console.WriteLine("Liked!");
            }
            else
            {
                Console.WriteLine("Error");
                Console.WriteLine(likeResponse.Content);
            }

            //End
            Console.WriteLine("Press Enter to End...");
            Console.ReadLine();
        }

        //use it to parse REST Response JSON data to Class fields
        public class LoginResponse
        {
            public string status { get; set; }
            public bool authenticated { get; set; }
        }
    }
}
