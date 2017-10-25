using Flurl.Http;
using Microsoft.AspNetCore.Mvc;
using Myapi.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Myapi.Controllers
{
    public class AuthController : Controller
    {
        [HttpPost]
        [Route("/api/GetToken_ClientCredentials", Name = "Token")]
        public async Task<Token> GetTokenAsync(string clientid,string secret)
        {
            //获得token
            var dict = new SortedDictionary<string, string>();
            dict.Add("client_id", clientid);
            dict.Add("client_secret", secret);
            dict.Add("grant_type", "client_credentials");
            var data = await (@"http://localhost:5000/connect/token").PostUrlEncodedAsync(dict).ReceiveJson<Token>();
            //根据token获得咨询信息 [Authorization: Bearer {THE TOKEN}]
            //var news = await (@"http://" + Request.RequestUri.Authority + @"/api/v1/oauth2/news").WithHeader("Authorization", "Bearer " + data.access_token).GetAsync().ReceiveString();
            // var news = await (@"http://"  + @"/api/v1/oauth2/news").WithOAuthBearerToken(data.access_token).GetAsync().ReceiveString();
            return data;
        }
        [HttpPost]
        [Route("/api/GetToken_ResourceOwnerPassword")]
        public async Task<Token> GetTokenAsync(string clientid, string secret,string username,string password)
        {
            //获得token
            var dict = new SortedDictionary<string, string>();
            dict.Add("client_id", clientid);
            dict.Add("client_secret", secret);
            dict.Add("grant_type", "password");
            dict.Add("username", username);
            dict.Add("password", password);
            var data = await (@"http://localhost:5000/connect/token").PostUrlEncodedAsync(dict).ReceiveJson<Token>();
            //根据token获得咨询信息 [Authorization: Bearer {THE TOKEN}]
            //var news = await (@"http://" + Request.RequestUri.Authority + @"/api/v1/oauth2/news").WithHeader("Authorization", "Bearer " + data.access_token).GetAsync().ReceiveString();
            // var news = await (@"http://"  + @"/api/v1/oauth2/news").WithOAuthBearerToken(data.access_token).GetAsync().ReceiveString();
            return data;
        }
    }
}