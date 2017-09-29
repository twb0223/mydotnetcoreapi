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
    public class ApplicationController : Controller
    {
        private SqlContext.MySqlContext mySqlContext;
        private AppServices appServices;

        public ApplicationController()
        {
            this.mySqlContext = new MySqlContext();
            this.appServices = new AppServices(mySqlContext);
        }
        
    }
}
