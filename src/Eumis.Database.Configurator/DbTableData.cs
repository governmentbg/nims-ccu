using Eumis.Database.Configurator.DbRows;
using System.Collections.Generic;

namespace Eumis.Database.Configurator
{
    internal class DbTableData
    {
        public DbTableData(string tableName, bool useIdentityInsert, List<IDbRow> dbRows)
        {
            this.TableName = tableName;
            this.UseIdentityInsert = useIdentityInsert;
            this.DbRows = dbRows;
        }

        public string TableName { get; private set; }

        public bool UseIdentityInsert { get; private set; }

        public List<IDbRow> DbRows { get; private set; }
    }
}
