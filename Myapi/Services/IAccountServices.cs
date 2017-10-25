using System;
using System.Collections.Generic;
using Myapi.Models;
using Myapi.ViewModel;

namespace Myapi.Services
{
    public interface IAccountServices
    {
        IEnumerable<Account_ApplicationDto> GetAccountApplist();
        IEnumerable<Account> GetAll();
        IEnumerable<Account> GetAll(Func<Account, bool> exp);
        void Insert(Account account);
    }
}