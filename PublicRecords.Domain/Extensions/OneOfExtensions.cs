using PublicRecords.Domain.Errors;
using OneOf;

namespace PublicRecords.Domain.Extensions
{
    public static class OneOfExtensions
    {
        public static bool IsSuccess<TResult>(this OneOf<TResult, BaseError> obj) => obj.IsT0;
        public static TResult GetValue<TResult>(this OneOf<TResult, BaseError> obj) => obj.AsT0;
        public static bool IsError<TResult>(this OneOf<TResult, BaseError> obj) => obj.IsT1;
        public static BaseError GetError<TResult>(this OneOf<TResult, BaseError> obj) => obj.AsT1;
    }
}
