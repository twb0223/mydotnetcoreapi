using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Myapi.ViewModel
{
    public class Account_ApplicationVM
    {
       public Guid AppId { get; set; }
        public string AppName { get; set; }
        public string AppSecret { get; set; }
        public string UserName { get; set; }
        public DateTime CreateTime { get; set; }
    }
    public class AppDto
    {
        public string AppName { get; set; }
        public Guid AccountID { get; set; }
    }
}
