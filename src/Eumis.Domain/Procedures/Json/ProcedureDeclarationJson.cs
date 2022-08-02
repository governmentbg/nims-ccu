using Eumis.Domain.OperationalMap.ProgrammeDeclarations;
using System;
using System.Collections.Generic;

namespace Eumis.Domain.Procedures.Json
{
    public class ProcedureDeclarationJson
    {
        public ProcedureDeclarationJson()
        {
            this.Items = new List<ProcedureDeclarationItemJson>();
        }

        public int ProcedureDeclarationId { get; set; }

        public Guid Gid { get; set; }

        public int OrderNum { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public string Content { get; set; }

        public string ContentAlt { get; set; }

        public FieldType FieldType { get; set; }

        public bool IsRequired { get; set; }

        public bool IsActive { get; set; }

        public IList<ProcedureDeclarationItemJson> Items { get; set; }
    }
}
