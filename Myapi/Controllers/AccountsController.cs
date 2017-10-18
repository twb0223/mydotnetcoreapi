using Microsoft.AspNetCore.Mvc;
using Myapi.Common;
using Myapi.Services;
using Myapi.SqlContext;
using Myapi.ViewModel;
using System;
using System.Collections.Generic;

namespace Myapi.Controllers
{

     [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        private SqlContext.MySqlContext mySqlContext;
        private AccountServices accountServices;
        private AppServices appServices;
        public AccountsController()
        {
            this.mySqlContext = new MySqlContext();
            this.accountServices = new AccountServices(mySqlContext);
            this.appServices = new AppServices(mySqlContext);
        }

        [HttpGet]
        public ListResult<Account_ApplicationVM> Get()
        {
            var commonResult = new ListResult<Account_ApplicationVM>
            {
                Code = ResultCode.C1000,
                Message = "OK",
                Data = accountServices.GetAccountApplist()
            };
            return commonResult;
        }

        [HttpPost]
        public SingleResult<string> AddNewApp(AppDto appDto)
        {
            appServices.Insert(new Models.Application
            {
                ID = Guid.NewGuid(),
                AccountID = appDto.AccountID,
                AppId = Guid.NewGuid(),
                AppName = appDto.AppName,
                AppSecret = MD5Helper.GetEncryptResult(appDto.AppName, appDto.AccountID + appDto.AppName),
                CreateTime = DateTime.Now
            });
            return new SingleResult<string>
            {
                Code = ResultCode.C1000,
                Message = "OK",
            };
        }
    }
}
