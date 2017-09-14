using System;

namespace InstagramTools.Api.Common.Models
{
    public class Result<T> : IResult<T>
    {
        public Result(bool succeeded, T value, ResultInfo info)
        {
            this.Succeeded = succeeded;
            this.Value = value;
            this.Info = info;
        }

        public Result(bool succeeded, ResultInfo info)
        {
            this.Succeeded = succeeded;
            this.Info = info;
        }

        public Result(bool succeeded, T value)
        {
            this.Succeeded = succeeded;
            this.Value = value;
        }

        public bool Succeeded { get; }
        public T Value { get; }
        public ResultInfo Info { get; } = new ResultInfo(string.Empty);
    }

    public static class Result
    {
        public static IResult<T> Success<T>(T resValue)
            => new Result<T>(true, resValue);

        public static IResult<T> Success<T>(string successMsg, T resValue)
            => new Result<T>(true, resValue, new ResultInfo(successMsg));

        public static IResult<T> Fail<T>(Exception exception)
            => new Result<T>(false, default(T), new ResultInfo(exception));

        public static IResult<T> Fail<T>(string errMsg)
            => new Result<T>(false, default(T), new ResultInfo(errMsg));

        public static IResult<T> Fail<T>(string errMsg, T resValue)
            => new Result<T>(false, resValue, new ResultInfo(errMsg));

        public static IResult<T> Fail<T>(string errMsg, ResponseType responseType, T resValue)
            => new Result<T>(false, resValue, new ResultInfo(responseType));
    }
}