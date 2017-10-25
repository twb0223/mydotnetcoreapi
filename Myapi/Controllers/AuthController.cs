using Flurl.Http;
using Microsoft.AspNetCore.Mvc;
using Myapi.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using IdentityModel.Client;
using System.Threading.Tasks;

namespace Myapi.Controllers
{
    public class AuthController : Controller
    {
        const string baseuri = "http://localhost:5000/";
        const string tokenurl = "http://localhost:5000/connect/token";
        const string apiurl = "http://localhost:5001";
        /// <summary>
        /// 获取凭据--客户端模式
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="secret"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/api/GetToken_ClientCredentials")]
        public async Task<Token> GetTokenAsync(string clientid, string secret)
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
        public async Task<Token> GetTokenAsync(string clientid, string secret, string username, string password)
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
        /// <summary>
        /// 使用IdentityServer Model 获取用户Claims
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/GetClaim")]
        public async Task<string> GetClaim()
        {
            var disco = await DiscoveryClient.GetAsync(baseuri);
            // request token
            var tokenClient = new TokenClient(disco.TokenEndpoint, "client", "secret");
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync("api1");
            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);
            var response = await client.GetAsync(apiurl + @"/api/Claims");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        /// <summary>
        /// 授权后请求数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/GetApplication")]
        public async Task<string> GetApplication()
        {
            //获得token
            var dict = new SortedDictionary<string, string>();
            dict.Add("client_id", "client");
            dict.Add("client_secret", "secret");
            dict.Add("grant_type", "client_credentials");
            var data = await tokenurl.PostUrlEncodedAsync(dict).ReceiveJson<Token>();
            var content = await ($@"{apiurl}/api/Application").WithOAuthBearerToken(data.access_token).PostAsync(null).ReceiveString();
            return content;
        }
    }
}