using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Eumis.Database.Configurator
{
    [TestClass]
    public class DatabaseConfiguratorTests
    {
        [TestMethod]
        public void TestDatabaseConfigurator()
        {
            bool dummyFlag = false;

            if (dummyFlag)
            {
                DatabaseConfigurator configurator = new DatabaseConfigurator(this.GetExcelConfigPath());

                List<ScriptData> scriptDataList = configurator.GenerateScriptData();

                string outputDir = this.CreateOutputDir();

                foreach (var scriptData in scriptDataList)
                {
                    this.SaveOutput(outputDir, scriptData.FileName, scriptData.Content);
                }
            }
        }

        private string CreateOutputDir()
        {
            string assemblyPath = new System.Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;

            string outputDir = string.Format(@"{0}\ExcelConfig", Path.GetDirectoryName(assemblyPath));

            if (Directory.Exists(outputDir))
            {
                Directory.Delete(outputDir, true);
            }

            Directory.CreateDirectory(outputDir);

            return outputDir;
        }

        private void SaveOutput(string outputDir, string outputFileName, string contents)
        {
            File.WriteAllText(string.Format(@"{0}\{1}", outputDir, outputFileName), contents, Encoding.UTF8);
        }

        private string GetExcelConfigPath()
        {
            string assemblyPath = new System.Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            string excelConfigPath =
                string.Format(
                    @"{0}\Eumis.Database\Insert\ExcelConfig\ExcelConfig.xml",
                    Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(assemblyPath).FullName).FullName).FullName).FullName);

            return excelConfigPath;
        }
    }
}
