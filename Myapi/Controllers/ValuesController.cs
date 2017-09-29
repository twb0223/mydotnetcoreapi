using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Myapi.Services;
using Myapi.Models;
using Myapi.SqlContext;
using Myapi.Common;
using Newtonsoft.Json;

namespace Myapi.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private SqlContext.MySqlContext mySqlContext;
        private AccountServices accountServices;

        public ValuesController()
        {
            this.mySqlContext = new MySqlContext();
            this.accountServices = new AccountServices(mySqlContext);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            CommonResult<Account> json = new CommonResult<Account>
            {
                Code = "000",
                Message = "ok",
                Data = accountServices.GetAll(x => x.ID != null).FirstOrDefault()
            };
            return JsonConvert.SerializeObject(json);
        }
    }
}
