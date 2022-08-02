using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Eumis.Portal.Web.Controllers
{
    public partial class AdminController : Controller
    {
        public virtual ActionResult ActiveSessions()
        {
            object sessionCount = 0;

            var connectionString =
                ((SessionStateSection) WebConfigurationManager.GetSection("system.web/sessionState"))
                    .SqlConnectionString;

            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                var sql = @"SELECT COUNT(DISTINCT SessionId) AS SessionCount
                                FROM ASPStateTempSessions";

                if (connection.State != ConnectionState.Open) connection.Open();

                SqlCommand command = new SqlCommand(sql, connection);

                sessionCount = command.ExecuteScalar();

                if (connection.State != ConnectionState.Closed) connection.Close();
            }

            return View(sessionCount);
        }
    }
}