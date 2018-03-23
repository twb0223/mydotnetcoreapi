using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Myapi.ViewModel
{
    public class Account_ApplicationDto
    {
       public int AppId { get; set; }
        public string AppName { get; set; }
        public string AppSecret { get; set; }
        public string UserName { get; set; }
        public DateTime CreateTime { get; set; }
    }
    public class AppDto
    {
        public string AppName { get; set; }
        public int AccountID { get; set; }
    }

    public class Token
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string expires_in { get; set; }
    }

    public class AuthDto
    {
        public string clientid { get; set; }
        public string secret { get; set; }
    }
}
