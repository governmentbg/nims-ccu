using System;

namespace Eumis.Public.Domain.Entities.Umis.Procedures.Json
{
    public class ProcedureAppDocJson
    {
        public ProcedureAppDocJson()
        {
        }

        public ProcedureAppDocJson(ProcedureApplicationDoc appDoc)
        {
            this.AppDocId = appDoc.ProcedureApplicationDocId;
            this.Gid = appDoc.Gid;
            this.Name = appDoc.Name;
            this.Extension = appDoc.Extension;
            this.IsRequired = appDoc.IsRequired;
            this.IsSignatureRequired = appDoc.IsSignatureRequired;
            this.IsActive = appDoc.IsActive;
        }

        public int AppDocId { get; set; }

        public Guid Gid { get; set; }

        public string Name { get; set; }

        public string Extension { get; set; }

        public bool IsRequired { get; set; }

        public bool IsSignatureRequired { get; set; }

        public bool IsActive { get; set; }
    }
}
