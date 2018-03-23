using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Myapi.Common;
using Myapi.Services;
using Myapi.SqlContext;
using Myapi.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Myapi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        private IAccountServices accountServices;
        private IAppServices appServices;
        public AccountsController(IAccountServices _accountServices, IAppServices _appServices)
        {

            this.accountServices = _accountServices;
            this.appServices = _appServices;
        }

        [HttpGet]
        [Route("/api/Claims")]
        public IActionResult GetClaims()
        {
            return new JsonResult(from c in User.Claims
                                  select 
                                  new { c.Issuer,
                                        c.OriginalIssuer,
                                        c.Type,
                                        c.Value
                                  });
        }

        [HttpGet]
        public ListResult<Account_ApplicationDto> Get()
        {
            var commonResult = new ListResult<Account_ApplicationDto>
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
                ID =new Random().Next(100000,999999),
                AccountID = new Random().Next(100000, 999999),
                AppId = new Random().Next(100000, 999999),
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
