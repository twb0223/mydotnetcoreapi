using Microsoft.AspNetCore.Mvc;
using Myapi.Services;
using Myapi.SqlContext;
using Myapi.Models;
using Myapi.Common;
using System.Collections.Generic;
using System;

namespace Myapi.Controllers
{
    [Route("api/[controller]")]
    public class ApplicationController : Controller
    {
        private SqlContext.MySqlContext mySqlContext;
        private AppServices appServices;

        public ApplicationController()
        {
            this.mySqlContext = new MySqlContext();
            this.appServices = new AppServices(mySqlContext);
        }

        [HttpPost]
        public PageResult<Application> GetAllApplications(int pagesize,int pageindex)
        {
            var pageResult = new PageResult<Application>
            {
                Code = ResultCode.C1000,
                Message = "OK",
                PageIndex=pageindex,
                PageSize=pagesize,
                Total=appServices.GetApplicationsCout(),
                Data = appServices.GetPageList(pagesize,pageindex)
            };
            return pageResult;
        }
        // [HttpPut]
        // public SingleResult<string> AddApplist()
        // {
        //     var applist = new List<Models.Application>();
        //     for (int i = 0; i < 100000; i++)
        //     {
        //         applist.Add(new Models.Application
        //         {
        //             ID = Guid.NewGuid(),
        //             AccountID = Guid.NewGuid(),
        //             AppId = Guid.NewGuid(),
        //             AppName = "App" + i,
        //             AppSecret = MD5Helper.GetEncryptResult("App" + i, "&&*(^FGHGYGY^^#^&*%^&#E"),
        //             CreateTime = DateTime.Now
        //         });
        //     }
        //     appServices.InsertList(applist);
        //     return new SingleResult<string>
        //     {
        //         Code = ResultCode.C1000,
        //         Message = "OK",
        //     };
        // }
    }
}
