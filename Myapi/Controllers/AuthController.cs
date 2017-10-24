using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Myapi.ViewModel;
using Flurl.Http;

namespace Myapi.Controllers
{
    public class AuthController : Controller
    {
        [HttpPost]
        [Route("/api/GetToken", Name = "Token")]
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

    }
}