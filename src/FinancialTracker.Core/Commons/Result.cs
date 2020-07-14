#nullable enable
using System;

namespace FinancialTracker.Core.Commons
{
    public class Result
    {
        public bool IsSuccess { get; protected set; }
        public string? ErrorMessage { get; protected set; }

        protected internal Result()
        {
            IsSuccess = true;
        }

        protected internal Result(string message)
        {
            IsSuccess = false;
            ErrorMessage = message ?? throw new ArgumentNullException(nameof(message));
        }

        public static Result Ok() => new Result();

        public static Result<TValue> Ok<TValue>(TValue value) => new Result<TValue>(value);

        public static Result Failed(string message) => new Result(message);

        public static Result<TValue> Failed<TValue>(string message) => new Result<TValue>(message);
    }

    public sealed class Result<TValue> : Result
    {
        public TValue Value { get; }

        internal Result(TValue value)
        {
            Value = value;
        }

        internal Result(string message)  : base(message)
        {
            Value = default!;
        }
    }
}