using Eumis.Common.Log;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;

namespace Eumis.Data.Core.Interception
{
    public class WhereInFixupEumisDbCommandInterceptor : IEumisDbCommandInterceptor
    {
        private static Regex whereInRegex = new Regex(
            @"(\s+IN\s*\(\s*)" + ////                                                  IN (                           [GROUP 1]
            @"(?:" + ////                                                                  any one of the following
                @"(\d+(?:\s*,\s*\d+)*)|" + ////                                            1, 2, 3, 4                 [GROUP 2]
                @"(N'(?:[^']|'')*'(?:\s*,\s*N'(?:[^']|'')*')*)" + ////                     N'a', N'b', N'c'           [GROUP 3]
            @")" +
            @"(\s*\))", ////                                                           )                              [GROUP 4]
            RegexOptions.Compiled);

        private ILogger logger;

        public WhereInFixupEumisDbCommandInterceptor(ILogger logger)
        {
            this.logger = logger;
        }

        public int Order
        {
            get { return 1; }
        }

        private static string WhereInReplace(Match match, List<SqlParameter> extraParameters)
        {
            string tableType;
            string columnName;
            Func<object, SqlDataRecord> createRecord;
            List<object> values;

            if (match.Groups[2].Length > 0)
            {
                tableType = "[dbo].[IntTableType]";
                columnName = "Item1";
                createRecord = (v) =>
                {
                    var record = new SqlDataRecord(new SqlMetaData(columnName, SqlDbType.Int));
                    record.SetInt32(0, (int)v);
                    return record;
                };

                values = match.Groups[2].Value
                    .Split(',')
                    .Select(s => s.Trim())
                    .Select(s => int.Parse(s))
                    .Cast<object>()
                    .ToList();
            }
            else if (match.Groups[3].Length > 0)
            {
                tableType = "[dbo].[StringTableType]";
                columnName = "Item1";
                createRecord = (v) =>
                {
                    var record = new SqlDataRecord(new SqlMetaData(columnName, SqlDbType.NVarChar, SqlMetaData.Max));
                    record.SetString(0, (string)v);
                    return record;
                };

                values = match.Groups[3].Value
                    .Split(',')
                    .Select(s => s.Trim())
                    .Select(s => s.Substring(2, s.Length - 3).Replace("''", "'"))
                    .Cast<object>()
                    .ToList();
            }
            else
            {
                throw new Exception("Unexpected match. Check the regex. This statement should be unreachable.");
            }

            string paramName = "@WHEREINFIXUP_" + extraParameters.Count;
            string replacement = "SELECT " + columnName + " FROM " + paramName;

            extraParameters.Add(
                new SqlParameter(paramName, SqlDbType.Structured)
                {
                    TypeName = tableType,
                    Value = values.Select(createRecord),
                });

            return match.Result("$1" + replacement + "$4");
        }

        private static Tuple<string, List<SqlParameter>> Replace(string commandText)
        {
            var extraParameters = new List<SqlParameter>();

            string newQuery = whereInRegex.Replace(commandText, (m) => WhereInReplace(m, extraParameters));

            return Tuple.Create(newQuery, extraParameters);
        }

        public void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
        }

        public void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
        }

        public void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
        }

        public void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            this.Fixup(command);
        }

        public void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
        }

        public void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            this.Fixup(command);
        }

        private void Fixup(DbCommand dbCommand)
        {
            try
            {
                SqlCommand command = dbCommand as SqlCommand;
                if (command == null)
                {
                    return;
                }

                if (command.CommandType != CommandType.Text)
                {
                    return;
                }

                var res = Replace(command.CommandText);
                string newQuery = res.Item1;
                List<SqlParameter> extraParameters = res.Item2;

                command.CommandText = newQuery;
                command.Parameters.AddRange(extraParameters.ToArray());
            }
            catch (Exception e)
            {
                this.logger.Log(LogLevel.Error, "An error occurred while trying two replace WhereIn clause.", e);
            }
        }
    }
}
