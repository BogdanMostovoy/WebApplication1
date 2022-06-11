using System;

namespace Web.Models;

public class Result
{
    public string ErrorMessage { get; private init; } = string.Empty; 
    public bool IsSuccess { get; private init; }
    public bool IsFailure => IsSuccess;

    protected Result()
    {
        IsSuccess = true;
    }
    
    protected Result(string errorMessage)
    {
        IsSuccess = false;
        ErrorMessage = errorMessage;
    }
    
}

public class Result<T> : Result
{
    public T Value { get; init; }
    
    public Result(T value)
    {
        Value = value;
    }

    public Result(string errorMessage): base(errorMessage)
    {
        Value = default;
    }

    public static implicit operator Result<T>(T value) => new(value);
}