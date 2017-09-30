using Myapi.Models;
using Myapi.SqlContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Myapi.Services
{
    public class AppServices
    {
        private MySqlContext mySqlContext;

        public AppServices(MySqlContext mySqlContext)
        {
            this.mySqlContext = mySqlContext;
        }

        public IEnumerable<Application> GetList(Func<Application, bool> exp)
        {
            return mySqlContext.Applications.Where(exp).ToList();
        }

        public IEnumerable<Application> GetList()
        {
            return mySqlContext.Applications.ToList();
        }

        public void Insert(Application application)
        {
            mySqlContext.Applications.Add(application);
            mySqlContext.SaveChanges();
        }
    }
}
