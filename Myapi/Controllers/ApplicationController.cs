using Microsoft.AspNetCore.Mvc;
using Myapi.Services;
using Myapi.SqlContext;
using Myapi.Models;
using Myapi.Common;

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
        public ListResult<Application> GetAllApplications()
        {
            var commonResult = new ListResult<Application>
            {
                Code = ResultCode.C1000,
                Message = "OK",
                Data = appServices.GetList()
            };
            return commonResult;
        }
    }
}
