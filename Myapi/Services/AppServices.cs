﻿using Myapi.Models;
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
        public bool CheckAppinfo(string appid,string appSecret)
        {
            return mySqlContext.Applications.Any(x=>x.AppId.ToString()==appid&x.AppSecret==appSecret);
        }
        public IEnumerable<Application> GetList(Func<Application, bool> exp)
        {
            return mySqlContext.Applications.Where(exp).ToList();
        }
        public IEnumerable<Application> GetPageList(int pagesize, int pageindex) => mySqlContext.Applications.Skip(pagesize * (pageindex - 1)).Take(pagesize).OrderBy(x => x.CreateTime);
        public int GetApplicationsCout()
        {
            return mySqlContext.Applications.Count();
        }
        public void Insert(Application application)
        {
            mySqlContext.Applications.Add(application);
            mySqlContext.SaveChanges();
        }
        public void InsertList(List<Application> applicationlist)
        {
            foreach (var item in applicationlist)
            {
                 mySqlContext.Applications.Add(item);
            }           
            mySqlContext.SaveChanges(); 
        }
    }
}
