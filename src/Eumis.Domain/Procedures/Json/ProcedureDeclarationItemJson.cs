using System;

namespace Eumis.Domain.Procedures.Json
{
    public class ProcedureDeclarationItemJson
    {
        public int ProgrammeDeclarationItemId { get; set; }

        public Guid Gid { get; set; }

        public int OrderNum { get; set; }

        public string Content { get; set; }

        public bool IsActive { get; set; }
    }
}
