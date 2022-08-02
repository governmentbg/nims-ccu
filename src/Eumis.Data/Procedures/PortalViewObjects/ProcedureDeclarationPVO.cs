using Eumis.Domain.OperationalMap.ProgrammeDeclarations;
using Eumis.Domain.Procedures.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Data.Procedures.PortalViewObjects
{
    public class ProcedureDeclarationPVO
    {
        public ProcedureDeclarationPVO()
        {
            this.Items = new List<ProcedureDeclarationItemPVO>();
        }

        public ProcedureDeclarationPVO(ProcedureDeclarationJson declaration)
            : this()
        {
            this.Gid = declaration.Gid;
            this.OrderNum = declaration.OrderNum;
            this.Name = declaration.Name;
            this.NameAlt = declaration.NameAlt;
            this.Content = declaration.Content;
            this.ContentAlt = declaration.ContentAlt;
            this.FieldType = declaration.FieldType;
            this.IsRequired = declaration.IsRequired;
            this.IsActive = declaration.IsActive;

            this.Items = declaration.Items.Select(di => new ProcedureDeclarationItemPVO(di)).ToList();
        }

        public Guid Gid { get; set; }

        public int OrderNum { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public string Content { get; set; }

        public string ContentAlt { get; set; }

        public FieldType FieldType { get; set; }

        public bool IsRequired { get; set; }

        public bool IsActive { get; set; }

        public IList<ProcedureDeclarationItemPVO> Items { get; set; }
    }
}
