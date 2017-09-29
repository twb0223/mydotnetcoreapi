namespace Myapi.Common
{
    public class BaseResponseResult
    {
        public string Code { get; set; }

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

    public class CommonResult<T> : BaseResponseResult where T : class
    {
        public T Data { get; set; }
    }

    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
    }
}