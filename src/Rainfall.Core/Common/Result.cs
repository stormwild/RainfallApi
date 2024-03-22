namespace Rainfall.Core.Common;


public sealed class Result<TResult, TError>
{
    public bool IsError { get; }
    public TResult? Value { get; }
    public TError? Error { get; }

    public Result(TResult result) => Value = result;

    public Result(TError error)
    {
        Error = error;
        IsError = true;
    }

    public static implicit operator Result<TResult, TError>(TResult result) => new(result);
    public static implicit operator Result<TResult, TError>(TError error) => new(error);

    public TOutcome Match<TOutcome>(Func<TResult, TOutcome> success, Func<TError, TOutcome> failure) => IsError ? failure(Error!) : success(Value!);
}

// public record struct Result<TResult, TError>(TResult? Value, TError? Error)
// {
//     public readonly bool IsError => Error is not null;

//     public Result(TResult result) : this(result, default) { }

//     public Result(TError error) : this(default, error) { }

//     public static implicit operator Result<TResult, TError>(TResult result) => new(result);
//     public static implicit operator Result<TResult, TError>(TError error) => new(error);

//     public readonly TOutcome Match<TOutcome>(Func<TResult, TOutcome> success, Func<TError, TOutcome> failure) => IsError ? failure(Error!) : success(Value!);
// }