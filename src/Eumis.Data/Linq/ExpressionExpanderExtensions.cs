using System.Linq.Expressions;

namespace Eumis.Data.Linq
{
    public static class ExpressionExpanderExtensions
    {
        public static Expression<T> Expand<T>(this Expression<T> expr)
        {
            return (Expression<T>)new ExpressionExpander().Visit(expr);
        }

        public static Expression Expand(this Expression expr)
        {
            return new ExpressionExpander().Visit(expr);
        }
    }
}
