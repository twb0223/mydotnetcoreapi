using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Myapi.SqlContext;
using Myapi.Models;

namespace Myapi.Services
{
    public class AccountServices
    {
        private MySqlContext mySqlContext;

        public AccountServices(MySqlContext _mysqlcontext)
        {
            this.mySqlContext=_mysqlcontext;
        }
        public IEnumerable<Account> GetAll()
        {
            return mySqlContext.Accounts.ToList();            
        }
        public IEnumerable<Account> GetAll(Func<Account,bool> exp)
        {
            return mySqlContext.Accounts.Where(exp).ToList();            
        }
        public void Insert(Account account)
        {
            mySqlContext.Accounts.Add(account);
            mySqlContext.SaveChanges();            
        }

        public IEnumerable<object> GetAccountApplist()
        {
            var result = (from a in mySqlContext.Applications
                          join b in mySqlContext.Accounts
                          on a.AccountID equals b.ID into hh
                          from h in hh.DefaultIfEmpty()
                          select new
                          {
                              a.AppId,
                              a.AppName,
                              a.AppSecret,
                              h.UserName,
                              a.CreateTime
                          }).ToList();
            return result;
        }
    }
}
