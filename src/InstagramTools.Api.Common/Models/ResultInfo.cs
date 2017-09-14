using System;

namespace InstagramTools.Api.Common.Models
{
    public class ResultInfo
    {
        public ResultInfo(string message)
        {
            this.Message = message;
        }

        public ResultInfo(Exception exception)
        {
            this.Exception = exception;
        }

        public ResultInfo(ResponseType responseType)
        {
            this.ResponseType = responseType;
        }

        public Exception Exception { get; }

        public string Message { get; }

        public ResponseType ResponseType { get; }
    }
}