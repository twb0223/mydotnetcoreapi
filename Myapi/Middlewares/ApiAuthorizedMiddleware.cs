using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Myapi.Common;
using Myapi.Models;
using Myapi.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Myapi.Middlewares
{
    public class ApiAuthorizedMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ApiAuthorizedOptions _options;

        public ApiAuthorizedMiddleware(RequestDelegate next, IOptions<ApiAuthorizedOptions> options)
        {
            this._next = next;
            this._options = options.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Path.Value.Contains("/api/Accounts"))
            {
                //switch (context.Request.Method.ToUpper())
                //{
                //    case "POST":
                //        if (context.Request.HasFormContentType)
                //        {
                //            await PostInvoke(context);
                //        }
                //        else
                //        {
                //            await ReturnNoAuthorized(context);
                //        }
                //        break;
                //    case "GET":
                //        await GetInvoke(context);
                //        break;
                //    default:
                //        await GetInvoke(context);
                //        break;
                //}

                await GetRequestHeadInvoke(context);
                ////根据状态码判断是否继续使用这个contex会话。
                if (context.Response.StatusCode == 200)
                {
                    await _next.Invoke(context);
                }
            }
            else
            {
                await _next.Invoke(context);
            }
        }

        #region private method
        /// <summary>
        /// not authorized request
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task ReturnNoAuthorized(HttpContext context)
        {
            BaseResponseResult response = new BaseResponseResult
            {
                Code = ResultCode.S401,
                Message = "You are not authorized!"
            };
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }

        /// <summary>
        /// timeout request 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task ReturnTimeOut(HttpContext context)
        {
            BaseResponseResult response = new BaseResponseResult
            {
                Code = ResultCode.S408,
                Message = "Time Out!"
            };
            context.Response.StatusCode = 408;
            await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }

        /// <summary>
        /// 判断是否存在该appid和appsecret的数据。
        /// </summary>
        /// <param name="context"></param>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <returns></returns>
        private async Task CheckApplication(HttpContext context, string appId, string appSecret)
        {
            var IsExist = GetAllApplications().Any(x => x.AppId.ToString() == appId & x.AppSecret == appSecret);
            if (!IsExist)
            {
                await ReturnNoAuthorized(context);
            }
        }

        /// <summary>
        /// check the expired time
        /// </summary>
        /// <param name="timestamp"></param>
        /// <param name="expiredSecond"></param>
        /// <returns></returns>
        private bool CheckExpiredTime(double timestamp, double expiredSecond)
        {
            double now_timestamp = (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
            return (now_timestamp - timestamp) > expiredSecond;
        }

        /// <summary>
        /// http get invoke
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task GetInvoke(HttpContext context)
        {
            var queryStrings = context.Request.Query;
            RequestInfo requestInfo = new RequestInfo
            {
                AppID = queryStrings["appid"].ToString(),
                AppSecret = queryStrings["appsecret"].ToString(),
                Timestamp = queryStrings["timestamp"].ToString(),
                Nonce = queryStrings["nonce"].ToString(),
                Sinature = queryStrings["signature"].ToString()
            };
            await Check(context, requestInfo);
        }
       
        /// <summary>
        /// 从head提取认证相关信息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task GetRequestHeadInvoke(HttpContext context)
        {
            var header = context.Request.Headers;
            RequestInfo requestInfo = new RequestInfo
            {
                AppID = header["appid"].ToString(),
                AppSecret = header["appsecret"].ToString(),
                Timestamp = header["timestamp"].ToString(),
                Nonce = header["nonce"].ToString(),
                Sinature = header["signature"].ToString()
            };
            await Check(context, requestInfo);
        }


        /// <summary>
        /// http post invoke
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task PostInvoke(HttpContext context)
        {
            var formCollection = context.Request.Form;
            RequestInfo requestInfo = new RequestInfo
            {
                AppID = formCollection["applicationId"].ToString(),
                AppSecret = formCollection["applicationPassword"].ToString(),
                Timestamp = formCollection["timestamp"].ToString(),
                Nonce = formCollection["nonce"].ToString(),
                Sinature = formCollection["signature"].ToString()
            };
            await Check(context, requestInfo);

        }

        /// <summary>
        /// the main check method
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requestInfo"></param>
        /// <returns></returns>
        private async Task Check(HttpContext context, RequestInfo requestInfo)
        {
            string computeSinature = MD5Helper.GetEncryptResult($"{requestInfo.AppID}-{requestInfo.Timestamp}-{requestInfo.Nonce}", _options.EncryptKey);
            if (computeSinature.Equals(requestInfo.Sinature) &&
                double.TryParse(requestInfo.Timestamp, out double tmpTimestamp))
            {
                if (CheckExpiredTime(tmpTimestamp, _options.ExpiredSecond))
                {
                    await ReturnTimeOut(context);
                }
                else
                {
                    await CheckApplication(context, requestInfo.AppID, requestInfo.AppSecret);
                }
            }
            else
            {
                await ReturnNoAuthorized(context);
            }
        }

        /// <summary>
        /// return the application infomations
        /// </summary>
        /// <returns></returns>
        private IEnumerable<Application> GetAllApplications()
        {
            Myapi.SqlContext.MySqlContext mySqlContext = new SqlContext.MySqlContext();
            AppServices appServices = new AppServices(mySqlContext);
            return appServices.GetList();
        }
        #endregion
    }
}