using System;
using System.Linq.Expressions;

namespace Eumis.Public.Data.Linq
{
    public class ExpressionHelper
    {
        public static Func<T1, T2> GetFieldAccessor<T1, T2>(string fieldName)
        {
            ParameterExpression param = Expression.Parameter(typeof(T1), "arg");

            MemberExpression member = Expression.Field(param, fieldName);

            LambdaExpression lambda = Expression.Lambda(typeof(Func<T1, T2>), member, param);

            return (Func<T1, T2>)lambda.Compile();
        }
    }
}
