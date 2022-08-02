using System;
using System.Linq.Expressions;

namespace Eumis.Public.Data.Core
{
    public interface IRepository
    {
        void LoadReference<TEntity, TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> navigationProperty)
            where TEntity : class
            where TProperty : class;
    }
}
