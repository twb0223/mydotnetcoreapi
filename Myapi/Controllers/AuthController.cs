using Flurl.Http;
using Microsoft.AspNetCore.Mvc;
using Myapi.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Myapi.Controllers
{
    public class AuthController : Controller
    {
        const string tokenurl = "http://localhost:5000/connect/token";
        /// <summary>
        /// 获取凭据--客户端模式
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="secret"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/api/GetToken_ClientCredentials")]
        public async Task<Token> GetTokenAsync(string clientid,string secret)
        {
            //获得token
            var dict = new SortedDictionary<string, string>();
            dict.Add("client_id", clientid);
            dict.Add("client_secret", secret);
            dict.Add("grant_type", "client_credentials");
            var data = await tokenurl.PostUrlEncodedAsync(dict).ReceiveJson<Token>();
            return data;
        }
        /// <summary>
        /// 获取凭据--密码模式
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="secret"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
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
            var data = await tokenurl.PostUrlEncodedAsync(dict).ReceiveJson<Token>();
            return data;
        }
    }
}