﻿using Eumis.Common.Db;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Eumis.Data.Core
{
    internal class Repository : IRepository
    {
        [SuppressMessage("", "SA1401:FieldsMustBePrivate", Justification = "TODO: review later")]
        protected UnitOfWork unitOfWork;

        public Repository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = (UnitOfWork)unitOfWork;
        }

        public void LoadReference<TEntity, TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> navigationProperty)
            where TEntity : class
            where TProperty : class
        {
            this.unitOfWork.DbContext.Entry(entity).Reference(navigationProperty).Load();
        }

        #region Protected methods

        protected virtual void ExecuteSqlCommand(string sql, params SqlParameter[] parameters)
        {
            this.unitOfWork.DbContext.Database.ExecuteSqlCommand(sql, parameters);
        }

        protected async virtual Task ExecuteSqlCommandAsync(string sql, CancellationToken ct, params SqlParameter[] parameters)
        {
            await this.unitOfWork.DbContext.Database.ExecuteSqlCommandAsync(sql, ct, parameters);
        }

        protected DbRawSqlQuery<TSpEntity> ExecProcedure<TSpEntity>(string procedureName, List<SqlParameter> parameters)
        {
            StringBuilder sb = new StringBuilder(procedureName + " ");

            for (int i = 0; i < parameters.Count; i++)
            {
                sb.AppendFormat("@{0}", parameters[i].ParameterName);
                if (i != parameters.Count - 1)
                {
                    sb.Append(", ");
                }
            }

            return this.unitOfWork.DbContext.Database.SqlQuery<TSpEntity>(
                sb.ToString(),
                parameters.ToArray());
        }

        protected List<TSpEntity> ExecFunction<TSpEntity>(string procedureName, List<SqlParameter> parameters)
        {
            StringBuilder sb = new StringBuilder("SELECT * FROM " + procedureName + "(");

            for (int i = 0; i < parameters.Count; i++)
            {
                sb.AppendFormat("@{0}", parameters[i].ParameterName);
                if (i != parameters.Count - 1)
                {
                    sb.Append(", ");
                }
            }

            sb.Append(")");

            return this.unitOfWork.DbContext.Database.SqlQuery<TSpEntity>(
                sb.ToString(),
                parameters.ToArray()).ToList();
        }

        protected DbRawSqlQuery<TSpEntity> SqlQuery<TSpEntity>(string sql, List<SqlParameter> parameters)
        {
            return this.unitOfWork.DbContext.Database.SqlQuery<TSpEntity>(sql, parameters.ToArray());
        }

        #endregion
    }
}
