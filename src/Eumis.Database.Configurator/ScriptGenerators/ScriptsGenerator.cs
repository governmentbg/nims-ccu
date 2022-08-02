using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eumis.Database.Configurator.ScriptGenerators
{
    internal abstract class ScriptsGenerator
    {
        protected ScriptsGenerator(Dictionary<string, List<Dictionary<int, string>>> sheetsRowsDictionary)
        {
            this.SheetsRowsDictionary = sheetsRowsDictionary;
        }

        protected Dictionary<string, List<Dictionary<int, string>>> SheetsRowsDictionary { get; private set; }

        protected abstract List<DbTableData> GetDbTableData();

        protected List<Dictionary<int, string>> GetExcelSheetRows(string sheetName)
        {
            return this.SheetsRowsDictionary.SingleOrDefault(e => e.Key.ToLower().Trim() == sheetName.ToLower().Trim()).Value;
        }

        public List<ScriptData> CreateScriptsData()
        {
            List<ScriptData> scriptDataList = new List<ScriptData>();

            foreach (var dbTableData in this.GetDbTableData())
            {
                StringBuilder scriptBuilder = new StringBuilder();

                scriptBuilder.AppendLine(string.Format("print 'Excel Insert {0}'", dbTableData.TableName));
                scriptBuilder.AppendLine("GO");
                scriptBuilder.AppendLine();

                if (dbTableData.UseIdentityInsert)
                {
                    scriptBuilder.AppendLine(string.Format("SET IDENTITY_INSERT [{0}] ON", dbTableData.TableName));
                    scriptBuilder.AppendLine();
                }

                foreach (var dbRow in dbTableData.DbRows)
                {
                    scriptBuilder.AppendLine(dbRow.CreateRowInsert());
                }

                if (dbTableData.UseIdentityInsert)
                {
                    scriptBuilder.AppendLine();
                    scriptBuilder.AppendLine(string.Format("SET IDENTITY_INSERT [{0}] OFF", dbTableData.TableName));
                }

                scriptBuilder.AppendLine("GO");
                scriptBuilder.AppendLine();

                ScriptData scriptData = new ScriptData(string.Format("{0}.sql", dbTableData.TableName), scriptBuilder.ToString());

                scriptDataList.Add(scriptData);
            }

            return scriptDataList;
        }
    }
}
