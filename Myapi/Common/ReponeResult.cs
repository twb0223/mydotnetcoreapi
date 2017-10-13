using Newtonsoft.Json;
using System.Collections.Generic;

namespace Myapi.Common
{
    public class BaseResponseResult
    {
        public ResultCode Code { get; set; }

        public string Message { get; set; }
    }

    public class RequestInfo
    {
        public string AppID { get; set; }
        public string Timestamp { get; set; }
        public string Nonce { get; set; }
        public string Sinature { get; set; }
        public string AppSecret { get; set; }
    }
    public class ListResult<T> : BaseResponseResult where T : class
    {
        public IEnumerable<T> Data { get; set; }
    }
    public class SingleResult<T> : BaseResponseResult where T : class
    {
        public T Data { get; set; }
    }
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
    }
    public enum ResultCode
    {
        C1000 = 1000,
        C1001 = 1001,
        C1002 = 1002,
        C1003 = 1003,
        S408 = 408,
        S401 = 401
    }
}