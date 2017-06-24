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
            client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/48.0.2564.103 Safari/537.36";
            var firstRequest = new RestRequest("/", Method.GET);
            var firstResponse = client.Execute(firstRequest);

            var csrftoken = firstResponse.Cookies.First(x => x.Name == "csrftoken").Value;

            var loginRequest = new RestRequest("/accounts/login/ajax/", Method.POST);
            loginRequest.AddHeader("X-CSRFToken", csrftoken);
            loginRequest.AddHeader("X-Requested-With", "XMLHttpRequest");
            loginRequest.AddHeader("X-Instagram-AJAX", "1");
            loginRequest.AddHeader("Referer", client.BaseUrl.ToString());
            loginRequest.AddParameter("username", "sasanichkin");
            loginRequest.AddParameter("password", "svirina");

            var loginResponse = client.Execute<LoginResponse>(loginRequest).Data;

            if (loginResponse.authenticated)
            {
                Console.WriteLine("Батя в здании!");
                var rq = new RestRequest("?__a=1", Method.GET);
                    rq.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
                var r = client.Execute(rq);
                JObject o = JObject.Parse(r.Content);
                Console.Write(r.Content);
                var a = o["graphql"]["user"]["edge_web_feed_timeline"]["edges"];
                var b = a.First["node"]["id"].ToString();
                r = client.Execute(new RestRequest($"web/likes/{b}/like/", Method.POST));
                Console.Write(r.Content);

            }

            Console.ReadLine();
        }
        public class LoginResponse
        {
            public string status { get; set; }
            public bool authenticated { get; set; }
        }
    }
}
