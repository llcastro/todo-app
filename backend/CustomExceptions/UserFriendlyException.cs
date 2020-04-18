using System;
using System.Net;

namespace backend.CustomExceptions
{
    [Serializable]
    public class UserFriendlyException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public UserFriendlyException(string Message) : base(Message)
        {
            this.StatusCode = HttpStatusCode.InternalServerError;
        }
        public UserFriendlyException(string Message, HttpStatusCode StatusCode) : base(Message)
        {
            this.StatusCode = StatusCode;
        }
    }
}