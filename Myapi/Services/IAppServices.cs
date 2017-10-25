using System;
using System.Collections.Generic;
using Myapi.Models;

namespace Myapi.Services
{
    public interface IAppServices
    {
        bool CheckAppinfo(string appid, string appSecret);
        int GetApplicationsCout();
        IEnumerable<Application> GetList(Func<Application, bool> exp);
        IEnumerable<Application> GetPageList(int pagesize, int pageindex);
        void Insert(Application application);
        void InsertList(List<Application> applicationlist);
    }
}