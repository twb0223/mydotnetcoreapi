using Myapi.Models;
using Myapi.SqlContext;
using Myapi.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Myapi.Services
{
    public class AccountServices
    {
        private MySqlContext mySqlContext;
        public AccountServices(MySqlContext _mysqlcontext)
        {
            this.mySqlContext = _mysqlcontext;
        }
        public IEnumerable<Account> GetAll()
        {
            return mySqlContext.Accounts.ToList();
        }
        public IEnumerable<Account> GetAll(Func<Account, bool> exp)
        {
            return mySqlContext.Accounts.Where(exp).ToList();
        }
        public void Insert(Account account)
        {
            mySqlContext.Accounts.Add(account);
            mySqlContext.SaveChanges();
        }
        public IEnumerable<Account_ApplicationDto> GetAccountApplist()
        {
            var result = (from a in mySqlContext.Applications
                          join b in mySqlContext.Accounts
                          on a.AccountID equals b.ID into hh
                          from h in hh.DefaultIfEmpty()
                          select new Account_ApplicationDto
                          {
                              AppId = a.AppId,
                              AppName = a.AppName,
                              AppSecret = a.AppSecret,
                              UserName = h.UserName,
                              CreateTime = a.CreateTime
                          }).ToList();
            return result;
        }
    }
}
