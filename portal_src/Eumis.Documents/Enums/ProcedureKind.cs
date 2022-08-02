using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Documents.Enums
{
    [Serializable]
    public class ProcedureKind
    {
        public ProcedureKind()
        {
        }

        public string Name { get; set; }

        public int Value { get; set; }

        public static readonly ProcedureKind Budget = new ProcedureKind { Value = 1, Name = "budget" };
        public static readonly ProcedureKind Schema = new ProcedureKind { Value = 2, Name = "schema" };

        private static List<ProcedureKind> GetProcedureKindList()
        {
            return new List<ProcedureKind>
            {
                ProcedureKind.Budget,
                ProcedureKind.Schema
            };
        }

        public static ProcedureKind GetProcedureKind(string name)
        {
            return GetProcedureKindList().Single(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
    }
}
