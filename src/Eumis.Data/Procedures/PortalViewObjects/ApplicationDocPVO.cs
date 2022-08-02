using System;
using Eumis.Domain.Procedures.Json;

namespace Eumis.Data.Procedures.PortalViewObjects
{
    public class ApplicationDocPVO
    {
        public ApplicationDocPVO(ProcedureAppDocJson appDoc)
        {
            this.Gid = appDoc.Gid;
            this.Name = appDoc.Name;
            this.Extension = appDoc.Extension;
            this.IsRequired = appDoc.IsRequired;
            this.IsSignatureRequired = appDoc.IsSignatureRequired;
            this.IsOriginal = true;
            this.IsActive = appDoc.IsActive;
        }

        public Guid Gid { get; set; }

        public string Name { get; set; }

        public string Extension { get; set; }

        public bool IsRequired { get; set; }

        public bool IsSignatureRequired { get; set; }

        public bool IsOriginal { get; set; }

        public bool IsActive { get; set; }
    }
}
