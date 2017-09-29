using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Myapi.Services;
using Myapi.SqlContext;

namespace Myapi.Controllers
{

    [Route("api/Accounts")]
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


        // GET: api/Accounts
        [HttpGet]
        public IEnumerable<object> Get()
        {
            return accountServices.GetAccountApplist();
        }
    }
}
