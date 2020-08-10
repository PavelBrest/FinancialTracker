#nullable enable
using System;

namespace FinancialTracker.Core.Commons
{
    public class Result
    {
        private static readonly Result _okResult = new Result();

        public bool IsSuccess { get; protected set; }
        public Exception? Exception { get; protected set; }
        public string? ErrorMessage => Exception?.Message;

        protected internal Result()
        {
            IsSuccess = true;
        }

        protected internal Result(Exception exception)
        {
            IsSuccess = false;
            Exception = exception ?? throw new ArgumentNullException(nameof(exception));
        }

        public static Result Ok() => _okResult;

        public static Result<TValue> Ok<TValue>(TValue value) => new Result<TValue>(value);

        public static Result Failed(Exception exception) => new Result(exception);

        public static Result<TValue> Failed<TValue>(Exception exception) => new Result<TValue>(exception);

        public static Result<TValue> Wrap<TValue>(Result result, TValue value)
        {
            return result.IsSuccess 
                ? new Result<TValue>(value)
                : new Result<TValue>(result.Exception!);
        }
    }

    public sealed class Result<TValue> : Result
    {
        public TValue Value { get; }

        internal Result(TValue value)
        {
            Value = value;
        }

        internal Result(Exception exception)  : base(exception)
        {
            Value = default!;
        }
    }
}