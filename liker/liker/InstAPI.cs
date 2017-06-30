using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace InstaBot
{
    public class InstAPI
    {
        private string _login;
        private string _pass;

        private string _instaWebLogin = "accounts/login/ajax/";
        private string _instaWebUserFeed = "?__a=1";
        private string _instaWebFeedByUser = "_USER_HERE_/?__a=1&max_id=";
        private string _instaWebLike = "web/likes/_ID_HERE_/like/";
        private string _instaWebPostInfo = "p/_CODE_HERE_/?__a=1";

        public string TempId { get; set; }

        private RestClient _client;
        private string _csrftoken;
        private bool _isLogin;

        public class MediaInfo
        {
            public string Id { get; set; }
            public string Code { get; set; }

        }

        public InstAPI(string login, string pass)
        {
            _login = login;
            _pass = pass;

            Init();
        }

        private void Init()
        {
            _client = new RestClient("https://www.instagram.com/");
            _client.CookieContainer = new CookieContainer();
            _client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36";
        }

        private void AddStdHeaders(ref RestRequest request)
        {
            request.AddHeader("X-CSRFToken", _csrftoken);
            request.AddHeader("X-Requested-With", "XMLHttpRequest");
            request.AddHeader("X-Instagram-AJAX", "1");
            request.AddHeader("Referer", _client.BaseUrl.ToString());
        }

        private IRestResponse MakeRequest(RestRequest request)
        {
            var response = _client.Execute(request);
            _csrftoken = response.Cookies.First(x => x.Name == "csrftoken").Value;

            return response;
        }

        public bool Login()
        {
            var firstRequest = new RestRequest("/", Method.GET);
            var firstResponse = _client.Execute(firstRequest);
            //use token to the next request, etc.
            _csrftoken = firstResponse.Cookies.First(x => x.Name == "csrftoken").Value;

            var loginRequest = new RestRequest(_instaWebLogin, Method.POST);
            AddStdHeaders(ref loginRequest);
            loginRequest.AddParameter("username", _login);
            loginRequest.AddParameter("password", _pass);

            var loginResponse = MakeRequest(loginRequest);
            var o = JObject.Parse(loginResponse.Content);

            if (o["authenticated"].ToString().ToLower() == "true")
            {
                Console.WriteLine("Login successful!");
                _isLogin = true;
            }
            else
            {
                Console.WriteLine("Login failed!");

                if (o["user"].ToString().ToLower() == "true")
                {
                    Console.WriteLine("Wrong password");
                }
                else
                {
                    Console.WriteLine("No such user");
                }
            }
            return _isLogin;
        }

        public void GetUserFeed()
        {
            if (!_isLogin)
            {
                Console.WriteLine("Please, login first!");
                return;
            }

            //getUserFeed JSON
            var feedRequest = new RestRequest(_instaWebUserFeed, Method.GET);
            AddStdHeaders(ref feedRequest);

            //rq.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            var feedResponse = MakeRequest(feedRequest);

            //get first media ID from JSON feed
            var o = JObject.Parse(feedResponse.Content);
            var a = o["graphql"]["user"]["edge_web_feed_timeline"]["edges"];
            var list = o["graphql"]["user"].ToList();
            var id = a.First["node"]["id"].ToString();
            var shortcode = a.First["node"]["shortcode"].ToString();
            var shortcodeUrl = "https://www.instagram.com/p/shortcode/".Replace("shortcode", shortcode);
            Console.WriteLine(id);
            Console.WriteLine(shortcodeUrl);

            TempId = id;
        }

        public List<MediaInfo> GetFeedMediaIdsByUser(string user)
        {
            if (!_isLogin)
            {
                Console.WriteLine("Please, login first!");
                return null;
            }
            var tmpMediaId = "";
            var listOfMediaId = new List<MediaInfo>();
            var hasNextPage = true;

            while (hasNextPage)
            {
                //getUserFeed JSON
                var feedRequest = new RestRequest(_instaWebFeedByUser.Replace("_USER_HERE_", user) + tmpMediaId, Method.GET);
                AddStdHeaders(ref feedRequest);

                var feedResponse = MakeRequest(feedRequest);

                //get first media ID from JSON feed
                var parsedJson = JObject.Parse(feedResponse.Content);
                var mediaNode = parsedJson["user"]["media"]["nodes"].ToList();
                var nextPageStr = parsedJson["user"]["media"]["page_info"]["has_next_page"].ToString();

                foreach (var b in mediaNode)
                {
                    listOfMediaId.Add(new MediaInfo() {Code = b["code"].ToString(), Id = b["id"].ToString()});
                }

                if (nextPageStr.ToLower() == "false")
                    hasNextPage = false;

                tmpMediaId = listOfMediaId.Last().Id;
                Console.Write("\rCollected: " + listOfMediaId.Count);
            }
        
            Console.WriteLine();
            return listOfMediaId;
        }

        public bool SetLikeToMedia(MediaInfo inf)
        {
            if (!_isLogin)
            {
                Console.WriteLine("Please, login first!");
                return false;
            }
            if (CheckLikeByCode(inf.Code))
            {
                Console.WriteLine("Skipped");
                return false;
            }

            //setLike to id
            var likeRequest = new RestRequest(_instaWebLike.Replace("_ID_HERE_", inf.Id), Method.POST);
            AddStdHeaders(ref likeRequest);

            var likeResponse = MakeRequest(likeRequest);

            if (likeResponse.Content.Contains("\"status\": \"ok\""))
            {
                Console.WriteLine("Liked ID=" + inf.Id);
                return true;
            }

            Console.WriteLine("Error");
            Console.WriteLine(likeResponse.Content);

            return false;
        }

        private bool CheckLikeByCode(string code)
        {
            if (!_isLogin)
            {
                Console.WriteLine("Please, login first!");
                return false;
            }

            var infoRequest = new RestRequest(_instaWebPostInfo.Replace("_CODE_HERE_", code), Method.POST);
            AddStdHeaders(ref infoRequest);

            var infoResponse = MakeRequest(infoRequest);
            var parsedJson = JObject.Parse(infoResponse.Content);
            var isLikedStr = parsedJson["graphql"]["shortcode_media"]["viewer_has_liked"].ToString();

            return bool.Parse(isLikedStr);
        }

    }
}