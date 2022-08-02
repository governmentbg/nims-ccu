using System;
using System.Linq.Expressions;

namespace Eumis.Data.Linq
{
    public static class ExpressionHelper
    {
        public static Func<T, TResult> GetFieldAccessor<T, TResult>(string fieldName)
        {
            ParameterExpression param = Expression.Parameter(typeof(T), "arg");

            MemberExpression member = Expression.Field(param, fieldName);

            LambdaExpression lambda = Expression.Lambda(typeof(Func<T, TResult>), member, param);

            return (Func<T, TResult>)lambda.Compile();
        }
    }
}
