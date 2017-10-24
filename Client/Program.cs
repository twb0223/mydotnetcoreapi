using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();

        }
        private static async Task MainAsync()
        {
            Console.Title = "Credentials Client";
            
            //// 1.从元数据中发现客户端
            //var disco = await DiscoveryClient.GetAsync("http://localhost:5000");
            //// 请求令牌
            //var tokenClient = new TokenClient(disco.TokenEndpoint, "client", "secret");
            //var tokenResponse = await tokenClient.RequestClientCredentialsAsync("api1");

            //if (tokenResponse.IsError)
            //{
            //    Console.WriteLine(tokenResponse.Error);
            //    return;
            //}

            //Console.WriteLine(tokenResponse.Json);
            //Console.WriteLine("\n\n");

            //2.直接通过Http 访问得到Token http://localhost:5000/connect/token,
            //需要构造请求头相关信息。
            List<KeyValuePair<string, string>> formdata = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type","client_credentials"),
                new KeyValuePair<string, string>("client_id","client"),
                new KeyValuePair<string, string>("client_secret","secret")
            };
            var token =await HttpPostAsync("http://localhost:5000", "connect/token", formdata);
            
            Console.WriteLine(token);

            //// 调用api
            //var client = new HttpClient();
            //client.SetBearerToken(tokenResponse.AccessToken);
            //var response = await client.GetAsync("http://localhost:5001/identity");
            //if (!response.IsSuccessStatusCode)
            //{
            //    Console.WriteLine(response.StatusCode);
            //}
            //else
            //{
            //    var content = await response.Content.ReadAsStringAsync();
            //    Console.WriteLine(JArray.Parse(content));
            //}
        }
        public async static Task<string> HttpPostAsync(string uri, string url, List<KeyValuePair<string, string>> formData = null, string charset = "UTF-8", string mediaType = "application/x-www-form-urlencoded")
        {
            string tokenUri = url;
            var client = new HttpClient();
            client.BaseAddress = new Uri(uri);
            HttpContent content = new FormUrlEncodedContent(formData);
            content.Headers.ContentType = new MediaTypeHeaderValue(mediaType);
            content.Headers.ContentType.CharSet = charset;
            for (int i = 0; i < formData.Count; i++)
            {
                content.Headers.Add(formData[i].Key, formData[i].Value);
            }
            var resp = await client.PostAsync(tokenUri, content);
            var token = await resp.Content.ReadAsStringAsync();
            return token;
        }
    }
}
