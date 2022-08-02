using Eumis.Domain.Procedures.Json;
using System;

namespace Eumis.Data.Procedures.PortalViewObjects
{
    public class ProcedureDeclarationItemPVO
    {
        public ProcedureDeclarationItemPVO()
        {
        }

        public ProcedureDeclarationItemPVO(ProcedureDeclarationItemJson item)
        {
            this.Gid = item.Gid;
            this.OrderNum = item.OrderNum;
            this.Content = item.Content;
            this.IsActive = item.IsActive;
        }

        public Guid Gid { get; set; }

        public int OrderNum { get; set; }

        public string Content { get; set; }

        public bool IsActive { get; set; }
    }
}
