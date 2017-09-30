using Microsoft.AspNetCore.Mvc;
using Myapi.Services;
using Myapi.SqlContext;

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
