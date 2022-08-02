using DbUp.Engine;
using DbUp.Engine.Transactions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Eumis.Database.Updater
{
    public class CustomEmbeddedScriptProvider : IScriptProvider
    {
        private readonly Regex nameRegex = new Regex(@"^Eumis\.Database\.Updater\.UpdateScripts\.(\d+)\.[sS][qQ][lL]$");
        private readonly Assembly assembly;

        public CustomEmbeddedScriptProvider()
        {
            this.assembly = Assembly.GetExecutingAssembly();
        }

        public IEnumerable<SqlScript> GetScripts(IConnectionManager connectionManager)
        {
            return (from res in this.assembly.GetManifestResourceNames()
                    let number = this.nameRegex.IsMatch(res) ? int.Parse(this.nameRegex.Match(res).Groups[1].Value) : 0
                    where number > 0
                    orderby number
                    select new SqlScript(number.ToString(), this.GetFileContents(number, this.assembly.GetManifestResourceStream(res))))
                   .ToList();
        }

        public string GetFileContents(long scriptNumber, Stream file)
        {
            // Read the BOM
            var bom = new byte[4];
            file.Read(bom, 0, 4);

            file.Position = 0;

            // Analyze the BOM
            if (bom[0] == 0xef && bom[1] == 0xbb && bom[2] == 0xbf)
            {
                using (StreamReader sr = new StreamReader(file, Encoding.UTF8, true))
                {
                    return sr.ReadToEnd();
                }
            }
            else
            {
                throw new Exception("Script with number " + scriptNumber + " is not encoded in UTF8 with BOM(signature)");
            }
        }
    }
}
